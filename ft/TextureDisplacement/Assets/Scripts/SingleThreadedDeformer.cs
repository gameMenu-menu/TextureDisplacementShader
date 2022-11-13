using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleThreadedDeformer : BaseDeformer
{
   private Vector3[] _vertices;

   protected override void Awake()
   {
       _vertices = mesh.vertices;
   }

   private void Update()
   {
       Deform();
   }

   private void Deform()
   {
       for (var i = 0; i < _vertices.Length; i++)
       {
           var position = _vertices[i];
           position.y = DeformerUtilities.CalculateDisplacement(position, Time.time, _speed, _amplitude);
           _vertices[i] = position;
       }

       // MarkDynamic optimizes mesh for frequent updates according to docs
       mesh.MarkDynamic();
       // Update the mesh visually just by setting the new vertices array
       mesh.SetVertices(_vertices);
       // Must be called so the updated mesh is correctly affected by the light
       mesh.RecalculateNormals();
   }
}
