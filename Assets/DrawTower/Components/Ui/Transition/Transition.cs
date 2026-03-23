using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace DrawTower.Ui
{
	public class Transition : MonoBehaviour, ITransition
	{
		[SerializeField]
        private CanvasGroup _canvasGroup;
        
        private float _duration = 0.5f;
		
		public async UniTask FadeInAsync(CancellationToken ct)
		{
			_canvasGroup.alpha = 0f;
			gameObject.SetActive(true);
		    await _canvasGroup.DOFade(1f, _duration).WithCancellation(ct);
		}
		
		public async UniTask FadeOutAsync(CancellationToken ct)
		{
			_canvasGroup.alpha = 1f;
			gameObject.SetActive(true);
		    await _canvasGroup.DOFade(0f, _duration).WithCancellation(ct);
		    gameObject.SetActive(false);
		}
	}
}
