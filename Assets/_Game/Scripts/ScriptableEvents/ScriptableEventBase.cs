using Game;
using System;
using System.Runtime.CompilerServices;
using Core.Tools.CustomDebugger;
using UnityEngine;

namespace DefaultNamespace.ScriptableEvents {
    public abstract class ScriptableEventBase : ScriptableObject {
        private event Action _eventNoPayload;

        public void Register(Action onEventNoPayload) {
            _eventNoPayload += onEventNoPayload;
        }

        public void Unregister(Action onEventNoPayload) {
            _eventNoPayload -= onEventNoPayload;
        }

        public void Raise(
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0
        ) {
#if EVENT_DEBUG
            EventDebugger.Log(this, memberName, sourceFilePath, sourceLineNumber);
#endif
            _eventNoPayload?.Invoke();
        }
    }
}
