using Core;
using UnityEngine;
using Variables;

namespace Lasers
{
    /// <summary>
    /// List of lasers, triggers laser fired observable when a new laser is being added
    /// </summary>
    [CreateAssetMenu(fileName = "New LaserSet", menuName = "Sets/LaserSet")]
    public class LaserSet : RuntimeSet<Laser> {
        [SerializeField] private IntObservable _totalLasersFiredObservable;

        public int Amount => _items.Count;

        public override void Add(Laser value) {
            base.Add(value);
            
            _totalLasersFiredObservable.ApplyChange(1);
        }
    }
}
