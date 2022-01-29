using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SimpleSemiAutoWeapon : Weapon
{
    public override void Release(){}
    public override void Trigger()
    {
        Attack();
    }
}
