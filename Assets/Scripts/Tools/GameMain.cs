using UnityEngine;
using System.Collections;
    
using System.IO;
using QFramework;
using UnityEngine.SceneManagement;

public class GameMain : MonoSingleton<GameMain>
{
    private AsyncOperation async;
    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    /// 加载进度条场景
    /// </summary>
    IEnumerator Load()
    {
        async = SceneManager.LoadSceneAsync("Loading");
        yield return async;
    }


    /// <summary>
    /// 加载场景
    /// </summary>
    public IEnumerator LoadScene(string uiScene)
    {
        Global.Contain3DScene = false;
        Global.LoadUIName = uiScene;
        yield return StartCoroutine(Load());
    }

    public IEnumerator LoadScene(string uiScene, string scene)
    {
        Global.Contain3DScene = true;
        Global.LoadUIName = uiScene;
        Global.LoadSceneName = scene;
        yield return StartCoroutine(Load());
    }
}

