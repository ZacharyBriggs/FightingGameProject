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
            return new List<string> {"Down", "DownForward", "Forward", "Attack" };
        }
    }

    public override int InputCount
    {
        get
        {
            return 4;
        }
    }

    public override void DoEffect(Vector3 pos, GameObject prefab, Rigidbody2D rb2d)
    {
        pos.x += 1.2f;
        Instantiate(prefab, pos, Quaternion.identity);
    }
}
