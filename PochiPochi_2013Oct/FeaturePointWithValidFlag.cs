using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FeaturePoints
{
    /// <summary>
    /// 有効無効フラグ付き特徴点を示す
    /// </summary>
    public class FeaturePointWithValidFlag
    {
        private System.Drawing.Point _point;
        private bool _valid;

        public bool Valid
        {
            get { return _valid; }
            private set { _valid = value; }
        }
        public System.Drawing.Point Point
        {
            get { return _point; }
            private set { _point = value; }
        }

        public FeaturePointWithValidFlag()
        {

        }

        public FeaturePointWithValidFlag(System.Drawing.Point pt, bool isValid)
        {
            _point = pt;
            _valid = isValid;
        }

        public FeaturePointWithValidFlag DeepClone()
        {
            FeaturePointWithValidFlag ret = new FeaturePointWithValidFlag();
            ret._point = _point;
            ret._valid = _valid;

            return ret;
        }
        
    }

    /// <summary>
    /// 有効無効フラグ付き特徴点の列を保持する
    /// 格納された順に特徴点番号となる(インデクサでアクセス)
    /// </summary>
    public class FeaturePointsList
    {
        private List<FeaturePointWithValidFlag> _fps;

        public FeaturePointsList()
        {
            _fps = new List<FeaturePointWithValidFlag>();
        }

        public FeaturePointWithValidFlag this[int pos]
        {
            set { this._fps[pos] = value; }
            get { return this._fps[pos]; }
        }

        public void Push(FeaturePointWithValidFlag fp)
        {
            _fps.Add(fp);
        }

        public FeaturePointWithValidFlag Pop()
        {
            int n = _fps.Count;
            FeaturePointWithValidFlag popfp = _fps.Last().DeepClone();
            _fps.RemoveAt(n - 1);
            return popfp;
        }

        public FeaturePointsList DeepClone(){
            FeaturePointsList ret = new FeaturePointsList();
            foreach (FeaturePointWithValidFlag fp in _fps)
            {
                ret.Push(fp);
            }
            return ret;
        }

        public int Length()
        {
            return _fps.Count;
        }
        public int Count()
        {
            return _fps.Count;
        }
            
    }
}
