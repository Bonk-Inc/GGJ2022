using UnityEngine;

public class HealingCone : Weapon
{

    [SerializeField]
    private int NpcHealKarmaIncrease = 100;

    [SerializeField]
    private float coneAngle = 90;

    [SerializeField]
    private float coneStartDistance = 0.5f , coneEndDistance = 5;

    [SerializeField]
    private float pushforce = 5;

    [SerializeField]
    private int healPerSecond = 2;

    [SerializeField]
    private SpriteRenderer coneVisualRepresentation;

    private float currentTimeSincePossibleHeal = 0;
    private bool isCasting;

    public override void Attack(){}

    public override void Release()
    {
        coneVisualRepresentation.enabled = false;
        isCasting = false;
    }

    public override void Trigger()
    {
        currentTimeSincePossibleHeal = 0;
        coneVisualRepresentation.enabled = true;
        isCasting = true;
    }

    private void Update() {
        currentTimeSincePossibleHeal += Time.deltaTime;
        if(isCasting){
            CastCone();
        }
        if (currentTimeSincePossibleHeal >= 1){
            currentTimeSincePossibleHeal -= 1;
        }
    }

    private void CastCone(){
        var allInCone = Physics2DHelper.ConeOverlapAll(transform.position, transform.up, coneAngle, coneEndDistance, coneStartDistance);
        foreach (var collider in allInCone)
        {
             CheckHit(collider);
        }
    }

    private void CheckHit(Collider2D collider){
        if(currentTimeSincePossibleHeal >= 1 && collider.CompareTag(Tags.npc)) {
            HandleNpcHit(collider);
        } else if (collider.CompareTag(Tags.enemy)){
            HandleEnemyHit(collider);
        }
    }

    private void HandleNpcHit(Collider2D collider){
            collider.GetComponent<Hittable>().Hit(new HitData {damage= -healPerSecond});
            GameManager.instance.karma.Increase(NpcHealKarmaIncrease);
    }

    private void HandleEnemyHit(Collider2D collider){
        var pushDirection = (collider.transform.position - transform.position).normalized;
        collider.attachedRigidbody.AddForce(pushDirection * pushforce);
    }

    private void OnDrawGizmos() {
        var color = isCasting ? Color.green : Color.red;
        Gizmos.color = color;
        GizmoHelper.DrawCircleCone2D(transform.position, transform.up, coneEndDistance, coneStartDistance, coneAngle);
    }

}


