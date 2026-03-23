using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DrawTower.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using R3;

namespace DrawTower.Ui
{
	public class VersusUi : BaseUi, IVersusUi
	{
		[SerializeField]
		private RectTransform _myRect;
		
		[SerializeField]
		private RectTransform _vsRect;
		
		[SerializeField]
		private RectTransform _opponentRect;
		
		[SerializeField]
		private TextMeshProUGUI _myName;
		
		[SerializeField]
		private TextMeshProUGUI _opponentName;
		
		[SerializeField]
		private Image _myBg;
		
		[SerializeField]
		private Image _opponentBg;
		
		private Sequence _seq;
		
		private const float LEFT_POS_X = -1500f;
		private const float RIGHT_POS_X = 1500f;
		
		public void SetMyData(Player player, string name)
		{
		    _myName.text = name;
		    _myBg.color = player.GetColor();
		}
		
		public void SetOpponentData(Player opponent, string name)
		{
		    _opponentName.text = name;
		    _opponentBg.color = opponent.GetColor();
		}
		
		public async UniTask ShowAndAnimAsync(CancellationToken ct)
		{
			Clear();
			Show();
			
			_seq = DOTween.Sequence()
				.Append(_myRect.DOAnchorPosX(0f, 0.5f).SetEase(Ease.OutCubic))
				.Append(_vsRect.DOScale(1f, 0.5f).SetEase(Ease.OutCubic))
				.Join(_vsRect.DOLocalRotate(Vector3.zero, 0.5f).SetEase(Ease.OutCubic))
				.Append(_opponentRect.DOAnchorPosX(0f, 0.5f).SetEase(Ease.OutCubic))
				.AppendInterval(1f)			
				.Append(_myRect.DOAnchorPosX(LEFT_POS_X, 0.5f).SetEase(Ease.OutCubic))
				.Join(_vsRect.DOScale(0f, 0.5f).SetEase(Ease.OutCubic))
				.Join(_vsRect.DOLocalRotate(new Vector3(0, 0, 90f), 0.5f).SetEase(Ease.OutCubic))
				.Join(_opponentRect.DOAnchorPosX(RIGHT_POS_X, 0.5f).SetEase(Ease.OutCubic))
				.AppendCallback(Hide);
			
			await _seq.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, ct);
		}

        public void Clear()
		{
		    _seq?.Kill();
		    _myRect.anchoredPosition = new Vector2(LEFT_POS_X, 400);
		    _vsRect.localScale = Vector3.zero;
		    _vsRect.localRotation = Quaternion.Euler(0, 0, 90);
		    _opponentRect.anchoredPosition = new Vector2(RIGHT_POS_X, -400);
		}
		
		protected override void OnDestroy()
		{
			_seq?.Kill();
			base.OnDestroy();
		}
	}
}
