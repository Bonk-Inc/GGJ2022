using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;

    [SerializeField]
    private TargetPicker targetPicker;

    private NavMeshAgent agent;

    private void Awake() {
        targetPicker = GetComponent<TargetPicker>();
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
        
        targetPicker.onTargetChanged += ChangeTarget;
        target = targetPicker.MainTarget;
    }

    private void Update()
    {
        agent.SetDestination(target.position);
    }

    private void ChangeTarget(Transform newTarget) {
        target = newTarget;
    }
}
