using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HadoukenEffect : Effect
{
    public override List<string> MoveInput
    {
        get
        {
            return new List<string> {"Down", "DownRight", "Right", "Attack" };
        }
    }

    public override void DoEffect(Vector3 pos, GameObject prefab)
    {
        pos.x += 1;
        Instantiate(prefab, pos, Quaternion.identity);
    }
}
