# DrawTower Portfolio

## 概要

本 repository は、App Store で公開している Unity 製アプリのうち、設計・実装方針・使用技術を確認しやすい部分を抜粋したポートフォリオ版です。

完成版のゲーム体験そのものは App Store 公開版で確認できるため、こちらでは本番向けのサーバー連携や公開情報に紐づく要素を整理し、コードレビューに向いた構成に絞っています。

本アプリは個人開発で、企画、設計、実装、UI構築、iOS 向け調整、App Store 公開対応まで一通り担当しています。

## この repository の目的

この repository では、完成版アプリの全機能を見せることではなく、以下の点を確認していただくことを目的としています。

- Unity プロジェクト全体の責務分離
- Presenter / Logic / UI / Object / Model の構成
- Zenject を用いた依存注入
- UniTask / R3 を用いた非同期処理とイベント駆動
- Unity から iOS ネイティブ機能を呼び出す実装

## ポートフォリオ版で残しているもの

- `OfflineGame` を中心とした単一シーン構成
- オフラインゲームループの実装
- Presenter 主導の画面制御
- ゲームロジック、UI、オブジェクト、モデルの分離
- iOS ネイティブ連携コード
- UI / Logic を単体で確認するための `Test Scene`
- コンポーネント単位で確認するためのテスト用コードの一部

## ポートフォリオ版で削除・簡略化しているもの

- 本番サーバーに依存する処理
- PlayFab / Photon を利用したオンライン導線
- Title / Home / Result / OnlineGame シーン
- 本番用の識別子や公開アプリ固有の設定値
- 本番サービス接続を前提としたテストコード

## 見てほしいポイント

- `Presenter` に画面進行と UI 制御を集約し、個別機能を `Logic` や `Ui` に分離している点
- シーン遷移や機能呼び出しをインターフェース経由で扱い、差し替えしやすくしている点
- 入力、描画、ブロック生成、落下制御、演出がそれぞれ別コンポーネントに分かれている点
- Unity C# 側と iOS ネイティブコードを疎結合に接続している点
- UI / Logic の各コンポーネントに `Test Scene` を用意し、機能単位で挙動を確認しやすくしている点

## 使用技術

### このポートフォリオ版で確認できる技術

- Unity
- C#
- Zenject
- UniTask
- R3
- DOTween
- iOS ネイティブプラグイン連携（Objective-C++）

### 公開版アプリで利用している技術

- Photon Fusion
- PlayFab
- PlayFab CloudScript
- オンライン対戦機能
- App Store 向け iOS リリース対応

## 設計方針

このプロジェクトでは、シーン上の処理を 1 つの巨大な MonoBehaviour に集約せず、役割ごとに分割することを重視しています。

- `Presenter`
  シーン全体の進行、UI の切り替え、各機能の接続を担当します。
- `Logic`
  ゲーム進行や入力処理、演出制御などの再利用可能なロジックを担当します。
- `Ui`
  UI 表示、アニメーション、ボタン入力など見た目に近い責務を担当します。
- `Object`
  ステージ上に存在するゲームオブジェクトを担当します。
- `Model`
  ゲーム内で扱うデータ構造を担当します。

現在のポートフォリオ版は、サーバー依存なしで実装方針を確認しやすい `OfflineGame` に絞っています。

また、本番シーンだけで確認するのではなく、UI / Logic の各機能を単体で確認できる `Test Scene` を用意しており、責務分離だけでなく検証しやすさも意識して構成しています。

## 主な確認対象ファイル

- `Assets/DrawTower/Scenes/OfflineGame.unity`
- `Assets/DrawTower/Components/Presenter/OfflineGamePresenter/OfflineGamePresenter.cs`
- `Assets/DrawTower/Components/Logic/LogicInstaller.cs`
- `Assets/DrawTower/Components/Logic/SceneLoader/SceneLoader.cs`
- `Assets/DrawTower/Components/Logic/DrawLine/DrawLine.cs`
- `Assets/DrawTower/Components/Logic/LineBlockCreator/LineBlockCreator.cs`
- `Assets/DrawTower/Components/Logic/BlockController/BlockController.cs`
- `Assets/DrawTower/Components/Logic/BlockMonitor/BlockMonitor.cs`
- `Assets/DrawTower/Components/Logic/AudioPlayer/AudioPlayer.cs`
- `Assets/DrawTower/Components/Logic/HapticPlayer/HapticPlayer.cs`
- `Assets/DrawTower/Components/Logic/KeychainManager/KeychainManager.cs`
- `Assets/Plugins/iOS/KeychainManager.mm`
- `Assets/Plugins/iOS/HapticFeedback.mm`

## ディレクトリ構成

```text
Assets/DrawTower/
  Components/
    Presenter/
    Logic/
    Ui/
    Object/
    Model/
    Test/
  Scenes/
    OfflineGame.unity
```

## 確認方法

1. Unity で project を開く
2. `Assets/DrawTower/Scenes/OfflineGame.unity` を開く
3. Play して挙動を確認する

ポートフォリオ版は単一シーン構成のため、ゲーム終了時は同じシーンを再読込する形にしています。  
これは完成版の導線を再現するためではなく、オフラインゲーム部分の設計と実装を単体で確認しやすくするためです。

## 補足

- 本 repository は公開版アプリそのものではなく、選考用に整理した抜粋版です。
- 完成版では、これ以外にオンライン対戦、サーバー連携、公開版向けの導線や設定が存在します。
- 現在の repository は、ゲーム内容よりも設計と実装スタイルの確認に重点を置いています。
