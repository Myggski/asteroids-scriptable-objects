using System;
using DefaultNamespace.Vars;
using UnityEngine;

[Serializable]
public class FloatRef
{
    [SerializeField] private bool _useSimpleValue;
        
    [SerializeField] private FloatVar _variableNewName;
    [SerializeField] private float _simpleValue;

    public float Value => _useSimpleValue ? _simpleValue : _variableNewName.Value;

#if UNITY_EDITOR
    public static string VariableName = nameof(_variableNewName);
    public static string UseSimpleValueName = nameof(_useSimpleValue); // "_useSimpleValue"
    public static string SimpleValueName = nameof(_simpleValue);
#endif
}