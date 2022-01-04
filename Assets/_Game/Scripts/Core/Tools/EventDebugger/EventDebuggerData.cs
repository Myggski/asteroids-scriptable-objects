using System;

namespace Core.Tools.CustomDebugger {
    /// <summary>
    /// Information that's saved in the EventDebugger
    /// </summary>
    public struct EventDebuggerData {
        public string EventName;
        public Type PayloadType;
        public string PayloadValue;
        public string MemberName;
        public string SourceFilePath;
        public int SourceLineNumber;
    }
}