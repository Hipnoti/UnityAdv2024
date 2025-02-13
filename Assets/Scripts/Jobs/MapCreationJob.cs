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
}
