using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Jobs;

public class JobSystemDeformer : BaseDeformer
{
   private NativeArray<Vector3> _vertices;
   private bool _scheduled;
   private DeformerJob _job;
   private JobHandle _handle;

   protected override void Awake()
   {
       base.Awake();
       // Similarly to the previous approach we cache the mesh vertices array
       // But now NativeArray<Vector3> instead of Vector3[] because the latter cannot be used in jobs
       _vertices = new NativeArray<Vector3>(mesh.vertices, Allocator.Persistent);
   }

   private void Update()
   {
       TryScheduleJob();
   }

   private void LateUpdate()
   {
       CompleteJob();
   }

   private void TryScheduleJob()
   {
       if (_scheduled)
       {
           return;
       }

       _scheduled = true;
       _job = new DeformerJob(_speed, _amplitude, Time.time, _vertices);
       _handle = _job.Schedule(_vertices.Length, 64);
   }

   private void CompleteJob()
   {
       if (!_scheduled)
       {
           return;
       }

       _handle.Complete();
       mesh.MarkDynamic();
       // SetVertices also accepts NativeArray<Vector3> so we can use in here too
       mesh.SetVertices(_vertices);
       mesh.RecalculateNormals();
       _scheduled = false;
   }

   private void OnDestroy()
   {
     // Make sure to dispose all unmanaged resources when object is destroyed
       _vertices.Dispose();
   }
}
