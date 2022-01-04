using System;
using DefaultNamespace.ScriptableEvents;
using UnityEngine;

namespace Variables
{
    [CreateAssetMenu(fileName = "New Int Observable", menuName = "ScriptableObjects/Variables/Int Observable")]
    public class IntObservable : IntVariable
    {
        [Header("References:")]
        [SerializeField] private ScriptableEventIntReference _onValueChangedIntReference;

        public override void ApplyChange(int change)
        {
            base.ApplyChange(change);
            _onValueChangedIntReference.Raise(ToIntReference());
        }
    }
}
  