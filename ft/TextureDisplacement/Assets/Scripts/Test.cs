using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public MeshRenderer Renderer;
    public MeshFilter Filter;
    
    
    public RenderTexture SaveTexture;
    void Start()
    {
        //SetRenderTexture(Renderer.material.GetTexture("_SideTex"));
    }

    void Update()
    {
        Renderer.material.SetVector("_ComparePos", transform.position);

        Texture texture = Renderer.material.GetTexture("_SideTex");

        Graphics.Blit (texture, SaveTexture, Renderer.material);
    }

    void LateUpdate()
    {
        //Filter.mesh.MarkDynamic();
        //Filter.mesh.RecalculateNormals();


    }

    void SetRenderTexture(Texture tex)
    {
        SaveTexture = new RenderTexture(tex.width, tex.height, 1);
    }
}
