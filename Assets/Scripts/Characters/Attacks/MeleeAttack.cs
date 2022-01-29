using System;
using System.Collections;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{ 
    private Coroutine attackCoroutine;
    
    public event Action onTargetHit;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        attackCoroutine = StartCoroutine(HitTarget());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        StopCoroutine(attackCoroutine);
    }

    private IEnumerator HitTarget()
    {
        while (true)
        {
            onTargetHit?.Invoke();

            yield return new WaitForSeconds(1f);   
        }
    }
}
