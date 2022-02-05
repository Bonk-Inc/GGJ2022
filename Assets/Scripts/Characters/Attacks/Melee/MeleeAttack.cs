using System.Collections;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField]
    private float attackRadius = 1f, attackInterval = 1f;

    [SerializeField]
    private int damage = 1;

    [SerializeField] 
    private LayerMask mask;
    
    private void Awake() { 
        StartCoroutine(HitTargets());
    }

    private HitData CreateHitData(string attackingTag)
    {
        return new HitData
        {
            attackerTag = tag,
            attackingTag = attackingTag,
            damage = damage,
        };
    }
    
    private IEnumerator HitTargets()
    {
        while (true)
        {
            var hits = Physics2D.OverlapCircleAll(transform.position, attackRadius, mask);

            if(hits.Length == 0)
            {
                yield return new WaitForFixedUpdate();
                continue;
            }

            foreach (var hit in hits)
            {
                var target = hit.GetComponent<Hittable>();
                if (target == null)
                    continue;
                
                var hitData = CreateHitData(hit.gameObject.tag);
                target.Hit(hitData);
            }
            
            yield return new WaitForSeconds(attackInterval);   
        }
    }
}