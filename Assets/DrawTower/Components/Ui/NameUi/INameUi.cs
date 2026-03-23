using System.Threading;
using Cysharp.Threading.Tasks;
using R3;

namespace DrawTower.Ui
{
	public interface INameUi
	{
		UniTask ShowAndAnimAsync(CancellationToken ct);
		UniTask HideAndAnimAsync(CancellationToken ct);
		string GetName();
		bool IsNameLengthInvalid(string name);
		void ShowNgWordAlert();
		void SetEditMode(bool isEdit);
		Observable<string> OnClickApplyBtnAsObservable();
	}
}
