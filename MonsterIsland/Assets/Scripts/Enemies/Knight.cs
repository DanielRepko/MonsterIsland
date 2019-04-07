using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Enemy {

    public override void InitializeEnemy()
    {
        base.InitializeEnemy();
        checkDelegate += CanTeleport;
    }

    public void CanTeleport()
    {
        if (CheckCooldown("ability") && isAggro && !attacksLocked && !inHitStun)
        {
            abilityDelegate();
        }
    }

    public override void Ability()
    {
        Ray topRay = new Ray();
        Ray middleRay = new Ray();
        Ray bottomRay = new Ray();

        topRay.origin = new Vector2(transform.position.x, transform.position.y + 1.4f);
        topRay.direction = new Vector2(facingDirection, 0);

        middleRay.origin = new Vector2(transform.position.x, transform.position.y);
        middleRay.direction = new Vector2(facingDirection, 0);

        bottomRay.origin = new Vector2(transform.position.x, transform.position.y - 1.8f);
        bottomRay.direction = new Vector2(facingDirection, 0);

        Debug.DrawRay(topRay.origin, new Vector2(8 * facingDirection, 0), Color.green);
        Debug.DrawRay(middleRay.origin, new Vector2(8 * facingDirection, 0), Color.green);
        Debug.DrawRay(bottomRay.origin, new Vector2(8 * facingDirection, 0), Color.green);

        //these raycasts check to see if there is terrain in the path of teleport
        RaycastHit2D topRayTerrainHit = Physics2D.Raycast(topRay.origin, topRay.direction, 8f, 1 << LayerMask.NameToLayer("Terrain"));
        RaycastHit2D middleRayTerrainHit = Physics2D.Raycast(middleRay.origin, middleRay.direction, 8f, 1 << LayerMask.NameToLayer("Terrain"));
        RaycastHit2D bottomRayTerrainHit = Physics2D.Raycast(bottomRay.origin, bottomRay.direction, 8f, 1 << LayerMask.NameToLayer("Terrain"));

        RaycastHit2D[] hitList = { topRayTerrainHit, middleRayTerrainHit, bottomRayTerrainHit };

        float shortestDistance = 8;

        for (int i = 0; i < hitList.Length; i++)
        {
            if (hitList[i])
            {
                if (hitList[i].distance < shortestDistance)
                {
                    shortestDistance = hitList[i].distance;
                }
            }
        }

        //shortestDistance - 0.3 to account for the half of the player that will be over the distance threshold
        float teleportDistance = transform.position.x + ((shortestDistance - 0.6f) * facingDirection);

        //making the player "disappear"
        monster.gameObject.SetActive(false);

        //moving the player to the new
        transform.position = new Vector2(teleportDistance, transform.position.y);

        //making the player "reappear"
        monster.gameObject.SetActive(true);
    }
}
