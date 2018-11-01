using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Float")]
public class FloatVariable : Variable
{
    public int _value;
    public int _MaxValue;
    public override object Value
    {
        get
        {
            return _value;
        }
    }

    public override object MaxValue
    {
        get
        {
            return _MaxValue;
        }
    }
}
