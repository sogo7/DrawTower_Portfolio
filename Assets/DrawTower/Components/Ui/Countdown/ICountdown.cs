using System.Threading;
using Cysharp.Threading.Tasks;

namespace DrawTower.Ui
{
	public interface ICountdown : IBaseUi
	{
		UniTask StartCountdownAsync(CountdownType type, CancellationToken ct);
	}
}
