using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using QFramework;

public class SingletonInitializer : MonoBehaviour
{
    public GameObject[] singleObjects;
    private static SingletonInitializer _instance;

    public static SingletonInitializer Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("SingletonInitializer");
                _instance = go.AddComponent<SingletonInitializer>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    private void OnEnable()
    {
        // 在场景加载完成后触发初始化
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void Start()
    {
        //InitializeAllSingletons();
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 扫描场景中的所有单例并初始化
        InitializeAllSingletons();
    }

    private void InitializeAllSingletons()
    {
    }
}