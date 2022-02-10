using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTargeter : MonoBehaviour
{
    
    private HashSet<Collider2D> colliders = new HashSet<Collider2D>();

    public HashSet<Collider2D> CurrentTargets => colliders;

    private void OnTriggerEnter2D(Collider2D other) {
        colliders.Add(other);
    }

    private void OnTriggerExit2D(Collider2D other) {
        colliders.Remove(other);
    }

}
