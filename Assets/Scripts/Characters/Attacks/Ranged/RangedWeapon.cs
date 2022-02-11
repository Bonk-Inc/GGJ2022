using UnityEngine;

public class RangedWeapon : SimpleSemiAutoWeapon
{
    
    [SerializeField]
    private Collider2D projectilePrefab;

    [SerializeField]
    private Collider2D shooterCollider;

    [SerializeField]
    private float shootDelay = 1;

    private float currentDelayLeft = 0;

    private void Update() {
        currentDelayLeft = Mathf.Max(currentDelayLeft - Time.deltaTime, 0);
    }


    public override void Attack()
    {
        if(currentDelayLeft > 0){
            return;
        }

        var projectileCollider = Instantiate<Collider2D>(projectilePrefab, transform.position, transform.rotation);
        projectileCollider.gameObject.GetComponent<ProjectileAttack>().senderTag = shooterCollider.gameObject.tag;
        
        Physics2D.IgnoreCollision(shooterCollider, projectileCollider);
        currentDelayLeft = shootDelay;
    }
}
