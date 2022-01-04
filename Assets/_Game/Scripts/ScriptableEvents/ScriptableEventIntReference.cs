using System.Runtime.CompilerServices;
using UnityEngine;
using Variables;

namespace DefaultNamespace.ScriptableEvents {
    [CreateAssetMenu(
        fileName = "new ScriptableEventIntReference",
        menuName = "ScriptableObjects/ScriptableEvent-IntReference", 
        order = 0
    )]
    public class ScriptableEventIntReference : ScriptableEventPayloadBase<IntReference> {
        public void Raise(
            int newValue,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0
        ) {
            Raise(new IntReference(newValue), memberName, sourceFilePath, sourceLineNumber);
        }
    }
}
