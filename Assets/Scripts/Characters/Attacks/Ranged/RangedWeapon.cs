using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : SimpleSemiAutoWeapon
{
    
    [SerializeField]
    private Collider2D projectilePrefab;

    [SerializeField]
    private Collider2D shooterCollider;

    public override void Attack()
    {
        var projectileCollider = Instantiate<Collider2D>(projectilePrefab, transform.position, transform.rotation);
        Physics2D.IgnoreCollision(shooterCollider, projectileCollider);
    }
}
