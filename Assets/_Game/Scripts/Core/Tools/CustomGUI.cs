using System;
using Core.Tools.CustomDebugger;
using UnityEditor;
using UnityEngine;

namespace Core.Tools {
    public static class CustomGUI {
        private static readonly GUIStyle _richLinkStyle = new GUIStyle() {
            richText = true,
        };
        
        private static readonly GUIStyle _rowStyle = new GUIStyle("Button") {
            padding = new RectOffset(8, 8, 8, 8),
        };

        public static void CheckboxWrapper(Action callback) {
            var originalMargin = EditorStyles.label.margin;
            EditorStyles.label.margin = new RectOffset(20, 20, 8, 8);

            using (new EditorGUILayout.VerticalScope(EditorStyles.label)) {
                callback.Invoke();
            }
                    
            EditorStyles.label.margin = originalMargin;
        }

        public static void Box(Action callback) {
            RectOffset originalPadding = EditorStyles.helpBox.padding;
            EditorStyles.helpBox.padding = new RectOffset(8, 8, 16, 16);

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox)) {
                callback.Invoke();
            }

            EditorStyles.helpBox.padding = originalPadding;
        }

        public static void Foldout(ref bool foldout, Action callback) {
            foldout = EditorGUILayout.Foldout(foldout, "Event Debug Filter", true);

            if (foldout) {
                callback.Invoke();
            }
        }

        public static void ScrollView(ref Vector2 debugScrollPosition, Action callback) {
            using (EditorGUILayout.ScrollViewScope scrollView = new EditorGUILayout.ScrollViewScope(debugScrollPosition)) {
                debugScrollPosition = scrollView.scrollPosition;

                callback.Invoke();
            }
        }

        public static void CustomDebuggerDataRow(EventDebuggerData log, bool isEven) {
            string parameter = String.Empty;
                
            if (log.PayloadValue != null) {
                parameter = $"{log.PayloadType.Name} {log.PayloadValue}";
            }

            GUI.backgroundColor = isEven? Color.clear : new Color(56f/255f, 56f/255f, 56f/255f, 0.1f);

            using (new EditorGUILayout.VerticalScope(_rowStyle)) {
                EditorGUILayout.LabelField(log.EventName, EditorStyles.boldLabel);
                EditorGUILayout.LabelField($"Method: {log.MemberName}({parameter})");
                FileLink(log.SourceFilePath, log.SourceLineNumber);
            }
                    
            GUI.backgroundColor = Color.clear;
        }

        public static void FileLink(string sourceFilePath, int sourceLineNumber) {
            string linkText = sourceFilePath
                .Replace('\\', '/')
                .Replace(Application.dataPath, "");
          
            EditorGUILayout.TextField($"<a href=\"{sourceFilePath}\" line=\"{sourceLineNumber}\">{linkText}:{sourceLineNumber}</a>", _richLinkStyle);
        }
    }
}