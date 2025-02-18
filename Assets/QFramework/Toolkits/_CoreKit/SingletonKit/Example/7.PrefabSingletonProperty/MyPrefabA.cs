using UnityEngine;

namespace QFramework.Example
{
    public class MyPrefabA : MonoBehaviour
    {
        public static MyPrefabA Instance => PrefabSingletonProperty<MyPrefabA>
            .InstanceWithLoader(prefabName => Resources.Load<GameObject>("Prefabs/" + prefabName));
    }
}