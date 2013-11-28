PochiPochi - 特徴点手動抽出ソフト
==================

これは画像中の特徴点(キーポイント)を手動で設定する際に用いるソフトウェアです。

##使い方
読み込んだ画像に対して、マウスを使って点(座標)を指定していきます。

####モード
追加削除モードと移動モードがあり、新たな点の追加だけでなく、すでに指定された点(座標)をドラッグアンドドロップにより移動させることもできます。

####座標系
座標系は、よくある画像座標系(左上が原点)に加えて、MATLABなどで使われる左下原点の座標系にも対応しています。

####書き込み・読み込み
指定した特徴点座標の読み込み・書き込み、特徴点付き画像の書き出し、すでに書き出された座標の読み込みが可能。

####特徴点の状態(ダミーデータの挿入)
3枚以上の画像を対象として対応する特徴点を指定していると、
全ての画像ではなく一部の(複数の)画像のみに写っている点というものが存在します。
その場合、全画像間の対応関係を保つために、
見えていない点にも何らかの情報を与えたくなる場合があります。
このソフトでは、ダミーデータを挿入することで対応が可能です。
ダミーデータを挿入する時には、Modeを「移動」にした後に、特徴点の状態ラジオボタンを「見えない」にセットしてください。
点の色が変わります。

書き出されるCSV(カンマ区切り)データのうち、3列目がダミーかどうかの識別フラグとなります。

##動作環境(開発環境)
Visual C# 2012 にて開発を行っています。

.NET Framework 3.5

外部のライブラリは使用していません。ソースコードだけコンパイルすれば動きます。

###ライセンス(License)
このソフトウェアは、MPL(BSD-Like)ライセンスで公開します。

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
