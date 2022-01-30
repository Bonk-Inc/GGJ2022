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
        projectileCollider.gameObject.GetComponent<ProjectileAttack>().senderTag = shooterCollider.gameObject.tag;
        
        Physics2D.IgnoreCollision(shooterCollider, projectileCollider);
    }
}
