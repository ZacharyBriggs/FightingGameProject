using UnityEngine;

[CreateAssetMenu(menuName = "_IntVariable")]
public class HealthScriptable : FloatVariable, IDamageable
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

    public void TakeDamage(int amount)
    {
        Value -= amount;
    }
}
