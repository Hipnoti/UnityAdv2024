using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;


[BurstCompile]
public struct MapCreationJob : IJobParallelFor
{
    public NativeArray<float> itemsResultArray;
    public NativeArray<float> npcResultArray;
    public NativeArray<float> strucutresResultArray;
    public NativeArray<float> resourcesResultArray;
    public NativeArray<float> terrainResultArray;
    
    // public void Execute(int index)
    // {
    //     itemsResultArray[index] = rand.NextInt(1, 10);
    // }
    
    public void Execute(int index)
    {
        Unity.Mathematics.Random threadRandom = new Unity.Mathematics.Random(15 + (uint)index);
        itemsResultArray[index] = threadRandom.NextInt(1, 10);
        npcResultArray[index] = threadRandom.NextInt(1, 10);
        strucutresResultArray[index] = threadRandom.NextInt(1, 10);
        resourcesResultArray[index] = threadRandom.NextInt(1, 10);
        terrainResultArray[index] = threadRandom.NextInt(1, 10);
    }
}
