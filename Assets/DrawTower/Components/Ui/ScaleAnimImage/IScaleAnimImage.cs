using System.Threading;
using Cysharp.Threading.Tasks;

namespace DrawTower.Ui
{
	public interface IScaleAnimImage : IBaseUi
	{
		UniTask ShowAndAnimAsync(CancellationToken ct);
	}
}
