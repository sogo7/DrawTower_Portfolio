using System.Threading;
using Cysharp.Threading.Tasks;

namespace DrawTower.Ui
{
	public interface IMoveToggle : IScaleAnimToggle
	{
		UniTask MoveLeftAnimAsync(CancellationToken ct);
		UniTask MoveCenterAnimAsync(CancellationToken ct);
		UniTask MoveRightAnimAsync(CancellationToken ct);
	}
}