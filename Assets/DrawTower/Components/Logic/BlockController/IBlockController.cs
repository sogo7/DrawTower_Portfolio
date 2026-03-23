using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace DrawTower.Logic
{
	public interface IBlockController
	{
		Observable<Unit> OnBlockLanded();
		UniTask StartControllerAsync(GameObject block, CancellationToken ct);
		void StopController();
		UniTask RotateSnapAsync(CancellationToken ct);
	}
}