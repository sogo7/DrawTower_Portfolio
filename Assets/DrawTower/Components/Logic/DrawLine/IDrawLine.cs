using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace DrawTower.Logic
{
    public interface IDrawLine
    {
        /// <summary>
        /// 描画中かどうかのフラグ
        /// </summary>
        bool IsDrawing();
        
        /// <summary>
        /// 描画に必要な設定をする
        /// </summary>
        void Setup(RectTransform area, Color color);

        /// <summary>
        /// 描画完了まで非同期で待機（3点未満なら再試行）
        /// </summary>
        UniTask<IReadOnlyList<Vector3>> StartDrawingAsync();

        /// <summary>
		/// 現在の描画を強制停止する
		/// </summary>
		void Cancel();
		
		/// <summary>
        /// 受信した線データを UI に再描画する（同期用）
        /// </summary>
        void DrawReceivedLine(IReadOnlyList<Vector3> points);
    }
}