using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILoading : MonoBehaviour
{
    //  加载进度条
    public Slider slider;
    AsyncOperation async;
    void Start()
    {
        StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
        //  判断是否有3D场景
        if (Global.Contain3DScene)
        {
            async = SceneManager.LoadSceneAsync(Global.LoadSceneName, LoadSceneMode.Single);
            if(!Global.LoadUIName.Equals("")) SceneManager.LoadSceneAsync(Global.LoadUIName, LoadSceneMode.Additive);
        }
        else
        {
            async = SceneManager.LoadSceneAsync(Global.LoadUIName, LoadSceneMode.Single);
        }

        yield return async;
    }
    private void Update()
    {
        slider.value = async.progress;
    }
}
