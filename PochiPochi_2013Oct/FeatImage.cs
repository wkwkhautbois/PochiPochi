using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using FeaturePoints;

namespace FeatImage
{
    [System.Runtime.InteropServices.GuidAttribute("EAF0D84A-0832-418A-8379-F872A4D1FA2D")]
    public partial class FeatImage : UserControl
    {
        //--------列挙型定義---------
        public enum ECoordinateOption
        {
            LeftTopZero,
            LeftBottomZero,
            LeftBottomOne
        }

        public enum EMouseMode
        {
            AddAndErase,
            Move
        }

        public enum EFeaturePointCondition
        {
            Visible,
            Invisible
        }


        //-----フィールド変数宣言-----
        private Bitmap _origImg;    //元の画像
        private Bitmap _fpImg;      //特徴点が描画された画像
        FeaturePointsList _fpList = new FeaturePointsList();
        private double _imgScale;
        private ECoordinateOption _coordinateSystem;
        private EMouseMode _mouseMode;
        private int _markSize;
        private int _fontSize;
        //private System.Drawing.Drawing2D.InterpolationMode _interpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
        //DrawImage()が遅いためしばらくは使わない, 自分でNN,Bilinearを実装したら利用するかも
        //private EFeaturePointCondition? _currentFPCondition;
        private int? _selectingFPNumber;
        private Point _lastClickMousePoint;   //最後にクリックされたマウス座標
        private FeaturePointWithValidFlag _lastClickFP; //最後にクリックされたマウス座標に対応する(近傍の)特徴点座標

        //-------プロパティ宣言-------
        public System.Windows.Forms.PictureBox _PictureBox
        {
            get { return _pictureBox; }
        }

        public double ImageScale
        {
            get { return _imgScale; }
            set
            {
                _imgScale = value;
                UpdateImage();
            }
        }

        public ECoordinateOption CoordinateSystem
        {
            get { return _coordinateSystem; }
            set
            {
                _coordinateSystem = value;
                if (_origImg != null)
                    UpdateImage();
            }
        }

        public EMouseMode MouseMode
        {
            get { return _mouseMode; }
            set
            {
                _mouseMode = value;
                UpdateImage();
            }
        }

        public int MarkSize
        {
            get { return _markSize; }
            set
            {
                _markSize = value;
                UpdateImage();
            }
        }

        public int FontSize
        {
            get { return _fontSize; }
            set
            {
                _fontSize = value;
                UpdateImage();
            }
        }

        //現在選択中の点が見えているか見えていないか
        public EFeaturePointCondition? CurrentFPCondition
        {
            get
            {
                if (SelectingFPNumber == null || FeaturePoints.Count() == 0)
                    return null;
                if (FeaturePoints[(int)SelectingFPNumber].Valid)
                    return EFeaturePointCondition.Visible;
                else
                    return EFeaturePointCondition.Invisible;
            }
            set
            {
                if (SelectingFPNumber != null)
                {
                    if (value == EFeaturePointCondition.Visible)
                        FeaturePoints[(int)SelectingFPNumber] = new FeaturePointWithValidFlag(FeaturePoints[(int)SelectingFPNumber].Point, true);
                    else if (value == EFeaturePointCondition.Invisible)
                        FeaturePoints[(int)SelectingFPNumber] = new FeaturePointWithValidFlag(FeaturePoints[(int)SelectingFPNumber].Point, false);
                    //if (_currentFPCondition == EFeaturePointCondition.Visible) _fpList[(int)SelectingFPNumber] = new FeaturePointWithValidFlag(_fpList[(int)SelectingFPNumber].Point, true);
                    //else if (_currentFPCondition == EFeaturePointCondition.Invisible) _fpList[(int)SelectingFPNumber] = new FeaturePointWithValidFlag(_fpList[(int)SelectingFPNumber].Point, false);
                    UpdateImage();
                }

            }

        }

        //選択されている特徴点番号[nullable](選択されている&&ドラッグ中=動かされている)
        public int? SelectingFPNumber
        {
            get { return _selectingFPNumber; }
            set
            {
                _selectingFPNumber = value;
                UpdateImage();
            }
        }
        //動かされている特徴点番号[nullable](選択されている&&ドラッグ中=動かされている)
        public int? MovingFPNumber
        {
            get;
            set;
        }

        
        public Bitmap Image
        {
            get { return _origImg; }
            set
            {
                _origImg = value;
                UpdateImage();
            }
        }
        public Bitmap MarkedImage
        {
            get {
                SelectingFPNumber = null;
                return _fpImg;
            }
        }

        public FeaturePointsList FeaturePoints
        {
            get { return _fpList; }
            set
            {
                _fpList = value.DeepClone();
                UpdateImage();
            }
        }


        //---------関数定義---------

        public FeatImage()
        {
            InitializeComponent();
        }

        public void UpdateImage()
        {
            if (_origImg == null) return;
            if (_pictureBox.Image != null) _pictureBox.Image.Dispose();

            _fpImg = DrawFeaturePoints(_origImg);


            //_pictureBox.Image = MakeScaledFeaturePointsImage(_fpImg);
            int newWidth = (int)(_fpImg.Width * _imgScale);
            int newHeight = (int)(_fpImg.Height * _imgScale);
            _pictureBox.Size = new Size(newWidth, newHeight);
            _pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            _pictureBox.Image = _fpImg;
        }

        private Bitmap DrawFeaturePoints(Bitmap src)
        {
            //描画前の状態の深いコピー
            Bitmap dst = new Bitmap(src, src.Width, src.Height);

            //描画処理
            int r = _markSize;
            int fontsize = _fontSize;


            using (Graphics g = Graphics.FromImage(dst))
            using (Font font = new Font("MS UI Gothic", fontsize))
            using (Pen pen_green = new Pen(Color.FromArgb(80, 0, 255, 0)))
            using (Pen pen_blue = new Pen(Color.FromArgb(200, 0, 0, 255)))
            {
                Pen pen_mark;
                int n = _fpList.Count();

                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;

                for (int i = 0; i < n; i++)
                {
                    Point center = _fpList[i].Point;
                    Point lt = new Point(center.X - r, center.Y - r);
                    Point rt = new Point(center.X + r, center.Y - r);
                    Point lb = new Point(center.X - r, center.Y + r);
                    Point rb = new Point(center.X + r, center.Y + r);

                    if (i == SelectingFPNumber)
                    {
                        pen_mark = pen_green;
                    }
                    else
                    {
                        pen_mark = pen_blue;
                    }

                    //有効(見える)特徴点かどうかで描画記号を変更
                    if (_fpList[i].Valid)
                    {
                        //×印
                        g.DrawLine(pen_mark, lt, rb);
                        g.DrawLine(pen_mark, lb, rt);
                        g.DrawString(i.ToString(), font, Brushes.Red, lt.X - fontsize, lt.Y - fontsize);
                    }
                    else
                    {
                        g.DrawEllipse(pen_mark, center.X - r, center.Y - r, 2 * r, 2 * r);
                        g.DrawString(i.ToString(), font, Brushes.Red, lt.X - fontsize, lt.Y - fontsize);
                    }
                }
            }

            return dst;
        }

        //Graphics.DrawImage()が遅いので使わない
        private Bitmap MakeScaledFeaturePointsImage(Bitmap src)
        {
            int newWidth = (int)(src.Width * _imgScale);
            int newHeight = (int)(src.Height * _imgScale);

            Bitmap dst = new Bitmap(src, newWidth, newHeight);

            using (Graphics g = Graphics.FromImage(dst))
            {
                //g.InterpolationMode = _interpolationMode;
                //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.DrawImage(src, 0, 0, newWidth, newHeight);
            }

            return dst;
        }

        private void PushFeaturePoint(FeaturePointWithValidFlag fp)
        {
            _fpList.Push(fp);
            UpdateImage();
        }

        private FeaturePointWithValidFlag PopFeaturePoint()
        {
            FeaturePointWithValidFlag ret = _fpList.Pop();
            UpdateImage();
            return ret;
        }

        //クリックされた座標(Scaled)を元の画像座標に変換
        private Point UnscalizePoint(Point pt)
        {
            pt.X = (int)(pt.X / _imgScale);
            pt.Y = (int)(pt.Y / _imgScale);
            return pt;
        }

        private void _pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            switch (_mouseMode)
            {
                case EMouseMode.AddAndErase:
                    switch (e.Button)
                    {
                        case System.Windows.Forms.MouseButtons.Left:
                            Point pt = UnscalizePoint(e.Location);
                            PushFeaturePoint(new FeaturePointWithValidFlag(pt, true));
                            break;
                        case System.Windows.Forms.MouseButtons.Right:
                            if (_fpList.Count() > 0) PopFeaturePoint();
                            break;
                    }
                    int k = FeaturePoints.Count();
                    if (k > 0)
                        SelectingFPNumber = _fpList.Count() - 1;

                    break;
                case EMouseMode.Move:
                    int? pos = SearchNeighborFP(UnscalizePoint(e.Location));
                    MovingFPNumber = pos;
                    SelectingFPNumber = pos;


                    if (pos != null)
                    {
                        //クリックされた座標と近傍特徴点座標を保持
                        _lastClickMousePoint = new Point(e.X, e.Y);
                        _lastClickFP = FeaturePoints[(int)pos].DeepClone();

                        UpdateImage();
                    }

                    break;

            }
        }

        private void _pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            MovingFPNumber = null;
        }

        private void _pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            //特徴点の移動中でない場合は何も処理しない
            if (MovingFPNumber == null) return;

            //最後にクリックされたマウス座標との差分だけ特徴点を元の場所から移動
            int dx = (e.X - _lastClickMousePoint.X) / (int)ImageScale;
            int dy = (e.Y - _lastClickMousePoint.Y) / (int)ImageScale;

            FeaturePoints[MovingFPNumber.Value] = new FeaturePointWithValidFlag(new Point(_lastClickFP.Point.X + dx, _lastClickFP.Point.Y + dy), _lastClickFP.Valid);

            UpdateImage();
        }

        private int? SearchNeighborFP(Point pt)
        {
            if (_fpList.Count() == 0) return null;


            int? nearestFPNum = null;
            int n = FeaturePoints.Count();
            int r = _markSize;

            int? dx_like_abs = null;
            int? dy_like_abs = null;

            for (int i = 0; i < n; i++)
            {
                int dx = FeaturePoints[i].Point.X - pt.X;
                int dy = FeaturePoints[i].Point.Y - pt.Y;
                int dx_abs = Math.Abs(dx);
                int dy_abs = Math.Abs(dy);
                //各特徴点が、ptの一辺が2*r+1の正方形の内側に入っている点かどうか判定する
                if (dx_abs > r || dy_abs > r) continue;

                ////初めての最近傍特徴点ならばそのまま候補として採用
                if (nearestFPNum == null)
                {
                    nearestFPNum = i;
                }
                else if (!(dx_abs + dy_abs > dx_like_abs + dy_like_abs))
                {   //これまでの候補よりマンハッタン距離が近ければ採用
                    nearestFPNum = i;
                    dx_like_abs = Math.Abs(FeaturePoints[(int)nearestFPNum].Point.X - pt.X);
                    dy_like_abs = Math.Abs(FeaturePoints[(int)nearestFPNum].Point.Y - pt.Y);
                }
            }


            return nearestFPNum;
        }

    }
}
