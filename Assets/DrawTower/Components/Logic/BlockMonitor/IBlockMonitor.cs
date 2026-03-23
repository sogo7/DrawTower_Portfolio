using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace DrawTower.Logic
{
	public interface IBlockMonitor
	{
		UniTask TrackBlockAsync(Transform block, CancellationToken ct);
		float GetSpawnPosY();
	}
}

