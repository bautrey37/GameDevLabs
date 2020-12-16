using UnityEngine;
using System.Collections;

public class Terrain : MonoBehaviour
{
    public static Terrain instance;

    public int countx = 3;
    public int county = 3;
    public Vector3 scale = new Vector3(10f, 2f, 10f);

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private Texture2D mapDataTexture;
    private RenderTexture mapDataRenderTexture;

    public void Awake()
    {
        instance = this;
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void GenerateTerrain(Texture2D mapDataTexture, RenderTexture rt)
    {
        this.mapDataTexture = mapDataTexture;
        this.mapDataRenderTexture = rt;

        Mesh mesh = new Mesh();
        meshFilter.sharedMesh = mesh;

        GenerateTerrainMesh(mesh);
        meshRenderer.material.SetTexture("_MainTex", mapDataRenderTexture);

        mesh.RecalculateNormals();
    }

    public Color Bisample(Vector2 pp, int tw, int th, Color[] colors)
    {
        float px = pp.x * tw;
        float py = pp.y * th;
        float fx = px - (int)(px);
        float fy = py - (int)(py);

        int ix = (int)(px);
        int iy = (int)(py);
        //Clamping
        ix = ix < tw ? ix : tw - 1;
        iy = iy < th ? iy : th - 1;
        int ix2 = (ix+1) < tw ? (ix+1) : tw - 1;
        int iy2 = (iy+1) < th ? (iy+1) : th - 1;

        int m1 = ix + iy * tw;
        int m2 = ix2 + iy * tw;
        int m3 = ix + iy2 * tw;
        int m4 = ix2 + iy2 * tw;

        Color c1 = colors[m1];
        Color c2 = colors[m2];
        Color c3 = colors[m3];
        Color c4 = colors[m4];
        return Color.Lerp(Color.Lerp(c1, c2, fx), Color.Lerp(c3, c4, fx), fy);
    }

    public void GenerateTerrainMesh(Mesh mesh)
    {
        int tw = mapDataTexture.width;
        int th = mapDataTexture.height;
        Color[] mapcols = mapDataTexture.GetPixels();

        Vector3 centerPos = scale / 2f;
        int vertCount = countx * county;

        int[] tris = new int[(countx - 1) * (county - 1) * 6];
        Vector3[] verts = new Vector3[vertCount];
        Vector2[] uvs = new Vector2[vertCount];

        int v = 0;
        int t = 0;
        for (int i = 0; i < countx; i++)
        {
            for (int j = 0; j < county; j++)
            {
                Vector2 pp = new Vector2(i / ((float)countx - 1), j / ((float)county - 1));
                Color val = Bisample(pp, tw, th, mapcols);
                float h = val.r;
                verts[v] = new Vector3(pp.x * scale.x, h * scale.y, pp.y * scale.z) - centerPos;
                uvs[v] = pp;
                v++;
                if (i < (countx - 1) && j < (county - 1))
                {
                    int pos = j + county * i;
                    tris[t] = pos;
                    tris[t + 1] = pos + 1;
                    tris[t + 2] = pos + county;
                    tris[t + 3] = pos + 1;
                    tris[t + 4] = pos + 1 + county;
                    tris[t + 5] = pos + county;
                    t += 6;
                }
            }
        }

        mesh.vertices = verts;
        mesh.uv = uvs;
        mesh.triangles = tris;
    }

}
