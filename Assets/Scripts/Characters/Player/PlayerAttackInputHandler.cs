using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackInputHandler : MonoBehaviour
{

    [SerializeField]
    private Weapon weapon;

    public void SelectWeapon(Weapon weapon){
        this.weapon = weapon;
    }

    public void OnAttack(InputAction.CallbackContext context){
        if(context.phase == InputActionPhase.Started) {
            weapon.Trigger();    
        } else if (context.phase == InputActionPhase.Canceled) {
            weapon.Release();
        }
    }
}
