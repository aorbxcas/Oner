using System;
using UnityEngine;

namespace QFramework.Example
{
    public class PrefabSingletonPropertyExample : MonoBehaviour
    {
        private void Start()
        {
            var prefabA = MyPrefabA.Instance;
        }

        private void OnDestroy()
        {
            PrefabSingletonProperty<MyPrefabA>.Dispose();
        }
    }


}
