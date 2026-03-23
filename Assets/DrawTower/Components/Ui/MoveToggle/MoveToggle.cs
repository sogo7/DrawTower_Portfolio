using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace DrawTower.Ui
{
	public class MoveToggle : ScaleAnimToggle
	{
		[SerializeField]
		private RectTransform _rect;
		
		private const float LEFT_POS_X = -1500f;
		private const float RIGHT_POS_X = 1500f;
		private float _duration = 0.5f;
		private Tween _tween;
		private bool _isMoving = false;
		
		public async UniTask MoveLeftAnimAsync(CancellationToken ct)
		{
            if (_isMoving) return;
            _isMoving = true;
    
		    _tween = _rect.DOAnchorPosX(LEFT_POS_X, _duration)
		    	.SetEase(Ease.OutCubic);
			await _tween.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, ct);
			Hide();
			
			_isMoving = false;
		}
		
		public async UniTask MoveCenterAnimAsync(CancellationToken ct)
		{
            if (_isMoving) return;
            _isMoving = true;
            
			Show();
		    _tween = _rect.DOAnchorPosX(0f, _duration)
		    	.SetEase(Ease.OutCubic);
			await _tween.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, ct);
			
			_isMoving = false;
		}
		
		public async UniTask MoveRightAnimAsync(CancellationToken ct)
		{
            if (_isMoving) return;
            _isMoving = true;
            
		    _tween = _rect.DOAnchorPosX(RIGHT_POS_X, _duration)
		    	.SetEase(Ease.OutCubic);
			await _tween.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, ct);
			Hide();
			
			_isMoving = false;
		}
		
		protected override void OnDestroy()
		{
			_tween?.Kill();
			base.OnDestroy();
		}
	}
}