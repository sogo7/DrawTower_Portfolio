using Cysharp.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using System.Threading;

namespace DrawTower.Ui
{
	public class Progress : MonoBehaviour, IProgress
	{
		[SerializeField]
		private RectTransform _root;
		private Tween _tween;
		
		public async UniTask ShowAnimAsync(CancellationToken ct)
		{					
			gameObject.SetActive(true);	
			
			_tween = _root.DORotate(new Vector3(0, 0, -360), 2f, RotateMode.FastBeyond360)
				.SetEase(Ease.Linear)
				.SetLoops(-1, LoopType.Incremental);
				
			await _tween.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, ct);
		}
		
		public void Hide()
		{
			gameObject.SetActive(false);
			_tween?.Kill();
		    _root.rotation = Quaternion.identity;
		}
		
		private void OnDestroy()
		{
			_tween?.Kill();
		}
	}
}

