using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace DrawTower.Ui
{
	public class PasscodeBtn : MoveBtn, IPasscodeBtn
	{
		public override async UniTask MoveLeftAnimAsync(CancellationToken ct)
		{
			await base.MoveLeftAnimAsync(ct);
			
			var rect = (RectTransform)_btn.transform;
			rect.anchoredPosition = new Vector2(
				RIGHT_POS_X,
				rect.anchoredPosition.y
			);
		}
	}
}
