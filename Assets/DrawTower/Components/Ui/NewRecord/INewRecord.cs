using System.Threading;
using Cysharp.Threading.Tasks;

namespace DrawTower.Ui
{
	public interface INewRecord : IBaseUi
	{
		UniTask ShowAnimAsync(CancellationToken ct);
	}
}
