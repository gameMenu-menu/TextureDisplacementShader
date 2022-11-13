using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDeformer : MonoBehaviour
{
    public float _speed, _amplitude;
    public Mesh mesh;

    protected virtual void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    
}
