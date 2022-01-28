using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float speed;

    private void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void FixedUpdate()
    {
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget(){
        var newCameraPosition = CalculateNewPosition();
        transform.position = newCameraPosition;
    }

    private Vector3 CalculateNewPosition(){
        var cameraPosition = transform.position;
        var targetPosition = target.transform.position;
        var newPosition = Vector2.Lerp(cameraPosition, targetPosition, speed * Time.deltaTime);
        return newPosition.ToVector3(transform.position.z);
    }
}
