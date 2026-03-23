using System.Collections.Generic;
using UnityEngine;
using R3;
using LibTessDotNet;
using Mesh = UnityEngine.Mesh;
using Fusion;
using Cysharp.Threading.Tasks;

namespace DrawTower.Logic
{
    public class LineBlockCreator : MonoBehaviour, ILineBlockCreator
    {
        [SerializeField] 
        private OnlineBlock _onlineBlock;
        
        [SerializeField] 
        private OfflineBlock _offlineBlock;
        
        [SerializeField] 
        private float UI_TO_WORLD_SCALE = 0.003f;
        
        private float _blockDepth = 0.1f;

        private readonly Subject<int> _onBlockScoreCreated = new();
        public Observable<int> OnBlockScoreCreated() => _onBlockScoreCreated;
              
        /// <summary>
        /// オンライン生成
        /// </summary>
        public async UniTask<NetworkObject> CreateOnlineBlockAsync(IReadOnlyList<Vector3> points, NetworkRunner runner, float spawnPosY, Color color)
        {
            var mesh = CreateMesh(points);
            
            var no = runner.Spawn(_onlineBlock, new Vector3(0, spawnPosY, 0), Quaternion.identity, runner.LocalPlayer);
            runner.SetPlayerObject(runner.LocalPlayer, no.Object);
            
            var ob = no.GetComponent<OnlineBlock>();
            await ob.WhenSpawnedAsync();
            ob.SetMesh(mesh);
            ob.SetColor(color);

            return no.Object;
        }
        
        /// <summary>
        /// オフライン生成
        /// </summary>
        public GameObject CreateOfflineBlock(IReadOnlyList<Vector3> points, float spawnPosY, Color color)
        {
            var mesh = CreateMesh(points);
            var block = Instantiate(_offlineBlock, new Vector3(0, spawnPosY, 0), Quaternion.identity);
            block.SetMesh(mesh);
            block.SetColor(color);
            return block.gameObject;
        }
        
        /// <summary>
        /// 送られてきたメッシュをブロックに反映
        /// </summary>
        public void ApplyMesh(NetworkObject no, IReadOnlyList<Vector3> points, Color color)
        {
            var mesh = CreateMesh(points);
            var ob = no.GetComponent<OnlineBlock>();
            ob.SetMesh(mesh);
            ob.SetColor(color);
        }

        /// <summary>
        /// 線データからブロックを生成
        /// </summary>
        public Mesh CreateMesh(IReadOnlyList<Vector3> points)
        {
            if (points == null || points.Count < 3)
            {
                Debug.LogWarning("ブロック化に必要な線データが不十分です。");
                return null;
            }

            // スコア（面積）
            float area = CalculatePolygonArea(points);
            int score = (int)(area / 1000f);
            _onBlockScoreCreated.OnNext(score);

            // 中心算出
            Vector3 center = Vector3.zero;
            foreach (var p in points)
                center += p;
            center /= points.Count;

            // 中心基準のローカル座標に変換（2D に）
            List<Vector2> poly2D = new();
            foreach (var p in points)
            {
                var local = (p - center) * UI_TO_WORLD_SCALE;
                poly2D.Add(new Vector2(local.x, local.y));
            }

            // ねじれ防止：ポリゴンを CCW に統一
            if (!IsPolygonCCW(poly2D))
                poly2D.Reverse();

            // LibTessDotNet 三角化（凹形・星型・自己交差にも対応）
            var frontMesh = Triangulate(poly2D);

            // 押し出し（3D 化）
            var finalMesh = Extrude(frontMesh, _blockDepth);
            return finalMesh;
        }

        /// <summary>
        /// LibTessDotNetを使った三角化
        /// </summary>
        private Mesh Triangulate(List<Vector2> points)
        {
            var tess = new Tess();

            var contour = new ContourVertex[points.Count];
            for (int i = 0; i < points.Count; i++)
            {
                contour[i].Position = new Vec3(points[i].x, points[i].y, 0);
            }
            tess.AddContour(contour);

            tess.Tessellate(WindingRule.NonZero, ElementType.Polygons, 3);

            Mesh mesh = new Mesh();
            var verts = new Vector3[tess.Vertices.Length];

            for (int i = 0; i < verts.Length; i++)
            {
                verts[i] = new Vector3(
                    tess.Vertices[i].Position.X,
                    tess.Vertices[i].Position.Y,
                    0
                );
            }

            mesh.vertices = verts;
            mesh.triangles = System.Array.ConvertAll(tess.Elements, e => (int)e);

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            return mesh;
        }

        /// <summary>
        /// Meshを奥行き方向に押し出して3Dブロック化
        /// </summary>
        private Mesh Extrude(Mesh source, float depth)
        {
            Vector3[] baseVerts = source.vertices;
            int[] baseTris = source.triangles;

            int vertCount = baseVerts.Length;

            Vector3[] verts = new Vector3[vertCount * 2];
            int[] tris = new int[baseTris.Length * 2 + vertCount * 6];

            float half = depth / 2f;

            // 前面 & 背面
            for (int i = 0; i < vertCount; i++)
            {
                verts[i] = new Vector3(baseVerts[i].x, baseVerts[i].y, -half);
                verts[i + vertCount] = new Vector3(baseVerts[i].x, baseVerts[i].y, +half);
            }

            // front
            for (int i = 0; i < baseTris.Length; i++)
                tris[i] = baseTris[i];

            // back（反転）
            int offset = baseTris.Length;
            for (int i = 0; i < baseTris.Length; i += 3)
            {
                tris[offset + i] = baseTris[i] + vertCount;
                tris[offset + i + 1] = baseTris[i + 2] + vertCount;
                tris[offset + i + 2] = baseTris[i + 1] + vertCount;
            }

            // 側面生成
            int sideStart = baseTris.Length * 2;
            int index = sideStart;

            for (int i = 0; i < vertCount; i++)
            {
                int next = (i + 1) % vertCount;

                int v0 = i;
                int v1 = next;
                int v2 = i + vertCount;
                int v3 = next + vertCount;

                tris[index++] = v0;
                tris[index++] = v2;
                tris[index++] = v1;

                tris[index++] = v1;
                tris[index++] = v2;
                tris[index++] = v3;
            }

            Mesh mesh = new Mesh();
            mesh.vertices = verts;
            mesh.triangles = tris;
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            return mesh;
        }

        /// <summary>
        /// ポリゴンの向きを判定
        /// </summary>
        private bool IsPolygonCCW(List<Vector2> pts)
        {
            float sum = 0f;
            for (int i = 0; i < pts.Count; i++)
            {
                Vector2 p1 = pts[i];
                Vector2 p2 = pts[(i + 1) % pts.Count];
                sum += (p2.x - p1.x) * (p2.y + p1.y);
            }
            return sum < 0f; // CCW の場合 true
        }

        /// <summary>
        /// 面積計算（スコア用）
        /// </summary>
        private float CalculatePolygonArea(IReadOnlyList<Vector3> points)
        {
            if (points == null || points.Count < 3)
                return 0f;

            var tess = new Tess();
            var contour = new ContourVertex[points.Count];

            for (int i = 0; i < points.Count; i++)
                contour[i].Position = new Vec3(points[i].x, points[i].y, 0);

            tess.AddContour(contour);
            tess.Tessellate(WindingRule.EvenOdd, ElementType.Polygons, 3);

            float area = 0f;
            var verts = tess.Vertices;
            var elems = tess.Elements;

            for (int i = 0; i < elems.Length; i += 3)
            {
                int i0 = elems[i];
                int i1 = elems[i + 1];
                int i2 = elems[i + 2];

                Vector2 a = new Vector2(verts[i0].Position.X, verts[i0].Position.Y);
                Vector2 b = new Vector2(verts[i1].Position.X, verts[i1].Position.Y);
                Vector2 c = new Vector2(verts[i2].Position.X, verts[i2].Position.Y);

                area += Mathf.Abs((b.x - a.x) * (c.y - a.y) - (b.y - a.y) * (c.x - a.x)) * 0.5f;
            }

            return area;
        }
    }
}
