using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.ScriptableEvents;

namespace Core.Tools.CustomDebugger {
    public static class EventDebugger {
        private static List<EventDebuggerData> _logs;

        public static event Action OnLogChanged;

        static EventDebugger() {
            _logs = new List<EventDebuggerData>();
        }

        public static List<EventDebuggerData> GetLogsByMemberNames(List<string> eventNames) {
            if (!_logs.Any()) {
                return _logs;
            }

            return _logs.Where(log => eventNames.Contains(log.EventName)).ToList();
        }

        public static void Log<TEvent>(
            TEvent calledEvent,
            string memberName = "",
            string sourceFilePath = "",
            int sourceLineNumber = 0
        ) where TEvent : ScriptableEventBase {
            EventDebuggerData data = new EventDebuggerData {
                EventName = calledEvent.name,
                MemberName = memberName,
                SourceFilePath = sourceFilePath,
                SourceLineNumber = sourceLineNumber
            };

            Add(data);
        }

        public static void Log<TEvent, TPayload>(
            TEvent calledEvent,
            TPayload payload = default,
            string memberName = "",
            string sourceFilePath = "",
            int sourceLineNumber = 0
        ) where TEvent : ScriptableEventPayloadBase<TPayload> {
            EventDebuggerData data = new EventDebuggerData {
                EventName = calledEvent.name,
                PayloadType = payload.GetType(),
                PayloadValue = payload.ToString(),
                MemberName = memberName,
                SourceFilePath = sourceFilePath,
                SourceLineNumber = sourceLineNumber
            };

            Add(data);
        }

        private static void Add(EventDebuggerData data) {
            _logs.Add(data);
            
            OnLogChanged?.Invoke();
        }
    }
}