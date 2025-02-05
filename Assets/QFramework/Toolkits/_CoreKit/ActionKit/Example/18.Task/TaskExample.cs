using System;
using System.Threading.Tasks;
using UnityEngine;

namespace QFramework.Example
{
    public class TaskExample : MonoBehaviour
    {
        private void Start()
        {
            ActionKit.Task(SomeTask).Start(this);
            
            SomeTask().ToAction().Start(this);

            ActionKit.Sequence()
                .Task(SomeTask)
                .Start(this);
        }

        async Task SomeTask()
        {
            await Task.Delay(TimeSpan.FromSeconds(1.0f));
            Debug.Log("Hello:" + Time.time);
        }
    }
}