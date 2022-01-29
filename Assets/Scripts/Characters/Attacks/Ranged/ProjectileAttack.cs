using UnityEngine;

public class ProjectileAttack : MonoBehaviour {

    [SerializeField]
    private int damage;

    [SerializeField]
    private float speed = 3f, destroyAfterSeconds = 3f;

    [SerializeField]
    private Rigidbody2D rigidbody;

    private void Start() {
        Destroy(gameObject, destroyAfterSeconds);
    }

    private void Update() {
        MoveForward();
    }

    private void MoveForward(){
        Vector2 totalMove = transform.up * speed * Time.deltaTime;
        Vector2 newPosition = rigidbody.position + totalMove;
        rigidbody.position = newPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        HitOther(collision);
        Destroy(gameObject);
    }

    private void HitOther(Collision2D collision) {
        var target = collision.collider.GetComponent<Hittable>();
        if (target == null)
            return;

        var hitData = CreatehitData(collision);
        target.Hit(hitData);
    }

    private HitData CreatehitData(Collision2D collision) {
        return new HitData {
            damage = damage,
            collision = collision,
        };
    }
}