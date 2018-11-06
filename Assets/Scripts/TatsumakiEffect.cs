using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TatsumakiEffect : Effect
{
    public float Speed;
    public override List<string> MoveInput
    {
        get
        {
            return new List<string> { "Down", "DownBack", "Back", "Attack" };
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
        rb2d.AddForce(new Vector2(Speed, 0));
    }
}
