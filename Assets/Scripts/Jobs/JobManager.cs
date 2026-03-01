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
    public const int OBJECT_COUNT = 400000000;
    private JobHandle handle;
    
    private NativeArray<int> numbersToMultiply;
    private NativeReference<long> multiplyResult;
    
    private NativeArray<float> itemsResultArray;
    public NativeArray<float> npcResultArray;
    public NativeArray<float> strucutresResultArray;
    public NativeArray<float> resourcesResultArray;
    public NativeArray<float> terrainResultArray;
    
    private float[] itemsArray;
    public float[] npcArray;
    public float[] strucutresRArray;
    public float[] resourcesArray;
    public float[] terrainArray;
    
    private Unity.Mathematics.Random random = new ((uint)DateTime.Now.Ticks);
    
    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();

      //   Stopwatch stopwatch = Stopwatch.StartNew();
      //  StartCoroutine(StartMultiplyJob());
   //     NormalMultiply();
        // stopwatch.Stop();
        //  Debug.Log($"Multiply Time: {stopwatch.ElapsedMilliseconds/1000f} ms");
        // StartJobs();
        // FinishJobs();
        
        StartCoroutine(StartJobs());

     //       NormalMapCreation();
        //  stopwatch.Stop();
        // Debug.Log($"Normal Map Creation Time: {stopwatch.ElapsedMilliseconds/1000f} ms");
    }

    [ContextMenu("Start Jobs")]
    private IEnumerator StartJobs()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        
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

      
        JobHandle handle = mapCreationJob.Schedule(OBJECT_COUNT, 2000000);
        
        yield return new WaitUntil(() => handle.IsCompleted);
        
        handle.Complete();
        
        stopwatch.Stop();
        Debug.Log($"Multiply Time: {stopwatch.ElapsedMilliseconds/1000f} ms");
        
        itemsResultArray.Dispose();
        npcResultArray.Dispose();
        strucutresResultArray.Dispose();
        resourcesResultArray.Dispose();
        terrainResultArray.Dispose();
        
   
        
     //   firstHandle = firstJob.Schedule(OBJECT_COUNT, 100000);
     //   handle = mapCreationJob.Schedule(OBJECT_COUNT, 100000);
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
    
    private void NormalMultiply()
    {  
        Stopwatch stopwatch = Stopwatch.StartNew();
        int[] numbersToMultiply = new int[OBJECT_COUNT];
        for (int i = 0; i < numbersToMultiply.Length; i++)
        {
            numbersToMultiply[i] = i + 1;
        }

        int result = 0;
        for (int i = 0; i < numbersToMultiply.Length - 1; i++)
        {
            result += numbersToMultiply[i] * numbersToMultiply[i + 1];
        }

        //numbersToMultiply.Dispose();
        Debug.Log("Multiply result is " + result);
        stopwatch.Stop();
        Debug.Log($"Multiply Time: {stopwatch.ElapsedMilliseconds/1000f} ms");
    }

    private void NormalMapCreation()
    {
        float[] itemsArray = new float[OBJECT_COUNT];
        float[] npcArray = new float[OBJECT_COUNT];
        float[] strucutresRArray = new float[OBJECT_COUNT];
        float[] resourcesArray = new float[OBJECT_COUNT];
        float[] terrainArray = new float[OBJECT_COUNT];
        for (int i = 0; i < OBJECT_COUNT; i++)
        {
            itemsArray[i] = random.NextFloat(1, 10);
            npcArray[i] = random.NextFloat(1, 10);
            strucutresRArray[i] = random.NextFloat(1, 10);
            resourcesArray[i] = random.NextFloat(1, 10);
            terrainArray[i] = random.NextFloat(1, 10);
        }
    }

    private IEnumerator StartMultiplyJob()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        multiplyResult = new NativeReference<long>(0,Allocator.TempJob);
        MultiplyJob multiplyJob = new MultiplyJob
        {
            numbersToMultiply = new NativeArray<int>(OBJECT_COUNT, Allocator.TempJob),
            result = multiplyResult
        };

        JobHandle jobHandle = multiplyJob.Schedule();
        
        yield return new WaitUntil(() => jobHandle.IsCompleted);
        
        jobHandle.Complete();
        
        Debug.Log("Multiply result is " + multiplyResult.Value);
        
        numbersToMultiply.Dispose();
        multiplyResult.Dispose();
        
        stopwatch.Stop();
         Debug.Log($"Multiply Time: {stopwatch.ElapsedMilliseconds/1000f} ms");
    }
}