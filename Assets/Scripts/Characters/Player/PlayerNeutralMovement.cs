using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerNeutralMovement : MonoBehaviour
{

    [SerializeField]
    private float force;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private Animator animator;

    private Vector2 direction = Vector2.zero;

    public void SetDirection(InputAction.CallbackContext context){
        direction = context.ReadValue<Vector2>().normalized;
    }

    private void FixedUpdate() {
        
        animator.SetInteger("Direction", (int) rb.velocity.x);
        MoveInSetDirection();
    }

    private void MoveInSetDirection() {
        rb.AddForce(direction * force, ForceMode2D.Force);
    }

}
