using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using R3;
using TMPro;
using UnityEngine;

namespace DrawTower.Ui
{
	public class TimerUi : BaseUi, ITimerUi
	{
		[SerializeField]
		private TextMeshProUGUI _text;
		
		private Tween _tween;
		
		private readonly Subject<Unit> _onTimeUp = new();
		
		public Observable<Unit> OnTimeUp() => _onTimeUp;
		
		public async UniTask StartTimerAsync(int seconds)
		{
			var token = GetToken();
			Show();

			try
			{
				for (int i = seconds; i > 0; i--)
				{
					await PlayTickAnimAsync(i, token);
				}

				_onTimeUp.OnNext(Unit.Default);
			}
			catch (OperationCanceledException){ }
			finally
			{
				Hide();
			}
		}
		
		private async UniTask PlayTickAnimAsync(int seconds, CancellationToken ct)
		{
			_tween?.Kill();

			bool isLast = seconds <= 5;

			float outDuration = 0.2f;
			float inDuration  = isLast ? 0.28f : 0.16f;

			float outScale = isLast ? 0.75f : 0.97f;
			float inScale  = isLast ? 1.55f : 1.07f;

			_tween = DOTween.Sequence()
				.Append(
					_text.DOFade(0f, outDuration)
				)
				.Join(
					_text.transform
						.DOScale(outScale, outDuration)
						.SetEase(Ease.InQuad)
				)
				.AppendCallback(() => 
				{
				    _text.text = seconds.ToString();
				})
				.AppendInterval(0.1f)
				.AppendCallback(() =>
				{
					_text.alpha = 0f;
					_text.transform.localScale = Vector3.one * inScale;
					_text.color = isLast ? Color.red : Color.white;
				})
				.Append(
					_text.DOFade(1f, inDuration)
				)
				.Join(
					_text.transform
						.DOScale(1f, inDuration)
						.SetEase(isLast ? Ease.OutBack : Ease.OutQuad)
				)
				.AppendInterval(
					Mathf.Max(0f, 1f - (outDuration + inDuration + 0.1f))
				);

			await _tween.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, ct);
		}
		
		public void Clear()
		{
		    Hide();
		    _text.text = "";
		    Dispose();
		}
		
		protected override void OnDestroy()
		{
			Clear();
			base.OnDestroy();
		}
	}
}
