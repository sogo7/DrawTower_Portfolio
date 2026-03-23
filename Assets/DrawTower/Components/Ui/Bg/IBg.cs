using System.Threading;
using Cysharp.Threading.Tasks;

namespace DrawTower.Ui
{
	public interface IBg
	{
		UniTask BgAnimAsync(CancellationToken ct);
	}
}
