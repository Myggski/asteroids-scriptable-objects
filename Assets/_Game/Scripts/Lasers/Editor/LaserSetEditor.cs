using UnityEditor;

namespace Lasers.Editor {
    [CustomEditor(typeof(LaserSet))]
    public class LaserSetEditor : UnityEditor.Editor {
        private LaserSet _target;
        private int _amount;

        public override bool RequiresConstantRepaint() {
            _target = (LaserSet) target;

            return _amount != _target.Amount;
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            _target = (LaserSet) target;
            _amount = _target.Amount;

            using (new EditorGUILayout.HorizontalScope()) {
                EditorGUILayout.LabelField($"Amount of Lasers: {_amount}");
            }
        }
    }
}
