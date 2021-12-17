using System.Collections.Generic;
using UnityEngine;

namespace Core {
    public abstract class RuntimeSet<TKey, TValue> : ScriptableObject {
        public Dictionary<TKey, TValue> Items = new Dictionary<TKey, TValue>();

        protected void Add(TKey key, TValue value) {
            if (!Items.TryGetValue(key, out _)) {
                Items.Add(key, value);
            }
        }

        protected void Remove(TKey key) {
            if (Items.TryGetValue(key, out _)) {
                Items.Remove(key);
            }
        }
    }
}