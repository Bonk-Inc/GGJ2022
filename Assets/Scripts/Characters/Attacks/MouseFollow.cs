using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseFollow : MonoBehaviour
{

    private Vector2 currentMousePosition;

    public void UpdateMousePosition(InputAction.CallbackContext context){
        currentMousePosition = context.ReadValue<Vector2>();
    }

    private void Update() {
        Camera cam = Camera.main;

        if(cam == null)
            return;

        Vector2 worldposition = (Vector2)cam.ScreenToWorldPoint(currentMousePosition.ToVector3(transform.position.z));
        transform.position = worldposition.ToVector3(transform.position.z);
    }
}
