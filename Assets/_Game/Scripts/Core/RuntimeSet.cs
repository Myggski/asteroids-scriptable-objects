using System.Collections.Generic;
using UnityEngine;

namespace Core {
    public abstract class RuntimeSet<T> : ScriptableObject where T : IRuntimeSetObject {
        protected Dictionary<int, T> _items = new Dictionary<int, T>();
        
        protected virtual void OnEnable()
        {
            _items.Clear();
        }

        public T Get(int instanceId) {
            _items.TryGetValue(instanceId, out T item);

            return item;
        }

        public virtual void Add(T value) {
            _items.Add(value.InstanceId, value);
        }

        public virtual void Remove(int instanceId) {
            _items.Remove(instanceId);
        }
    }
}