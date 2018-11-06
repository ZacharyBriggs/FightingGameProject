using UnityEngine;

[CreateAssetMenu]
public class MeterScriptable : FloatVariable
{
    [SerializeField] public float _mMaxValue;
    [SerializeField] public float _mValue;

    public float Value
    {
        get { return _mValue; }
        set
        {
            _mValue = value;
        }
    }
    public float MaxValue
    {
        get { return _mMaxValue; }
        set
        {
            _mMaxValue = value;
        }
    }

    public void AddToValue(float amount)
    {
        Value += amount;
    }
}