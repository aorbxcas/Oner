using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public readonly string configPath = "/config.txt";

    private AsyncOperation async;
    IEnumerator Start()
    {
        //while (!Configuration.IsDone)
        //    yield return null;
        //UIManager.Instance.InitializeUIs();
        DontDestroyOnLoad(gameObject);
        Configuration.LoadConfig(configPath);
        while (!Configuration.IsDone)
            yield return null;
    }
    public void GameStart()
    {
        if (Configuration.IsDone)
        {
            string uiscene = Configuration.GetContent("Scene", "LoadGameStart");
            StartCoroutine(LoadScene(uiscene));
        }
    }
    IEnumerator Load()
    {
        async = SceneManager.LoadSceneAsync("Loading");
        yield return async;
    }


    /// <summary>
    /// º”‘ÿ≥°æ∞
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
