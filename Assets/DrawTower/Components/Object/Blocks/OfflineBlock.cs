using UnityEngine;

public class OfflineBlock : MonoBehaviour
{
    [SerializeField] 
    private Material _material;
    
    [SerializeField]
    private MeshFilter _mf;
    
    [SerializeField]
    private MeshCollider _mc;
    
    [SerializeField]
    private MeshRenderer _mr;
    
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
}
