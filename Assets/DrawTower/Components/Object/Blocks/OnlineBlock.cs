using Cysharp.Threading.Tasks;
using Fusion;
using UnityEngine;

public class OnlineBlock : NetworkBehaviour
{     
    [SerializeField] 
    private Material _material;
    
    [SerializeField]
    private MeshFilter _mf;
    
    [SerializeField]
    private MeshCollider _mc;
    
    [SerializeField]
    private MeshRenderer _mr;
    
    private UniTaskCompletionSource _spawnedTcs;
    private bool _spawned;
    
    public override void Spawned()
    {
        _spawned = true;
        _spawnedTcs?.TrySetResult();
        _spawnedTcs = null;
    }
    
    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        _spawned = false;
        _spawnedTcs?.TrySetCanceled();
        _spawnedTcs = null;
    }
    
    public void SetMesh(Mesh mesh)
    {
        _mf.mesh = mesh;
        _mc.sharedMesh = mesh;
    }
    
    public void SetColor(Color color)
    {
        _mr.sharedMaterial = _material;
        _mr.material.color = color;
    }
    
    public UniTask WhenSpawnedAsync()
    {
        if (_spawned) return UniTask.CompletedTask;

        _spawnedTcs ??= new UniTaskCompletionSource();
        return _spawnedTcs.Task;
    }
}
