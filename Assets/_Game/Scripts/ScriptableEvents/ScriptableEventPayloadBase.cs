using Game;
using System;
using System.Runtime.CompilerServices;
using Core.Tools.CustomDebugger;
using UnityEngine;

namespace DefaultNamespace.ScriptableEvents {
    public abstract class ScriptableEventPayloadBase<TPayload> : ScriptableEventBase {
        private event Action<TPayload> _event;

        public void Register(Action<TPayload> onEventNoPayload) {
            _event += onEventNoPayload;
        }

        public void Unregister(Action<TPayload> onEventNoPayload) {
            _event -= onEventNoPayload;
        }

        public void Raise(
            TPayload newValue,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0
        ) {
#if EVENT_DEBUG
            EventDebugger.Log(this, newValue, memberName, sourceFilePath, sourceLineNumber);
#endif

            _event?.Invoke(newValue);
        }
    }
}