using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimInputHandler : MonoBehaviour
{

    public void UpdateAim(InputAction.CallbackContext context){
        Camera cam = Camera.current;

        if(cam == null)
            return;

        Vector2 mousePosition = context.ReadValue<Vector2>();
        Vector2 worldposition = (Vector2)cam.ScreenToWorldPoint(mousePosition.ToVector3(transform.position.z));
        Vector2 thisPosition = (Vector2)transform.position;
        float angle = Vector2.SignedAngle(Vector2.up, worldposition - thisPosition);
        SetRotationZ(angle);
    }

    public void SetRotationZ(float z){
        var rotation = transform.rotation.eulerAngles;
        rotation.z = z;
        transform.rotation = Quaternion.Euler(rotation);
    }

}
