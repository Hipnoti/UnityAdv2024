using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Jobs
{
    
    public class JobManager : MonoBehaviour
    {
        private JobHandle _handle;
        private NativeArray<float> result;
        
        [ContextMenu("Start Jobs")]
        private void StartJobs()
        {
            ExampleJob job = new ExampleJob();
            job.array = result = new NativeArray<float>(1, Allocator.TempJob);
            job.number = 10;
            job.number2 = 20;
            job.number3 = 30;
            _handle = job.Schedule();
        }
        
        [ContextMenu("Finish Jobs")]
        private void FinishJobs()
        {
            _handle.Complete();
            
        }
    }
}