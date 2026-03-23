using System.Threading;
using Cysharp.Threading.Tasks;

namespace DrawTower.Ui
{
	public interface ITransition
	{
		UniTask FadeInAsync(CancellationToken ct);
		UniTask FadeOutAsync(CancellationToken ct);
	}
}
