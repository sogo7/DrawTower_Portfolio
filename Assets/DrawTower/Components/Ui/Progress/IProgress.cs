using System.Threading;
using Cysharp.Threading.Tasks;

namespace DrawTower.Ui
{
	public interface IProgress
	{
		UniTask ShowAnimAsync(CancellationToken ct);
		void Hide();
	}
}

