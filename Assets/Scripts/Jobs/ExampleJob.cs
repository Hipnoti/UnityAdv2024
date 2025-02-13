using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public struct ExampleJob : IJob
{
    public float number;
    public float number2;
    public float number3;

    public NativeArray<float> array;
    
    public void Execute()
    {
        number += number2;
        number *= number3;
        array[0] = number;
    }
}
