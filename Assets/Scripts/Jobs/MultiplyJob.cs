using Unity.Burst;
using Unity.Jobs;
using Unity.Collections;

public struct MultiplyJob : IJob
{
    public NativeArray<int> numbersToMultiply;
    public NativeArray<int> result;

    //Part of IJob
    public void Execute()
    {
        for (int i = 0; i < numbersToMultiply.Length - 1; i++)
        {
            result[0] += numbersToMultiply[i] * numbersToMultiply[i + 1];
        }
    }
}