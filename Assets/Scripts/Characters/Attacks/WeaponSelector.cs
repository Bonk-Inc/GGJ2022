using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSelector : MonoBehaviour {

    [SerializeField]
    private PlayerAttackInputHandler weaponHandler;

    private List<Weapon> weapons;
    private int currentWeapon = 0;

    public void Start() {
        weapons = new List<Weapon>();
        for (var i = 0; i < transform.childCount; i++) {
            var child = transform.GetChild(i);
            var weapon = child.GetComponent<Weapon>();

            if (weapon == null)
                continue;

            child.gameObject.SetActive(false);
            weapons.Add(weapon);
        }
        SelectNewWeapon(currentWeapon);

    }

    public void SelectWeaponInput(InputAction.CallbackContext context) {

        if (context.performed) {
            var direction = context.ReadValue<Vector2>();
            if (direction.y > 0) {
                SelectNextWeapon();
            } else if (direction.y < 0) {
                SelectPrevWeapon();
            }
        }
    }

    private void SelectNextWeapon() {
        var nextWeapon = currentWeapon + 1;
        nextWeapon %= weapons.Count;
        SelectNewWeapon(nextWeapon);
    }

    private void SelectPrevWeapon() {
        var prevWeapon = currentWeapon - 1;
        if (prevWeapon < 0) {
            prevWeapon = weapons.Count - 1;
        }
        SelectNewWeapon(prevWeapon);
    }

    private void SelectNewWeapon(int index) {
        Debug.Log(index);
        Weapon previousWeapon = weapons[currentWeapon];
        previousWeapon.gameObject.SetActive(false);

        Weapon weapon = weapons[index];
        weapon.gameObject.SetActive(true);

        weaponHandler.SelectWeapon(weapon);
        currentWeapon = index;
    }

}