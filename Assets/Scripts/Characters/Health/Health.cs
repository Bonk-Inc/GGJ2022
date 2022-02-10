using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    
    [SerializeField]
    private int maxHealth;

    [SerializeField]
    private int initialHealth;

    public int CurrentHealth { get; private set; }
    public int MaxHealth => maxHealth;

    public bool IsDead => CurrentHealth <= 0;

    public event EventHandler<HealthChangeArgs> OnHealthChange;

    private void Awake() {
        CurrentHealth = initialHealth;
    }

    public void SetHealth(int health){
        int prevHealth = CurrentHealth;
        CurrentHealth = Math.Min(health, maxHealth);
        CallEvent(prevHealth, CurrentHealth);
    }

    public void Heal(int amount){
        SetHealth(CurrentHealth + amount);
    }
    
    public void Damage(int amount){
        SetHealth(CurrentHealth - amount);
    }

    public void Kill(){
        SetHealth(0);
    }

    private void CallEvent(int previousHealth, int newHealth){
        HealthChangeArgs eventArgs = new HealthChangeArgs() {
            newHealth = newHealth,
            previousHealth = previousHealth
        };
        OnHealthChange?.Invoke(this, eventArgs);
    }

    public class HealthChangeArgs
    {
        public bool IsDead => newHealth <= 0;

        public int newHealth;
        public int previousHealth;
    }

}
