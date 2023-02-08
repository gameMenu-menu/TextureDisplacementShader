using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public MeshRenderer Renderer;
    public MeshFilter Filter;
    
    public LayerMask surfaceMask;

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            MoveSphere();
        }

        Renderer.material.SetVector("_ComparePos", transform.position);


    }

    void MoveSphere()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 10000f, surfaceMask))
        {
            transform.position = hit.point;
        }
    }

}
