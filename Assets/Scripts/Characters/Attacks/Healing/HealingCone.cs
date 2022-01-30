using UnityEngine;

public class HealingCone : Weapon
{
    private const string NpcTag = "Neutral";
    private const string EnemyTag = "Enemy";

    private const int NpcHealKarmaIncrease = 100;

    [SerializeField]
    private float coneAngle = 90;

    [SerializeField]
    private float minDistance = 0.5f , maxDistance = 5;

    [SerializeField]
    private float pushforce = 5;

    [SerializeField]
    private int healPerSecond = 2;

    [SerializeField]
    private SpriteRenderer visual;


    private float currentHealStamp = 0;
    private bool isCasting;

    public override void Attack(){}

    public override void Release()
    {
        visual.enabled = false;
        isCasting = false;
    }

    public override void Trigger()
    {
        currentHealStamp = 0;
        visual.enabled = true;
        isCasting = true;
    }

    private void Update() {
        currentHealStamp += Time.deltaTime;
        if(isCasting){
            CastCone();
        }
        if (currentHealStamp >= 1){
            currentHealStamp -= 1;
        }
    }

    private void CastCone(){
        var allInCone = Physics2DHelper.ConeOverlapAll(transform.position, transform.up, coneAngle, maxDistance, minDistance);
        foreach (var collider in allInCone)
        {
            if(currentHealStamp >= 1 && collider.CompareTag(NpcTag)) {
                collider.GetComponent<Hittable>().Hit(new HitData {damage= -healPerSecond});
                GameManager.instance.karma.Increase(NpcHealKarmaIncrease);
            } else if (collider.CompareTag(EnemyTag)){
                var pushDirection = (collider.transform.position - transform.position).normalized;
                collider.attachedRigidbody.AddForce(pushDirection * pushforce);
            }
        }
        
    }

    private void OnDrawGizmos() {
        var color = isCasting ? Color.green : Color.red;
        Gizmos.color = color;
        GizmoHelper.DrawCircleCone2D(transform.position, transform.up, maxDistance, minDistance, coneAngle);
    }

}


