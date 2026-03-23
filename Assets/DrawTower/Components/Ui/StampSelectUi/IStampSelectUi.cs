using System.Threading;
using Cysharp.Threading.Tasks;
using R3;

namespace DrawTower.Ui
{
	public interface IStampSelectUi : IBaseUi
	{
		UniTask ShowOpenBtnAsync(CancellationToken ct);
		UniTask ShowSelectAreaAsync(CancellationToken ct);
		UniTask HideSelectAreaAsync(CancellationToken ct);
		Observable<Unit> OnClickOpenBtnAsObservable();
		Observable<StampType> OnClickSelectBtnAsObservable();
	}
}
