using UnityEngine;

namespace QFramework.Example
{
    public class IgnoreTimeScaleExample : MonoBehaviour
    {
        private void Start()
        {
            Time.timeScale = 0.25f;
            Debug.Log("Launch Scaled Time:" + Time.time);
            Debug.Log("Launch Unscaled Time:" + Time.unscaledTime);
            ActionKit.Sequence()
                .Delay(3.0f)
                .Callback(() =>
                {
                    Debug.Log("Scaled Time:" + Time.time);
                    Debug.Log("Unscaled Time:" + Time.unscaledTime);
                })
                .Start(this)
                .IgnoreTimeScale();
        }
    }
}
// Scaled Time: 0.7585141
// Unscaled Time: 3.02394;