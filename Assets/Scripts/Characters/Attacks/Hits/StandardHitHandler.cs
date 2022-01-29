using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardHitHandler : Hittable
{
    
    [SerializeField]
    private Health health;

    public override void Hit(HitData hitData)
    {
        int hitDamage = hitData.damage;
        health.Damage(hitDamage);
    }

}
