using System;
using UnityEngine;

namespace Variables
{
    [CreateAssetMenu(fileName = "New FloatVariable", menuName = "ScriptableObjects/Variables/IntVariable")]
    public class IntVariable : ScriptableObject
    {
        [SerializeField] private int _value;

        private int _currentValue;

        public int Value => _currentValue;

        protected IntReference ToIntReference() => new IntReference(this);

        public virtual void ApplyChange(int change)
        {
            _currentValue += change;
        }

        private void OnEnable()
        {
            _currentValue = _value;
        }

        public override string ToString() {
            return Value.ToString();
        }
    }
}
