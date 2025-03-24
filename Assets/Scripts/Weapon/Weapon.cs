using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour {

    // public UnityEvent onWeaponFire;
    public float weaponFireRate;
    public bool weaponIsAuto;
    public float weaponDamage;
    public float weaponRange;
    public float weaponMag;
    public float weaponReserve;

    private float weaponMagAmmo;
    private float weaponReserveAmmo;
    private float weaponCooldown;
    private Transform playerCamera;

    void Start() {
        weaponCooldown = 1 / weaponFireRate;
        weaponMagAmmo = weaponMag;
        weaponReserveAmmo = weaponReserve;
        playerCamera = Camera.main.transform;
    }

    void Update() {
        if (weaponIsAuto) {
            if (Input.GetButton("Fire")) {
                if (weaponCooldown <= 0f) {
                    // onWeaponFire?.Invoke();
                    weaponShoot();
                    weaponCooldown = 1 / weaponFireRate;
                }
            }
        } else {
            if (Input.GetButtonDown("Fire")) {
                if (weaponCooldown <= 0f) {
                    // onWeaponFire?.Invoke();
                    weaponShoot();
                    weaponCooldown = 1 / weaponFireRate;
                }
            }
        }

        weaponCooldown -= Time.deltaTime;
    }

    void weaponShoot() {
        Ray weaponRaycast = new Ray(playerCamera.position, playerCamera.forward);
        if (Physics.Raycast(weaponRaycast, out RaycastHit weaponHitInfo, weaponRange)) {
            if (weaponHitInfo.collider.transform.root.TryGetComponent(out Enemy enemy)) {
                enemy.TakeDamage(weaponDamage, weaponHitInfo.collider.gameObject);
            }
        }
    }
}
