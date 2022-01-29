using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;

    [SerializeField]
    private TargetPicker targetPicker;

    [SerializeField]
    private float speed = 10;

    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        targetPicker = GetComponent<TargetPicker>();
        
        targetPicker.onTargetChanged += ChangeTarget;
        target = targetPicker.MainTarget;
    }

    private void Update()
    {
        Vector2 dir = (target.position - transform.position).normalized;
        rb.position += (dir * Time.deltaTime * speed);
    }

    private void ChangeTarget(Transform newTarget) {
        target = newTarget;
    }
}
