using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampiricWeapon : Weapon
{
    
    [SerializeField]
    private List<VampiricTargetData> targetsHitActionData;

    [SerializeField]
    private ColliderTargeter targeter;

    [SerializeField]
    private Health myHealth;

    [SerializeField]
    private float secondsBetweenHits = 1;

    [SerializeField]
    private SpriteRenderer areaVisual;
    
    private Coroutine hitRoutine;

    public override void Attack()
    {}

    public override void Trigger()
    {
        StartHitting();
    }

    public override void Release()
    {
        StopHitting();
    }

    private void OnDisable() {
        StopHitting();
    }

    private void StartHitting(){
        areaVisual.enabled = true;
        hitRoutine = StartCoroutine(HitRoutine());
    }

    private void StopHitting(){
        if(hitRoutine != null){
            StopCoroutine(hitRoutine);
        }
        areaVisual.enabled = false;
    }

    private IEnumerator HitRoutine(){
        while (true)
        {
            yield return new WaitForSeconds(secondsBetweenHits);
            HitTargets();
        }
    }


    private void HitTargets(){
        var targets = new List<Collider2D>(targeter.CurrentTargets);
        for (var i = 0; i < targets.Count; i++)
        {
            var target = targets[i];
            CheckForHittableTarget(target);
        }
    }

    private void CheckForHittableTarget(Collider2D target){
        foreach (var possibleHit in targetsHitActionData)
        {
            if(!target.CompareTag(possibleHit.Tag))
                continue;

            HitTarget(target, possibleHit);
        }
    }

    private void HitTarget(Collider2D target, VampiricTargetData vampiricHitData){
        Hittable hittable = target.GetComponent<Hittable>();
        if(hittable == null)
            return;
            
        hittable.Hit( HitData.FromDamage(vampiricHitData.DamageTotarget) );
        myHealth.Heal( vampiricHitData.HealthStealAmount );
        GameManager.instance.karma.Increase(vampiricHitData.KarmaChangeAmount);
    }



    [System.Serializable]
    private class VampiricTargetData {
        [SerializeField, Tag]
        private string targetTag;

        [SerializeField]
        private int damageToTarget;

        [SerializeField]
        private int healthStealAmount;

        [SerializeField]
        private int karmaChangeAmount;

        public string Tag => targetTag;
        public int HealthStealAmount => healthStealAmount;
        public int DamageTotarget => damageToTarget;
        public int KarmaChangeAmount => karmaChangeAmount;
    }

}
