using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : ScriptableObject
{
    public abstract List<string> MoveInput { get; }
    public abstract void DoEffect(Vector3 pos, GameObject prefab);
}
