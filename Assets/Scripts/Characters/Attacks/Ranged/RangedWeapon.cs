using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    
    [SerializeField]
    private Collider2D projectilePrefab;

    [SerializeField]
    private Collider2D shooterCollider; 

    public void Shoot(){
        var projectileCollider = Instantiate<Collider2D>(projectilePrefab, transform.position, transform.rotation);
        Physics2D.IgnoreCollision(shooterCollider, projectileCollider);
    }


}
