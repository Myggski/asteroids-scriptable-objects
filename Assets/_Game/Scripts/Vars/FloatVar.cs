using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New FloatVar", menuName = "SOs/FloatVar")]
public class FloatVar : ScriptableObject {
    [Range(0, 20)]
    [SerializeField]
    private float _value;

    [TextArea(3, 6)]
    [SerializeField]
    private string _developerDescription;

    private float _currentValue;

    public float Value => _value;

    private void OnEnable() => _currentValue = _value;
}
