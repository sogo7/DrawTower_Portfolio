using UnityEngine;
using R3;
using Fusion;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DrawTower.Ui;

namespace DrawTower.Logic
{
    public class NetworkSyncObserver : NetworkBehaviour, INetworkSyncObserver
    {
        //線データ
        private readonly Subject<IReadOnlyList<Vector3>> _onDrawLine = new();
        public Observable<IReadOnlyList<Vector3>> OnDrawLineAsObservable() => _onDrawLine;
        private UniTaskCompletionSource<IReadOnlyList<Vector3>> _drawLineTcs;
        private List<Vector3> _recvDrawBuffer = new();

        [Rpc(RpcSources.All, RpcTargets.All, Channel = RpcChannel.Reliable, InvokeLocal = false, TickAligned = true)]
        private void RPC_DrawLine(float[] xs, float[] ys, bool isFirst, bool isLast)
        {
            if (isFirst)
                _recvDrawBuffer.Clear();
        
            var pts = ToVector3List(xs, ys);
            _recvDrawBuffer.AddRange(pts);
            
            if (isLast)
            {
                _onDrawLine.OnNext(new List<Vector3>(_recvDrawBuffer));
                _drawLineTcs?.TrySetResult(new List<Vector3>(_recvDrawBuffer));
            }
        }
        
        public async UniTask SendDrawLineAsync(IReadOnlyList<Vector3> points)
        {
            int total = points.Count;
            int index = 0;

            while (index < total)
            {
                int count = Mathf.Min(50, total - index);
                var slice = Slice(points, index, count);

                bool isFirst = index == 0;
                bool isLast = index + count >= total;

                ToFloatArrays(slice, out var xs, out var ys);
                RPC_DrawLine(xs, ys, isFirst, isLast);

                index += count;
                
                if (!isLast)
                {
                    var t = Runner.Tick;
                    await UniTask.WaitUntil(() => Runner != null && Runner.Tick > t);
                }
            }
        }

        public UniTask<IReadOnlyList<Vector3>> WaitForDrawLineAsync()
        {
            _drawLineTcs?.TrySetCanceled();
            
            _drawLineTcs = new UniTaskCompletionSource<IReadOnlyList<Vector3>>();
            return _drawLineTcs.Task;
        }
        
        // メッシュデータ
        private readonly Subject<(NetworkObject, IReadOnlyList<Vector3>)> _onApplyMesh = new();
        public Observable<(NetworkObject, IReadOnlyList<Vector3>)> OnApplyMeshAsObservable() => _onApplyMesh;
        private NetworkObject _meshTarget;
        private UniTaskCompletionSource<NetworkObject> _meshTargetTcs;

        [Rpc(RpcSources.All, RpcTargets.All, Channel = RpcChannel.Reliable, InvokeLocal = false, TickAligned = true)]
        private void RPC_ApplyMesh(NetworkId networkId)
        {
            _ = ApplyMeshAsync(networkId);
        }
        
        private async UniTask ApplyMeshAsync(NetworkId networkId)
        {
            var no = await WaitFindAsync(networkId);
            _onApplyMesh.OnNext((no, new List<Vector3>(_recvDrawBuffer)));
            
            if (_meshTargetTcs != null)
            {
                _meshTargetTcs.TrySetResult(no);
                _meshTargetTcs = null;
            }
            else
            {
                _meshTarget = no;
            }
        }
        
        public void SendMesh(NetworkId networkId)
        {
            RPC_ApplyMesh(networkId);
        }

        public UniTask<NetworkObject> WaitForMeshTargetAsync()
        {
            if (_meshTarget != null)
            {
                var no = _meshTarget;
                _meshTarget = null;
                return UniTask.FromResult(no);
            }
            
            _meshTargetTcs?.TrySetCanceled();
            
            _meshTargetTcs = new UniTaskCompletionSource<NetworkObject>();
            return _meshTargetTcs.Task;
        }
        
        //ブロック操作完了       
        private UniTaskCompletionSource<Unit> _blockLandedTcs;
        
        [Rpc(RpcSources.All, RpcTargets.All, Channel = RpcChannel.Reliable, InvokeLocal = false, TickAligned = true)]
        private void RPC_BlockLanded()
        {
            _blockLandedTcs?.TrySetResult(Unit.Default);
        }
        
        public void SendBlockLanded()
        {
            RPC_BlockLanded();
        }

        public UniTask<Unit> WaitForBlockLandedAsync()
        {
            _blockLandedTcs?.TrySetCanceled();
            
            _blockLandedTcs = new UniTaskCompletionSource<Unit>();
            return _blockLandedTcs.Task;
        }
        
        // 落下判定
        private readonly Subject<bool> _onBlockDropped = new();
        public Observable<bool> OnBlockDroppedAsObservable() => _onBlockDropped;

        [Rpc(RpcSources.All, RpcTargets.All, Channel = RpcChannel.Reliable, InvokeLocal = false, TickAligned = true)]
        private void RPC_BlockDropped(bool isLose)
        {
            _onBlockDropped.OnNext(isLose);
        }

        public void SendBlockDropped(bool isLose)
        {
            RPC_BlockDropped(!isLose);
        }
        
        // 攻守交代
        private int _cachedNextTurnOwner = 0;
        private readonly Subject<int> _onNextTurnOwner = new();
        public Observable<int> OnNextTurnOwnerAsObservable() => _onNextTurnOwner;

        [Rpc(RpcSources.All, RpcTargets.All, Channel = RpcChannel.Reliable, InvokeLocal = true, TickAligned = true)]
        private void RPC_SetNextTurnOwner(int value)
        {
            _onNextTurnOwner.OnNext(value);
        }

        public void SendNextTurnOwner()
        {
            if (Runner.IsSharedModeMasterClient == false)
            return;
            
            _cachedNextTurnOwner = (_cachedNextTurnOwner == 0) ? 1 : 0;
            RPC_SetNextTurnOwner(_cachedNextTurnOwner);
        }
        
        public void SendDecideTurnOwner()
        {
            if (Runner.IsSharedModeMasterClient == false)
                return;

            _cachedNextTurnOwner = Random.Range(0, 2);
            RPC_SetNextTurnOwner(_cachedNextTurnOwner);
        }
        
        // スタンプ
        private readonly Subject<StampType> _onStampReceived = new();
        public Observable<StampType> OnStampReceivedAsObservable() => _onStampReceived;

        [Rpc(RpcSources.All, RpcTargets.All, Channel = RpcChannel.Reliable, InvokeLocal = false, TickAligned = true)]
        private void RPC_SendStamp(StampType type)
        {
            _onStampReceived.OnNext(type);
        }

        public void SendStamp(StampType type)
        {
            RPC_SendStamp(type);
        }
        
        private void ToFloatArrays(
            IReadOnlyList<Vector3> points,
            out float[] xs,
            out float[] ys)
        {
            int count = points.Count;
            xs = new float[count];
            ys = new float[count];

            for (int i = 0; i < count; i++)
            {
                xs[i] = points[i].x;
                ys[i] = points[i].y;
            }
        }

        private List<Vector3> ToVector3List(float[] xs, float[] ys)
        {
            int count = xs.Length;
            var list = new List<Vector3>(count);

            for (int i = 0; i < count; i++)
                list.Add(new Vector3(xs[i], ys[i], 0));

            return list;
        }
        
        private static List<Vector3> Slice(IReadOnlyList<Vector3> src, int start, int count)
        {
            var list = new List<Vector3>(count);
            int end = start + count;
            for (int i = start; i < end; i++)
            {
                list.Add(src[i]);
            }
            return list;
        }
        
        private async UniTask<NetworkObject> WaitFindAsync(NetworkId id)
        {
            while (true)
            {
                if (Runner.TryFindObject(id, out var no))
                    return no;

                var t = Runner.Tick;
                await UniTask.WaitUntil(() => Runner.Tick > t);
            }
        }
    }
}
