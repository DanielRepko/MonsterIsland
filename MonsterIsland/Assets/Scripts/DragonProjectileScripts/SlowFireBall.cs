using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowFireBall : Projectile {

    public float timeTillDestroy = 6;
    private float destroyTimer;

    private void FixedUpdate()
    {
        if (unsetTriggerTimer < unsetTriggerTime)
        {
            unsetTriggerTimer += Time.deltaTime;
        }
        else if (unsetTriggerTimer >= unsetTriggerTime && GetComponent<BoxCollider2D>().isTrigger)
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
        }

        if (destroyTimer < timeTillDestroy)
        {
            destroyTimer += Time.deltaTime;
        }
        else if (destroyTimer >= timeTillDestroy)
        {
            Destroy(gameObject);
        }

        //code to follow player
        Vector2 destination = (PlayerController.Instance.transform.position - transform.position).normalized * speed;
        GetComponent<Rigidbody2D>().velocity = destination;
    }
}
