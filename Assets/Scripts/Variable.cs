using UnityEngine;

public abstract class Variable : ScriptableObject
{
    public abstract object Value { get; }
    public abstract object MaxValue { get; }
    public delegate void OnValueChanged();
    public OnValueChanged onValueChanged;
}
