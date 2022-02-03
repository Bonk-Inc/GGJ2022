using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;

    [SerializeField]
    private TargetPicker targetPicker;

    [SerializeField]
    private float slowDownSpeed = 0.8f;

    private Rigidbody2D rb;
    private NavMeshAgent agent;

    private void Awake() {
        targetPicker = GetComponent<TargetPicker>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody2D>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
        
        targetPicker.onTargetChanged += ChangeTarget;
        ChangeToDefault();
    }

    private void Update()
    {
        if(target == null) ChangeToDefault();
        agent.SetDestination(target.position);
    }

    private void ChangeTarget(Transform newTarget) {
        target = newTarget;
    }

    private void ChangeToDefault() {
        target = targetPicker.MainTarget;
    }
}
