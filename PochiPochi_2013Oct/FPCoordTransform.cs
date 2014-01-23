using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FeaturePoints
{
    enum EFPCoordinate
    {
        LeftTopZero,
        LeftBottomZero,
        LeftBottomOne
    }

    class FPCoordTransformer
    {
        private int _height;
        private ITransform _trans;

        private FPCoordTransformer()
        {

        }

        public FPCoordTransformer(EFPCoordinate sourceCoord, EFPCoordinate targetCoord, int height)
        {
            _height = height;
            //TODO: ちゃんとした設定
            if (sourceCoord == targetCoord)
                _trans = new KeepCoord();
            else if (sourceCoord == EFPCoordinate.LeftTopZero && targetCoord == EFPCoordinate.LeftBottomZero) _trans = new LeftTopZero2LeftBottomZero();
            else if (sourceCoord == EFPCoordinate.LeftTopZero && targetCoord == EFPCoordinate.LeftBottomOne) _trans = new LeftTopZero2LeftBottomOne();
            else throw new NotImplementedException();
        }

        public FeaturePointsList Transform(FeaturePointsList currentFpList)
        {
            FeaturePointsList newFpList = currentFpList.DeepClone();

            return _trans.Transform(newFpList, _height);
        }

        private interface ITransform
        {
            FeaturePointsList Transform(FeaturePointsList currentFpList, int height);
        }

        private class KeepCoord : ITransform
        {
            public FeaturePointsList Transform(FeaturePointsList currentFpList, int height)
            {
                return currentFpList.DeepClone();
            }
        }

        private class LeftTopZero2LeftBottomZero : ITransform
        {
            public FeaturePointsList Transform(FeaturePointsList currentFpList, int height)
            {
                int n = currentFpList.Count();
                FeaturePointsList transformedList = new FeaturePointsList(n);
                for (int i = 0; i < n; i++)
                {
                    Point origPt = currentFpList[i].Point;
                    Point transPt = new Point(origPt.X, height - origPt.Y - 1);
                    bool flag = currentFpList[i].Valid;
                    transformedList[i] = new FeaturePointWithValidFlag(transPt, flag);
                }

                return transformedList;
            }
        }

        private class LeftTopZero2LeftBottomOne : ITransform
        {
            public FeaturePointsList Transform(FeaturePointsList currentFpList, int height)
            {
                int n = currentFpList.Count();
                FeaturePointsList transformedList = new FeaturePointsList(n);
                for (int i = 0; i < n; i++)
                {
                    Point origPt = currentFpList[i].Point;
                    Point transPt = new Point(origPt.X + 1, height - origPt.Y);
                    bool flag = currentFpList[i].Valid;
                    transformedList[i] = new FeaturePointWithValidFlag(transPt, flag);
                }

                return transformedList;
            }
        }

    }
}
