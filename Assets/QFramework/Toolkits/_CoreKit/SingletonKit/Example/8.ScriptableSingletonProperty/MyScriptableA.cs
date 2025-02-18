using UnityEngine;

namespace QFramework.Example
{
    [CreateAssetMenu(fileName = nameof(MyScriptableA))]
    public class MyScriptableA : ScriptableObject
    {
        public string ScriptableKey;
        
        public static MyScriptableA Instance => ScriptableSingletonProperty<MyScriptableA>.InstanceWithLoader(
            scriptableName => Resources.Load<MyScriptableA>("Scriptable/" + scriptableName));
    }
}