using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarmaStateMovementHandler : KarmaStateChangeHandler
{
    [SerializeField]
    private PlayerMovement movement;

    [SerializeField]
    private float angelForce, demonForce, humanForce;

    protected override void OnAngelState(KarmaState prevState) {
        movement.Force = angelForce;
    }
    protected override void OnDemonState(KarmaState prevState) {
        movement.Force = demonForce;
    }
    protected override void OnHumanState(KarmaState prevState) {
        movement.Force = humanForce;
    }
}
