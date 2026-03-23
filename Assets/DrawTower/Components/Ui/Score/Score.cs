using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace DrawTower.Ui
{
	public class Score : BaseUi, IScore
	{
		[SerializeField]
		private TextMeshProUGUI _text;
		
		private Tween _tween;
		
		private float _startValue = 0f;
		
		public async UniTask ShowAndAnimAsync(CancellationToken ct)
        {
			_text.gameObject.transform.localScale = Vector3.zero;
            Show();

            _tween = _text.gameObject.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
            await _tween.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, ct);
        }
        
        public async UniTask HideAndAnimAsync(CancellationToken ct)
        {
            _tween = _text.gameObject.transform.DOScale(0f, 0.5f).SetEase(Ease.OutBack);
            await _tween.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, ct);
            Hide();
        }

		/// <summary>
		/// スロット風にスコアを更新
		/// </summary>
		public async UniTask UpdateTextAsync(float score, float duration, CancellationToken ct)
		{
			Clear();
			float currentValue = _startValue;

			// 数値を滑らかに変化させる
			_tween = DOTween.To(
				() => currentValue,
				x =>
				{
					currentValue = x;
					_text.text = $"{currentValue:F1}m";
				},
				score,
				duration
			)
			.SetEase(Ease.OutCubic);
			
			_startValue = score;

			await _tween.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, ct);
		}
		
		public void Clear()
        {
            _tween?.Kill();
        }
		
		protected override void OnDestroy()
		{
			Clear();
			base.OnDestroy();
		}
	}
}
