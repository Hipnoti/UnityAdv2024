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
    
    public void Execute(int index)
    {
        Unity.Mathematics.Random threadRandom = new Unity.Mathematics.Random(15 + (uint)index);
        itemsResultArray[index] = threadRandom.NextFloat(1, 10);
        npcResultArray[index] = threadRandom.NextFloat(1, 10);
        strucutresResultArray[index] = threadRandom.NextFloat(1, 10);
        resourcesResultArray[index] = threadRandom.NextFloat(1, 10);
        terrainResultArray[index] = threadRandom.NextFloat(1, 10);
    }
}
