using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackInputHandler : MonoBehaviour
{

    [SerializeField]
    private Weapon weapon;

    [SerializeField]
    private Animator animator;

    public void SelectWeapon(Weapon weapon){
        this.weapon = weapon;
    }

    public void OnAttack(InputAction.CallbackContext context){
        if(context.phase == InputActionPhase.Started) {
            animator.SetTrigger("Attack");
            weapon.Trigger();    
        } else if (context.phase == InputActionPhase.Canceled) {
            weapon.Release();
        }
    }
}
