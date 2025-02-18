using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Example
{
    public class ScriptableSingletonPropertyExample : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            MyScriptableA.Instance.ScriptableKey.LogInfo();

        }


        private void OnDestroy()
        {
            ScriptableSingletonProperty<MyScriptableA>.Dispose();
        }
    }
}
