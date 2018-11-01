using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadoukenBehaviour : MonoBehaviour
{
    public Vector3 Force;
    public float Mass;
    public float LifeTime;
	
	// Update is called once per frame
	void Update ()
    {
        var vel = Force / Mass;
        this.transform.position += vel * Time.deltaTime;
        LifeTime -= Time.deltaTime;
        if (LifeTime <= 0)
            Destroy(this.gameObject);
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        var other = col.GetComponent<IDamageable>();
        other.TakeDamage(10);
        Destroy(this.gameObject);
    }
}
