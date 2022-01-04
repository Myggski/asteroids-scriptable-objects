using UnityEditor;
using UnityEngine;

namespace DefaultNamespace.ScriptableEvents.Editor
{
    [CustomEditor(typeof(ScriptableEventBase), true)]
    public class ScriptableEventEditor : UnityEditor.Editor
    {
        private ScriptableEventBase _target;

        public override void OnInspectorGUI()
        {
            _target = (ScriptableEventBase)target;

            base.OnInspectorGUI();

            if (GUILayout.Button("Debug Raise Event"))
            {
               _target.Raise();
            }
        }
    }
}
