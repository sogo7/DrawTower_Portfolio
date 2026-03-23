using System.Threading;
using Cysharp.Threading.Tasks;

namespace DrawTower.Ui
{
	public interface IMoveBtn : IScaleAnimBtn
	{
		UniTask MoveLeftAnimAsync(CancellationToken ct);
		UniTask MoveCenterAnimAsync(CancellationToken ct);
		UniTask MoveRightAnimAsync(CancellationToken ct);
	}
}