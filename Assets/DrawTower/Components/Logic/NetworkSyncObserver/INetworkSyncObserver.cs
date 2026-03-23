using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Fusion;
using R3;
using UnityEngine;
using DrawTower.Ui;

namespace DrawTower.Logic
{
	public interface INetworkSyncObserver
	{
		//線データ
		Observable<IReadOnlyList<Vector3>> OnDrawLineAsObservable();
		UniTask SendDrawLineAsync(IReadOnlyList<Vector3> points);	
		UniTask<IReadOnlyList<Vector3>> WaitForDrawLineAsync();
		
		// メッシュデータ
		Observable<(NetworkObject, IReadOnlyList<Vector3>)> OnApplyMeshAsObservable();
		void SendMesh(NetworkId networkId);
		UniTask<NetworkObject> WaitForMeshTargetAsync();
		
		//ブロック操作完了  
		void SendBlockLanded();
		UniTask<Unit> WaitForBlockLandedAsync();
		
		// 落下判定
		void SendBlockDropped(bool isLose);
		Observable<bool> OnBlockDroppedAsObservable();
		
		// 攻守交代
		Observable<int> OnNextTurnOwnerAsObservable();
		void SendNextTurnOwner();
		void SendDecideTurnOwner();
		
		// スタンプ
		Observable<StampType> OnStampReceivedAsObservable();
		void SendStamp(StampType stamp);
	}
}
