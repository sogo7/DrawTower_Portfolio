using System.Threading;
using Cysharp.Threading.Tasks;
using R3;

namespace DrawTower.Ui
{
	public interface IPasscodeArea : IBaseUi
	{
		string GetPasscode();
		UniTask MoveLeftAnimAsync(CancellationToken ct);
		UniTask MoveCenterAnimAsync(CancellationToken ct);
		UniTask MoveRightAnimAsync(CancellationToken ct);
		Observable<Unit> OnClickSearchBtnAsObservable();
	}
}
