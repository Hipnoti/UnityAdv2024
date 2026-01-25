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
    private const int OBJECT_COUNT = 400000000;

    private JobHandle firstHandle;
    private JobHandle handle;
    private NativeArray<float> itemsResultArray;
    public NativeArray<float> npcResultArray;
    public NativeArray<float> strucutresResultArray;
    public NativeArray<float> resourcesResultArray;
    public NativeArray<float> terrainResultArray;
    private Unity.Mathematics.Random random = new ((uint)DateTime.Now.Ticks);
    
    private IEnumerator Start()
    {
        yield return null;
           //
           // StartJobs();
           // FinishJobs();
        
        // Stopwatch stopwatch = Stopwatch.StartNew();
        //  NomralMapCreation();
        //  stopwatch.Stop();
        // Debug.Log($"Normal Map Creation Time: {stopwatch.ElapsedMilliseconds/1000f} ms");
    }

    [ContextMenu("Start Jobs")]
    private void StartJobs()
    {
        itemsResultArray = new NativeArray<float>(OBJECT_COUNT, Allocator.TempJob);
        resourcesResultArray = new NativeArray<float>(OBJECT_COUNT, Allocator.TempJob);
        terrainResultArray = new NativeArray<float>(OBJECT_COUNT, Allocator.TempJob);
        npcResultArray = new NativeArray<float>(OBJECT_COUNT, Allocator.TempJob);
        strucutresResultArray = new NativeArray<float>(OBJECT_COUNT, Allocator.TempJob);
        
        MapCreationJob mapCreationJob = new MapCreationJob
        {
            itemsResultArray = itemsResultArray,
            npcResultArray = npcResultArray,
            strucutresResultArray = strucutresResultArray, 
            resourcesResultArray = resourcesResultArray,
            terrainResultArray = terrainResultArray
        };

      //  MapCreationJob firstJob = new MapCreationJob();
     //   firstHandle = firstJob.Schedule(OBJECT_COUNT, 100000);
        handle = mapCreationJob.Schedule(OBJECT_COUNT, 100000);
    }
    //
    [ContextMenu("Finish Jobs")]
    private void FinishJobs()
    { 
        Stopwatch stopwatch = Stopwatch.StartNew();
    
        handle.Complete();
        
        itemsResultArray.Dispose();
        stopwatch.Stop();
        Debug.Log($"Job Handle Completion Time: {stopwatch.ElapsedMilliseconds/1000f}");
        
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