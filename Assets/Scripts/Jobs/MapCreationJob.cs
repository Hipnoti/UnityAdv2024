using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;


public struct MapCreationJob : IJobParallelFor
{
    public NativeArray<float> array;
    public Unity.Mathematics.Random rand;
    
    public void Execute(int index)
    {
        array[index] = rand.NextInt(1, 10);
    }
    
    // public void Execute(int index)
    // {
    //     Unity.Mathematics.Random threadRandom = new Unity.Mathematics.Random(15 + (uint)index);
    //     array[index] = threadRandom.NextInt(1, 10);
    // }
}
