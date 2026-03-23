using System.Threading;
using Cysharp.Threading.Tasks;

namespace DrawTower.Ui
{
	public interface IScore : IBaseUi
	{
		UniTask ShowAndAnimAsync(CancellationToken ct);
		UniTask HideAndAnimAsync(CancellationToken ct);
		UniTask UpdateTextAsync(float score, float duration, CancellationToken ct);
		void Clear();
	}
}
