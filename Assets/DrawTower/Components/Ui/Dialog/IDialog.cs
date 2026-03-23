using System.Threading;
using Cysharp.Threading.Tasks;
using R3;

namespace DrawTower.Ui
{
	public interface IDialog
	{
		UniTask ShowAndAnimAsync(DialogType dialogType, CancellationToken ct, string text = null);
		UniTask HideAndAnimAsync(CancellationToken ct);
		DialogType GetDialogType();
		Observable<Unit> OnClickCloseBtnAsObservable();
		Observable<Unit> OnClickYesBtnAsObservable();
		Observable<Unit> OnClickNoBtnAsObservable();
		Observable<Unit> OnClickActionBtnAsObservable();
	}
}

