using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.ScriptableEvents;
using UnityEditor;
using UnityEngine;

namespace Core.Tools.CustomDebugger.Editor {
    public class EventDebugConsole : EditorWindow {
        private static List<EventDebugSettingData> _eventCheckboxDataList;
        
        private const string CUSTOM_LOG_FLAG = "EVENT_DEBUG";
        private static bool _filterFoldout = true;
        private static Vector2 _debugScrollPosition = Vector2.zero;

        private string DefinedSymbols =>
            PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone);

        private bool HasCustomDefineSymbol => DefinedSymbols.Contains(CUSTOM_LOG_FLAG);
        private int NumberOfCheckboxesChecked => _eventCheckboxDataList.Count(x => x.IsChecked);
        private List<string> SelectedEventNames => _eventCheckboxDataList != null && _eventCheckboxDataList.Any()
            ? _eventCheckboxDataList
                .Where(checkboxData => !ReferenceEquals(checkboxData.EventAsset, null) && checkboxData.IsChecked)
                .Select(checkboxData => checkboxData.EventAsset.name)
                .ToList() 
            : new List<string>();

        [MenuItem("Events/Debug Console")]
        private static EventDebugConsole DebuggerWindow() => GetWindow<EventDebugConsole>("Event Debug Console");
        private void Awake() => Initialize();
        private void OnEnable() => Enable();
        private void OnDisable() => EventDebugger.OnLogChanged += Repaint;
        private void OnDestroy() => RemoveCustomDefineSymbols();
        private void OnGUI() => DisplayGUI();
        private void OnProjectChange() => ProjectChange();

        private void Initialize() {
            SetCustomDefineSymbols();
        }

        private void ProjectChange() {
            _eventCheckboxDataList = EventDebugConsoleSettings.GetSavedData();
            Repaint();
        }
        
        private void Enable() {
            EventDebugger.OnLogChanged += Repaint;
            _eventCheckboxDataList = EventDebugConsoleSettings.GetSavedData();
        }
        
        private void DisplayGUI() {
            DisplayFilterCheckboxes();
            DisplayLog();
        }

        /// <summary>
        /// Adds CUSTOM_LOG as defined symbol
        /// </summary>
        private void SetCustomDefineSymbols() {
            if (HasCustomDefineSymbol) {
                return;
            }

            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone,
                $"{DefinedSymbols}{CUSTOM_LOG_FLAG};");
        }

        /// <summary>
        /// Removes CUSTOM_LOG as defined symbol
        /// </summary>
        private void RemoveCustomDefineSymbols() {
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, DefinedSymbols
                .Replace(CUSTOM_LOG_FLAG, ""));
        }

        /// <summary>
        /// Displays the created event assets as checkboxes
        /// </summary>
        private void DisplayFilterCheckboxes() {
            if (!_eventCheckboxDataList.Any()) {
                EditorGUILayout.LabelField("No event to debug");
                return;
            }

            int totalChecked = NumberOfCheckboxesChecked;

            CustomGUI.Box(() => {
                CustomGUI.Foldout(ref _filterFoldout, () => {
                    CustomGUI.CheckboxWrapper(() => {
                        _eventCheckboxDataList.ForEach(checkboxData => {
                            checkboxData.IsChecked =
                                EditorGUILayout.ToggleLeft(checkboxData.EventAsset.name, checkboxData.IsChecked);
                        });
                    });
                });
            });

            // If any checkbox value has changed, the data will be saved
            if (totalChecked != NumberOfCheckboxesChecked) {
                EventDebugConsoleSettings.SaveData(_eventCheckboxDataList);
            }
            
            EditorGUILayout.Separator();
        }

        /// <summary>
        /// Displays all the custom event logs
        /// TODO: Fix autoscroll whenever a new log is added, temporary solution for this is to display the log backwards
        /// </summary>
        private void DisplayLog() {
            List<EventDebuggerData> logs = EventDebugger.GetLogsByMemberNames(SelectedEventNames);

            if (!logs.Any()) {
                return;
            }
            
            CustomGUI.ScrollView(ref _debugScrollPosition, () => {
                for (int i = logs.Count - 1; i >= 0; i--) {
                    CustomGUI.CustomDebuggerDataRow(logs[i], i % 2 == 0);
                }
            });
        }
    }
}