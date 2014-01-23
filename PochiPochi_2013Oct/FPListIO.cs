using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Point = System.Drawing.Point;

namespace FeaturePoints
{
    enum ESaveFormat
    {
        CSV_XYF
    }

    

    class FPListSaverFactory
    {
        public static FPListSaver Create(ESaveFormat eformat){
            FPListSaver fpListSaver;

            switch (eformat)
            {
                case ESaveFormat.CSV_XYF:
                    fpListSaver = new FPListSaverCSV_XYF();
                    break;
                default:
                    throw new NotImplementedException();
            }

            return fpListSaver;
        }
    }

    abstract class FPListSaver
    {
        public abstract void Save(string filename, FeaturePointsList FPList);      
    }

    class FPListSaverCSV_XYF : FPListSaver
    {
        /// <summary>
        /// 有効無効フラグつきの特徴点情報をCSVファイルとして書き出す
        /// 各行に、"x座標", "y座標", "フラグ(1が有効,0が無効)" が書き込まれる
        /// </summary>
        /// <param name="filename">保存するファイル名</param>
        /// <param name="FPList">保存される特徴点</param>
        public override void Save(string filename, FeaturePointsList FPList)
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(filename))
            {
                int n = FPList.Count();
                for (int i = 0; i < n; i++)
                {
                    string flagValid;
                    if (FPList[i].Valid) { flagValid = "1"; }
                    else { flagValid = "0"; }

                    sw.WriteLine(FPList[i].Point.X.ToString() + "," + FPList[i].Point.Y.ToString() + "," + flagValid);
                }
            }
        }

    }
}
