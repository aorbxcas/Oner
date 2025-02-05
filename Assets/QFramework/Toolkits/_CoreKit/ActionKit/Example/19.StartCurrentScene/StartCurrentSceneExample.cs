using UnityEngine;
using UnityEngine.SceneManagement;

namespace QFramework.Example
{
    public class StartCurrentSceneExample : MonoBehaviour
    {
        void Start()
        {
            ActionKit.Sequence()
                .Delay(1.0f)
                .Callback(() =>
                {
                    Debug.Log("printed");
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                })
                .Delay(1.0f)
                .Callback(() =>
                {
                    Debug.Log("Not print");
                })
                .StartCurrentScene();
        }
    }
}