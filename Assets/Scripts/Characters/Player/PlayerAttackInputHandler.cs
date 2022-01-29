using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackInputHandler : MonoBehaviour
{

    [SerializeField]
    private RangedWeapon weapon;

    public void OnAttack(InputAction.CallbackContext context){
        if(context.phase == InputActionPhase.Started) {
            weapon?.Shoot();
        }
    }
}
