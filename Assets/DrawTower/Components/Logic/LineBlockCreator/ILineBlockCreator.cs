using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Fusion;
using R3;
using UnityEngine;

namespace DrawTower.Logic
{
    public interface ILineBlockCreator
    {
        GameObject CreateOfflineBlock(IReadOnlyList<Vector3> points, float spawnPosY, Color color);
        UniTask<NetworkObject> CreateOnlineBlockAsync(IReadOnlyList<Vector3> points, NetworkRunner runner, float spawnPosY, Color color);
        Mesh CreateMesh(IReadOnlyList<Vector3> points);
        void ApplyMesh(NetworkObject no, IReadOnlyList<Vector3> points, Color color);
        Observable<int> OnBlockScoreCreated();
    }
}