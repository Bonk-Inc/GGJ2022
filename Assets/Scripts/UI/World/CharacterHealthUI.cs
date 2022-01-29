using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHealthUI : MonoBehaviour
{
    [SerializeField]
    private Health health;

    [SerializeField]
    private Image healthBar;

    private void Start() {
        health.OnHealthChange += (caller, args) => {
            UpdateUI();
        };
        UpdateUI();
    }

    private void UpdateUI(){
        healthBar.fillAmount = 1f / health.MaxHealth * health.CurrentHealth;
    }

}
