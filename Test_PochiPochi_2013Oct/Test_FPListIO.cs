using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Drawing;

namespace Test_PochiPochi_2013Oct
{
    [TestClass]
    public class Test_FPListIO
    {
        [TestMethod]
        public void 左上0から左上0の恒等変換のテスト()
        {
            ////テストデータの準備
            //4点, 0...3
            const int Height = 4;
            const int N = 4;
            //Input
            Point[] points = new Point[N] { 
                new Point(0, 0), new Point(1,0), new Point(0,3), new Point(2,2)
            };
            bool[] flags = new bool[N] { true, true, false, true };
            FeaturePoints.FeaturePointsList fpList = new FeaturePoints.FeaturePointsList();
            for (int i = 0; i < N; i++)
            {
                fpList.Push(new FeaturePoints.FeaturePointWithValidFlag(points[i], flags[i]));
            }
            //Output
            Point[] true_points = new Point[N]{
                new Point(0, 0), new Point(1, 0), new Point(0,3), new Point(2,2)
            };
            bool[] true_flags = new bool[N] { true, true, false, true };

            ////テスト
            FeaturePoints.FPCoordTransformer fpCoordTransformer;
            fpCoordTransformer = new FeaturePoints.FPCoordTransformer(FeaturePoints.EFPCoordinate.LeftTopZero, FeaturePoints.EFPCoordinate.LeftTopZero, Height);
            FeaturePoints.FeaturePointsList transedFPList = fpCoordTransformer.Transform(fpList);

            for (int i = 0; i < N; i++)
            {
                //Assert.AreEqual(true_points[i], transedFPList[i].Point);
                //Assert.AreEqual(true_flags[i], transedFPList[i].Valid);
                Assert.AreEqual<FeaturePoints.FeaturePointWithValidFlag>(new FeaturePoints.FeaturePointWithValidFlag(true_points[i], true_flags[i]), transedFPList[i]);
            }


        }

        [TestMethod]
        public void 左上0から左下0の変換のテスト()
        {
            ////テストデータの準備
            //4点, 0...3
            const int Height = 4;
            const int N = 4;
            //Input
            Point[] points = new Point[N] { 
                new Point(0, 0), new Point(1,0), new Point(0,3), new Point(2,2)
            };
            bool[] flags = new bool[N]{true, true, false, true};
            FeaturePoints.FeaturePointsList fpList = new FeaturePoints.FeaturePointsList();
            for (int i = 0; i < N; i++)
            {
                fpList.Push(new FeaturePoints.FeaturePointWithValidFlag(points[i], flags[i]));
            }
            //Output
            Point[] true_points = new Point[N]{
                new Point(0, 3), new Point(1, 3), new Point(0,0), new Point(2, 1)
            };
            bool[] true_flags = new bool[N] { true, true, false, true };

            ////テスト
            FeaturePoints.FPCoordTransformer fpCoordTransformer;
            fpCoordTransformer = new FeaturePoints.FPCoordTransformer(FeaturePoints.EFPCoordinate.LeftTopZero, FeaturePoints.EFPCoordinate.LeftBottomZero, Height);
            FeaturePoints.FeaturePointsList transedFPList = fpCoordTransformer.Transform(fpList);

            for (int i = 0; i < N; i++)
            {
                Assert.AreEqual(true_points[i], transedFPList[i].Point);
                Assert.AreEqual(true_flags[i], transedFPList[i].Valid);
            }
                

        }


        [TestMethod]
        public void 左上0から左下1の変換のテスト()
        {
            ////テストデータの準備
            //4点, 0...3
            const int Height = 4;
            const int N = 4;
            //Input
            Point[] points = new Point[N] { 
                new Point(0, 0), new Point(1,0), new Point(0,3), new Point(2,2)
            };
            bool[] flags = new bool[N] { true, true, false, true };
            FeaturePoints.FeaturePointsList fpList = new FeaturePoints.FeaturePointsList();
            for (int i = 0; i < N; i++)
            {
                fpList.Push(new FeaturePoints.FeaturePointWithValidFlag(points[i], flags[i]));
            }
            //Output
            Point[] true_points = new Point[N]{
                new Point(1, 4), new Point(2, 4), new Point(1,1), new Point(3,2)
            };
            bool[] true_flags = new bool[N] { true, true, false, true };

            ////テスト
            FeaturePoints.FPCoordTransformer fpCoordTransformer;
            fpCoordTransformer = new FeaturePoints.FPCoordTransformer(FeaturePoints.EFPCoordinate.LeftTopZero, FeaturePoints.EFPCoordinate.LeftBottomOne, Height);
            FeaturePoints.FeaturePointsList transedFPList = fpCoordTransformer.Transform(fpList);

            for (int i = 0; i < N; i++)
            {
                Assert.AreEqual(true_points[i], transedFPList[i].Point);
                Assert.AreEqual(true_flags[i], transedFPList[i].Valid);
            }


        }
    }
}
