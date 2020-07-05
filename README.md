# HPADesign
名前の通りHuman Powered Aircraft(HPA)の設計を行うためのソフトウェアです。
現在は鳥人間のOBを中心に開発が進められています。

## 実行・開発環境
Universal Windows Platform build 16299 (Fall Creators Update version 1709)

動作には最新版のWindows 10が必要です

開発には最新版のVisual Studioが必要です

## Software Document
HPADesignは複数のタブによって構成されています。

### Conceptタブ
このタブでは設計思想に関することを決めていきます。
具体的に、「巡航機（気）速」「仮機体重量」「仮翼分割」がこのタブによって決められます。

### Airfoilタブ
このタブでは飛行機の設計に使用する翼型の情報をソフトウェアに登録します。
また登録した翼型を流体力学解析することもできます。

### Lift and Drag タブ
このタブでは3次元翼を空気力学に解析することで翼に働く揚力と抗力を求めることができます

### Spar and Deflectionタブ
一次構造材であるCFRPパイプの積層構成と「Lift and Drag」タブから求められた空気力学的な力を加えた時にどれぐらい翼がたわむかを計算します
