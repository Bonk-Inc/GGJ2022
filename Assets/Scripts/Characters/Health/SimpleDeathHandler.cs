using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDeathHandler : MonoBehaviour
{

    [SerializeField]
    private Health health;

    private void Awake() {
        health.OnHealthChange += OnHealthChange;
    }

    private void OnHealthChange(object caller, Health.HealthChangeArgs args){
        if(args.IsDead){
            Destroy(gameObject);
        }
    }
    
}
