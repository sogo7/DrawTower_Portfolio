using System.Threading;
using Cysharp.Threading.Tasks;
using R3;

namespace DrawTower.Ui
{
	public interface IScaleAnimBtn : IBaseUi
	{
		UniTask ShowAndAnimAsync(CancellationToken ct);
		UniTask HideAndAnimAsync(CancellationToken ct);
		void Clear();
		CustomBtn GetCustomBtn();
		Observable<Unit> OnClickAsObservable();
	}
}
