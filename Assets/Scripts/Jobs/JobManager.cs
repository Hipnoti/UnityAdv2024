using System;
using System.Collections;
using System.Diagnostics;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class JobManager : MonoBehaviour
{
    private const int OBJECT_COUNT = 2000000000;
    
    private JobHandle handle;
    private NativeArray<float> result;
    private Unity.Mathematics.Random random = new ((uint)DateTime.Now.Ticks);
    
    private IEnumerator Start()
    {
        yield return null;
        
          StartJobs();
          FinishJobs();
        
        // Stopwatch stopwatch = Stopwatch.StartNew();
        //  NomralMapCreation();
        //  stopwatch.Stop();
        // Debug.Log($"Normal Map Creation Time: {stopwatch.ElapsedMilliseconds/1000f} ms");
    }

    [ContextMenu("Start Jobs")]
    private void StartJobs()
    {
        result = new NativeArray<float>(OBJECT_COUNT, Allocator.TempJob);
        MapCreationJob job = new MapCreationJob { array = result, rand = random};
        handle = job.Schedule(OBJECT_COUNT, 10000);
    }

    [ContextMenu("Finish Jobs")]
    private void FinishJobs()
    { 
        Stopwatch stopwatch = Stopwatch.StartNew();

        handle.Complete();
       
        result.Dispose();
        stopwatch.Stop();
        Debug.Log($"Job Handle Completion Time: {stopwatch.ElapsedMilliseconds/1000f} ms");
        
    }
    
    private void NomralMapCreation()
    {
        float[] array = new float[OBJECT_COUNT];
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = random.NextInt(1, 10);
        }
    }
}