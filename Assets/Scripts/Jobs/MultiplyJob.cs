using Unity.Burst;
using Unity.Jobs;
using Unity.Collections;

public struct MultiplyJob : IJob
{
    public NativeReference<long> result;
    public NativeArray<int> numbersToMultiply;
    
    public void Execute()
    {
        for (int i = 0; i < numbersToMultiply.Length; i++)
        {
            numbersToMultiply[i] = i + 1;
        }
        for (int i = 0; i < numbersToMultiply.Length - 1; i++)
        {
            result.Value += numbersToMultiply[i] * numbersToMultiply[i + 1];
        }
    }
}