using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{
    [SerializeField]
    private float damage, speed = 3f, destroyAfterSeconds = 3f;

    [SerializeField] 
    private Rigidbody2D rigidbody;

    private void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    private void Update()
    {
        rigidbody.velocity = Vector2.up * speed * Time.deltaTime;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(gameObject);
    }
}