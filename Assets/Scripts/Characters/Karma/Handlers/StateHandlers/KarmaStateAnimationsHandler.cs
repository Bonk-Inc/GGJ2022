using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarmaStateAnimationsHandler : KarmaStateChangeHandler
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private RuntimeAnimatorController angelController, demonController;

    protected override void OnAngelState(KarmaState prevState) {
        animator.runtimeAnimatorController = angelController;
    }
    protected override void OnDemonState(KarmaState prevState) {
        animator.runtimeAnimatorController = demonController;
    }
}
