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
        // �ڳ���������ɺ󴥷���ʼ��
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
        // ɨ�賡���е����е�������ʼ��
        InitializeAllSingletons();
    }

    private void InitializeAllSingletons()
    {
    }
}