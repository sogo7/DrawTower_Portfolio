using System.Threading;
using Cysharp.Threading.Tasks;

namespace DrawTower.Logic
{
	public interface ISceneLoader
	{
		UniTask LoadOfflineGameSceneAsync(CancellationToken token = default);
	}
}
