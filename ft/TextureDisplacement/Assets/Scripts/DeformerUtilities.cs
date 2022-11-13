using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Burst;

public static class DeformerUtilities
{
   [BurstCompile]
   public static float CalculateDisplacement(Vector3 position, float time, float speed, float amplitude)
   {
       var distance = 6f - Vector3.Distance(position, Vector3.zero);
       return Mathf.Sin(time * speed + distance) * amplitude;
   }

   /*public static bool CalculateDisplacement(Vector3 position)
   {
       var distance = Vector3.Distance(position, ComparePosition);
       return Mathf.Sin(time * speed + distance) * amplitude;
   }*/
}
