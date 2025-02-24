using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILoading : MonoBehaviour
{
    //  ���ؽ�����
    public Slider slider;
    AsyncOperation async;
    void Start()
    {
        StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
        //  �ж��Ƿ���3D����
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
