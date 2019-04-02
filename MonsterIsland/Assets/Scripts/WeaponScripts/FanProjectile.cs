using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanProjectile : Projectile {

    private float timeTillDestroy = 1.5f;
    private float destroyTimer = 0;

    private void FixedUpdate()
    {
        if(destroyTimer < timeTillDestroy)
        {
            destroyTimer += Time.deltaTime;
        }
        else if(destroyTimer >= timeTillDestroy)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
