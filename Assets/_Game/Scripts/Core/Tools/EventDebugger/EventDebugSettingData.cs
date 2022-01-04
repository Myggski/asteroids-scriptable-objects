using System;
using DefaultNamespace.ScriptableEvents;
using UnityEngine;

namespace Core.Tools.CustomDebugger {
    /// <summary>
    /// Setting information about the a specific event
    /// </summary>
    [Serializable]
    public class EventDebugSettingData {
        [SerializeField]
        private string _assetGuid;
        [SerializeField]
        private ScriptableEventBase _eventAsset;
        [SerializeField]
        private bool _isChecked;

        public string AssetGuid => _assetGuid;
        public ScriptableEventBase EventAsset => _eventAsset;
        public bool IsChecked {
            get => _isChecked;
            set => _isChecked = value;
        }

        public EventDebugSettingData(string assetGuid, ScriptableEventBase eventAsset, bool isChecked) {
            _assetGuid = assetGuid;
            _eventAsset = eventAsset;
            _isChecked = isChecked;
        }
    }
}