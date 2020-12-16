using UnityEngine;
using System.Collections;

public class MapRenderer : MonoBehaviour
{

    public int size = 512;

    private RenderTexture rt;
    private GameObject quad;
    private MeshRenderer quadRenderer;
    private Camera cam;

    public void Awake()
    {
        cam = this.GetComponent<Camera>();
        quad = this.transform.Find("Quad").gameObject;
        quadRenderer = quad.GetComponent<MeshRenderer>();
    }

    private Texture2D GetTexture2D(RenderTexture rt)
    {
        RenderTexture currentActiveRT = RenderTexture.active;
        RenderTexture.active = rt;
        Texture2D tex = new Texture2D(rt.width, rt.height);
        tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
        RenderTexture.active = currentActiveRT;
        return tex;
    }

    void Start()
    {
        //Generate heights
        rt = new RenderTexture(size, size, 16, RenderTextureFormat.ARGB32);
        rt.Create();
        cam.targetTexture = rt;
        cam.Render();

        Texture2D tex = GetTexture2D(rt);
        Terrain.instance.GenerateTerrain(tex, rt);
    }

}
