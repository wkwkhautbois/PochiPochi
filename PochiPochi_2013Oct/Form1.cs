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
            _radioButton_coordLeftBottomOne.PerformClick();
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
            FeaturePoints.FeaturePointsList fpList_lt = _featImage.FeaturePoints;

            //指定の座標系に変換
            FeaturePoints.FeaturePointsList fpList = fpList_lt.DeepClone(); //変換後の特徴点座標, 要素数確保のため、ダミーとしてfpList_ltの深いコピーを入れておく
            FeatImage.FeatImage.ECoordinateOption coordOpt = CheckCoordinateOption();
            
            int n = fpList_lt.Count();
            for (int i = 0; i < n; i++)
            {
                Point pt;
                int imgHeight = _featImage.Image.Height;
                if (coordOpt == FeatImage.FeatImage.ECoordinateOption.LeftTopZero) pt = fpList_lt[i].Point;
                else if (coordOpt == FeatImage.FeatImage.ECoordinateOption.LeftBottomZero) pt = new Point(fpList_lt[i].Point.X, imgHeight - fpList_lt[i].Point.Y - 1);
                else if (coordOpt == FeatImage.FeatImage.ECoordinateOption.LeftBottomOne) pt = new Point(fpList_lt[i].Point.X + 1, imgHeight - fpList_lt[i].Point.Y);
                else
                {
                    MessageBox.Show("座標系指定エラー@_button_saveFPs_Click(object sender, EventArgs e):\n左上原点として扱います", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    pt = fpList_lt[i].Point;
                }

                fpList[i] = new FeaturePoints.FeaturePointWithValidFlag(pt, fpList_lt[i].Valid);

            }
            SaveFPsAsCSV(sfd.FileName, fpList);
        }
        FeatImage.FeatImage.ECoordinateOption CheckCoordinateOption()
        {
            if (_radioButton_coordLeftBottomOne.Checked) return FeatImage.FeatImage.ECoordinateOption.LeftBottomOne;
            else if (_radioButton_coordLeftBottomZero.Checked) return FeatImage.FeatImage.ECoordinateOption.LeftBottomZero;
            else if (_radioButton_coordLeftTopZero.Checked) return FeatImage.FeatImage.ECoordinateOption.LeftTopZero;
            else
            {
                MessageBox.Show("座標系指定エラー@CheckCoordinateOption():\n左上原点として扱います", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return FeatImage.FeatImage.ECoordinateOption.LeftTopZero;
            }

            
        }

        /// <summary>
        /// 有効無効フラグつきの特徴点情報をCSVファイルとして書き出す関数
        /// 各行に、"x座標", "y座標", "フラグ(1が有効,0が無効)" が書き込まれる
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="data"></param>
        private void SaveFPsAsCSV(string filename, FeaturePoints.FeaturePointsList data)
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(filename))
            {
                int n = data.Count();
                for (int i = 0; i < n; i++)
                {
                    string flagValid;
                    if (data[i].Valid) { flagValid = "1"; }
                    else { flagValid = "0"; }

                    sw.WriteLine(data[i].Point.X.ToString() + "," + data[i].Point.Y.ToString() + "," + flagValid);
                }
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
            FeatImage.FeatImage.ECoordinateOption coordOpt = CheckCoordinateOption();

            int n = fpList_orig.Count();
            for (int i = 0; i < n; i++)
            {
                Point pt;
                int imgHeight = _featImage.Image.Height;
                if (coordOpt == FeatImage.FeatImage.ECoordinateOption.LeftTopZero) pt = fpList_orig[i].Point;
                else if (coordOpt == FeatImage.FeatImage.ECoordinateOption.LeftBottomZero) pt = new Point(fpList_orig[i].Point.X, imgHeight - fpList_orig[i].Point.Y - 1);
                else if (coordOpt == FeatImage.FeatImage.ECoordinateOption.LeftBottomOne) pt = new Point(fpList_orig[i].Point.X - 1, imgHeight - fpList_orig[i].Point.Y);
                else
                {
                    MessageBox.Show("座標系指定エラー@_button_saveFPs_Click(object sender, EventArgs e):\n左上原点として扱います", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    pt = fpList_orig[i].Point;
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
                //_featImage.FontSize = 16;
                //_featImage.MarkSize = 16;
                _featImage.MarkedImage.Save(sfd.FileName, format);
                _featImage.FontSize = fs;
                _featImage.MarkSize = ms;
            }
        }


    }
}
