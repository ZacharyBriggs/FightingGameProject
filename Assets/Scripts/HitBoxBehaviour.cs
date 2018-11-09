using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxBehaviour : MonoBehaviour, IDamager
{
    public float DamageAmount;
    public FighterBehaviour Fighter;
    public void DealDamage(IDamageable other, float amount)
    {
        other.TakeDamage(amount);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        var damagable = col.GetComponent<IDamageable>();
        DealDamage(damagable, DamageAmount);
        Fighter.Meter.AddToValue(10);
    }
}
