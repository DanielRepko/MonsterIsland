using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidBreath : MonoBehaviour {

    public int damage = 1;

    public float lifeTime = 2;
    private float lifeTimer = 0;

    public float timeTillDamage = 0.3f;
    private float damageTimer = 0;
    private bool readyToDamage = true;

    public string target;

    private void FixedUpdate()
    {
        if(!readyToDamage && damageTimer < timeTillDamage)
        {
            damageTimer += Time.deltaTime;
        }
        else if(!readyToDamage && damageTimer >= timeTillDamage)
        {
            damageTimer = 0;
            readyToDamage = true;
        }

        if (lifeTimer < lifeTime)
        {
            lifeTime += Time.deltaTime;
        }
        else if (lifeTimer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
