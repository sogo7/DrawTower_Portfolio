using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace DrawTower.Ui
{
	public class InstructionUi : BaseUi, IInstructionUi
	{
		[SerializeField]
		private TextMeshProUGUI _text;

		[SerializeField]
		private InstructionDataSO _instructionData;
		
		private Tween _tween;
		
		public async UniTask ShowAndAnimAsync(InstructionType type, CancellationToken token)
		{
			Clear();
			var data = GetData(type);
			_text.text = data != null ? data.text : string.Empty;
			_text.fontSize = data != null && data.fontSize > 0f ? data.fontSize : 80f;
			Show();
			_tween = _text.rectTransform.DOScaleY(1f, 0.4f).SetEase(Ease.OutBack);
			await _tween.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, token);
		}
		
		public async UniTask HideAndAnimAsync(CancellationToken token)
		{
			Clear();
			_tween = _text.rectTransform.DOScaleY(0f, 0.2f).SetEase(Ease.OutQuad);
			await _tween.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, token);
			_text.text = "";
			Hide();
		}

        public void Clear()
		{
		    _tween?.Kill();
		}

		private InstructionData GetData(InstructionType type)
		{
			return _instructionData != null ? _instructionData.GetInstructionDataByType(type) : null;
		}
		
		protected override void OnDestroy()
		{
			Clear();
			base.OnDestroy();
		}
	}
}
