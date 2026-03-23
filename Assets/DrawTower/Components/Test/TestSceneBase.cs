using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using DrawTower.Token;
using UnityEngine;
using UnityEngine.UI;

namespace DrawTower.Test
{
    /// <summary>
    /// テストシーンで共通的に利用できるボタンバインドやログ出力機能を提供する基底クラス
    /// </summary>
    public class TestSceneBase : AsyncTokenMonoBehaviour
    {
        [SerializeField]
        protected GameObject _btns;
        
        /// <summary>
        /// 指定インデックスのテスト用ボタンを取得する
        /// </summary>
        protected Button GetButton(int index) => _btns?.transform.GetChild(index).GetComponent<Button>();
        
        /// <summary>
        /// 指定したボタンに同期処理（Action）をバインドする  
        /// ボタンがクリックされると指定の処理が実行される
        /// </summary>
        protected void BindButton(int index, Action action)
        {
            GetButton(index).OnClickAsObservable()
            .Subscribe(_ => action?.Invoke())
            .AddTo(this);
        }
        
        /// <summary>
        /// 指定したボタンに非同期処理（UniTask）をバインドする  
        /// ボタンがクリックされるとキャンセルトークン付きで処理が実行される
        /// </summary>
        protected void BindButton(int index, Func<CancellationToken, UniTask> func)
        {
            GetButton(index).OnClickAsObservable()
            .Subscribe(async _ => await func(GetToken()))
            .AddTo(this);
        }

        /// <summary>
        /// 指定されたObservableを購読し、イベント発火時にメッセージをログ出力する 
        /// </summary>
        protected void BindLog(Observable<Unit> observable, string msg)
        {
            observable
                .Subscribe(_ => Debug.Log($"[TestScene] {msg}"))
                .AddTo(this);
        }
    }
}