using System;
using System.Collections.Generic;
using System.IO;
using DefaultNamespace.ScriptableEvents;
using UnityEditor;
using UnityEngine;

namespace Core.Tools.CustomDebugger {
    /// <summary>
    /// This helper-class gets and saves previous settings of the Event Debug Console
    /// </summary>
    public static class EventDebugConsoleSettings {
        private const string FILE_NAME = "DebuggerToolSettings";
        private const string SAVEFILE_TYPE = ".json";
        private static readonly string SAVE_DIRECTORY = $"{Application.dataPath}/Save/";
        private static readonly string[] _debuggableEvents = {
            nameof(ScriptableEvent),
            nameof(ScriptableEventInt),
            nameof(ScriptableEventVector3),
            nameof(ScriptableEventIntReference)
        };
        
        private static string GetFilePath => $"{SAVE_DIRECTORY}{FILE_NAME}{SAVEFILE_TYPE}";

        /// <summary>
        /// Saves the state of the filter checkboxes
        /// </summary>
        /// <param name="checkboxData">List of checkboxes</param>
        public static void SaveData(List<EventDebugSettingData> checkboxData) {
            if (!Directory.Exists(SAVE_DIRECTORY)) {
                Directory.CreateDirectory(SAVE_DIRECTORY);
            }

            File.WriteAllText(GetFilePath, JsonUtility.ToJson(new DebuggerToolFilterSettingsData(checkboxData)));
        }

        /// <summary>
        /// Loads the state of the filter checkboxes from file
        /// Then check adds new events that's not saved in the file, if there are any new
        /// </summary>
        /// <returns></returns>
        public static List<EventDebugSettingData> GetSavedData() {
            string jsonData = GetDataString();

            List<EventDebugSettingData> savedData = !string.IsNullOrEmpty(jsonData)
                ? JsonUtility.FromJson<DebuggerToolFilterSettingsData>(jsonData).SavedData
                : new List<EventDebugSettingData>();
            
            Array.ForEach(_debuggableEvents, eventName => {
                string[] newEventAssetGuids = Array.FindAll(AssetDatabase.FindAssets($"t:{eventName}", new[] {"Assets/_Game/Components"}),
                    assetGuid => !savedData.Exists(x => x.AssetGuid == assetGuid));
               
                Array.ForEach(newEventAssetGuids,  assetGuid => savedData.Add(new EventDebugSettingData(assetGuid,
                    AssetDatabase.LoadAssetAtPath<ScriptableEventBase>(AssetDatabase.GUIDToAssetPath(assetGuid)),
                    false)));
            });

            return savedData;
        }

        private static string GetDataString() {
            if (File.Exists(GetFilePath)) {
                return File.ReadAllText(GetFilePath);
            }

            return null;
        }

        /// <summary>
        /// This class exist just to be able to convert List to JSON
        /// </summary>
        [Serializable]
        private class DebuggerToolFilterSettingsData {
            [SerializeField]
            private List<EventDebugSettingData> _savedData;

            public List<EventDebugSettingData> SavedData => _savedData;

            public DebuggerToolFilterSettingsData(List<EventDebugSettingData> savedData) {
                _savedData = savedData;
            }
        }
    }
}