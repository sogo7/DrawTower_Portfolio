using System.Threading;
using Cysharp.Threading.Tasks;
using R3;

namespace DrawTower.Ui
{
	public interface IStartBtn : IBaseUi
	{
		UniTask ShowAndAnimAsync(CancellationToken cancellationToken);
		void Clear();
		Observable<Unit> OnClickAsObservable();
	}
}
