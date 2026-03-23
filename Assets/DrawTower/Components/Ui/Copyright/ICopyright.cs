using System.Threading;
using Cysharp.Threading.Tasks;
using R3;

namespace DrawTower.Ui
{
	public interface ICopyright
	{
		UniTask ShowAndAnimAsync(CancellationToken ct);
		UniTask HideAndAnimAsync(CancellationToken ct);
		void SetText(string text);
		bool IsLoaded();
		Observable<Unit> OnClickCloseBtnAsObservable();
	}
}

