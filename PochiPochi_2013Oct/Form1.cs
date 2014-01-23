using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using FeaturePoints;

namespace PochiPochi_2013Oct
{
    public partial class Form1 : Form
    {

        string _currentImageFileNameWithoutExtention = null;

        public Form1()
        {
            InitializeComponent();

            //_featImage._pictureBoxにMouseDownイベントを追加
            _featImage._PictureBox.MouseDown += new MouseEventHandler(addedEvent_after_pictureBoxMouseDown);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            _radioButton_modeAddErase.PerformClick();
            _radioButton_coordLeftTopZero.PerformClick();
        }

        private void _button_openNewImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "ImageFile|*.bmp;*.png;*.jpg;*.jpeg;*.tif|AllFile|*.*";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _featImage.Image = new Bitmap(ofd.FileName);    //画像の読み込み
                this.Text = ofd.FileName;   //タイトルバーの更新
                _currentImageFileNameWithoutExtention = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName);
            }
        }

        private void _textBox_scale_TextChanged(object sender, EventArgs e)
        {
            if(_textBox_scale.Text!="" && double.Parse(_textBox_scale.Text)>0.0) _featImage.ImageScale = double.Parse(_textBox_scale.Text);
        }

        private void _button_saveFPs_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV|*.csv|TXT|*.txt|ALL|*.*";
            sfd.FileName = _currentImageFileNameWithoutExtention;
            if (sfd.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            //まずは左上(0,0)の座標系で座標を取得
            FeaturePointsList fpList_lt = _featImage.FeaturePoints;

            ////指定の座標系に変換
            //FeaturePoints.FeaturePointsList fpList = fpList_lt.DeepClone(); //変換後の特徴点座標, 要素数確保のため、ダミーとしてfpList_ltの深いコピーを入れておく
            FeaturePoints.EFPCoordinate coordOpt = CheckCoordinateOption();
            FeaturePoints.FPCoordTransformer trans = new FPCoordTransformer(FeaturePoints.EFPCoordinate.LeftTopZero, coordOpt, _featImage.Image.Height);
            FeaturePointsList fpList=trans.Transform(fpList_lt);

            FeaturePoints.FPListSaver saver = FeaturePoints.FPListSaverFactory.Create(ESaveFormat.CSV_XYF);
            saver.Save(sfd.FileName, fpList);


        }
        FeaturePoints.EFPCoordinate CheckCoordinateOption()
        {
            if (_radioButton_coordLeftBottomOne.Checked) return FeaturePoints.EFPCoordinate.LeftBottomOne;
            else if (_radioButton_coordLeftBottomZero.Checked) return FeaturePoints.EFPCoordinate.LeftBottomZero;
            else if (_radioButton_coordLeftTopZero.Checked) return FeaturePoints.EFPCoordinate.LeftTopZero;
            else
            {
                MessageBox.Show("座標系指定エラー@CheckCoordinateOption():\n左上原点として扱います", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new NotImplementedException();
            }

        }

        

        private FeaturePoints.FeaturePointsList LoadFPsAsCSV(string filename)
        {
            FeaturePoints.FeaturePointsList fpList = new FeaturePoints.FeaturePointsList();

            string[] strDataMat = System.IO.File.ReadAllLines(filename);
            foreach (string line in strDataMat)
            {
                string[] vals = line.Split(',');

                //空行の処理(主に最終行を仮定)
                if (vals[0] == "" || vals == null) continue;

                int x = int.Parse(vals[0]);
                int y = int.Parse(vals[1]);
                bool isValid = (int.Parse(vals[2]) == 1);

                fpList.Push(new FeaturePoints.FeaturePointWithValidFlag(new Point(x, y), isValid));
            }

            return fpList;
        }

        private void _button_loadFPs_Click(object sender, EventArgs e)
        {
            //画像が読み込まれていない場合は処理しない(座標系の変換には画像サイズが必要のため)
            if (_featImage.Image == null)
            {
                MessageBox.Show("先に画像を指定してください", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV|*.csv|TXT|*.txt|ALL|*.*";
            ofd.FileName = _currentImageFileNameWithoutExtention;
            if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            FeaturePoints.FeaturePointsList fpList_orig = LoadFPsAsCSV(ofd.FileName);
            

            //指定の座標系に変換
            FeaturePoints.FeaturePointsList fpList = fpList_orig.DeepClone(); //変換後の特徴点座標, 要素数確保のため、ダミーとしてfpList_ltの深いコピーを入れておく
            FeaturePoints.EFPCoordinate coordOpt = CheckCoordinateOption();

            int n = fpList_orig.Count();
            for (int i = 0; i < n; i++)
            {
                Point pt;
                int imgHeight = _featImage.Image.Height;
                if (coordOpt == FeaturePoints.EFPCoordinate.LeftTopZero) pt = fpList_orig[i].Point;
                else if (coordOpt == FeaturePoints.EFPCoordinate.LeftBottomZero) pt = new Point(fpList_orig[i].Point.X, imgHeight - fpList_orig[i].Point.Y - 1);
                else if (coordOpt == FeaturePoints.EFPCoordinate.LeftBottomOne) pt = new Point(fpList_orig[i].Point.X - 1, imgHeight - fpList_orig[i].Point.Y);
                else
                {
                    MessageBox.Show("座標系指定エラー@_button_saveFPs_Click(object sender, EventArgs e):\n左上原点として扱います", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    pt = fpList_orig[i].Point;
                    throw new NotImplementedException();
                }

                fpList[i] = new FeaturePoints.FeaturePointWithValidFlag(pt, fpList_orig[i].Valid);

            }


            _featImage.FeaturePoints = fpList;
        }

        private void _radioButton_modeAddErase_Click(object sender, EventArgs e)
        {
            //順番に注意 必ずMouseModeを変更してからFPConditionを変更
            _featImage.MouseMode = FeatImage.FeatImage.EMouseMode.AddAndErase;
            _groupBox_fpCondition.Enabled = false;

            _featImage.CurrentFPCondition = FeatImage.FeatImage.EFeaturePointCondition.Visible;
            
        }

        private void _radioButton_modeMove_Click(object sender, EventArgs e)
        {
            //順番に注意 必ずMouseModeを変更してからFPConditionを変更
            _featImage.MouseMode = FeatImage.FeatImage.EMouseMode.Move;
            _groupBox_fpCondition.Enabled = true;
            
        }

        public void addedEvent_after_pictureBoxMouseDown(object sender, MouseEventArgs e)
        {
            //特徴点の状態(見える/見えない)をフィードバック
            FeatImage.FeatImage.EFeaturePointCondition? fpCondition = _featImage.CurrentFPCondition;
            if (fpCondition == FeatImage.FeatImage.EFeaturePointCondition.Invisible) {
                _radioButton_fpConditionInvisible.Checked = true;
                _radioButton_fpConditionVisible.Checked = false;
            }
            else if (fpCondition == FeatImage.FeatImage.EFeaturePointCondition.Visible)
            {
                _radioButton_fpConditionInvisible.Checked = false;
                _radioButton_fpConditionVisible.Checked = true;
            }

            //特徴点数をフィードバック
            _label_numFP.Text = _featImage.FeaturePoints.Count().ToString();
        }

        private void _radioButton_fpConditionVisible_Click(object sender, EventArgs e)
        {
            _featImage.CurrentFPCondition = FeatImage.FeatImage.EFeaturePointCondition.Visible;
        }

        private void _radioButton_fpConditionInvisible_Click(object sender, EventArgs e)
        {
            _featImage.CurrentFPCondition = FeatImage.FeatImage.EFeaturePointCondition.Invisible;
        }

        private void _button_SaveImage_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "png|*.png|jpeg|*.jpg|bitmap|*.bmp|ALL|*.*";
            sfd.FileName = _currentImageFileNameWithoutExtention + "_fp";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string ext = System.IO.Path.GetExtension(sfd.FileName); //保存拡張子
                System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Png;
                //形式で場合分け
                switch(ext){
                    case ".png":
                        format = System.Drawing.Imaging.ImageFormat.Png;
                        break;
                    case ".jpg":
                        format = System.Drawing.Imaging.ImageFormat.Jpeg;
                        break;
                    case ".bmp":
                        format = System.Drawing.Imaging.ImageFormat.Bmp;
                        break;
                }
                //指定した形式で特徴点つき画像を保存
                //フォントサイズ, マーカーサイズを一時的に変更
                int fs = _featImage.FontSize;
                int ms = _featImage.MarkSize;

                _featImage.MarkedImage.Save(sfd.FileName, format);
                _featImage.FontSize = fs;
                _featImage.MarkSize = ms;
            }
        }


    }
}
