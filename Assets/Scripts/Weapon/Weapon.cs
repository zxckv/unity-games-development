using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour {

    [SerializeField] public GameObject obj;

    public UnityEvent onWeaponFire;
    public float weaponFireRate;
    public bool weaponIsAuto;
    public float weaponDamage;
    public float weaponRange;
    public int weaponMag;
    public int weaponReserve;
    public float weaponReloadTime;
    public int weaponLevel = 1;

    private int weaponMagAmmo;
    private int weaponReserveAmmo;
    private bool isReloading;
    
    private float weaponCooldown;
    private float reloadCooldown;
    private Transform playerCamera;

    public TMP_Text ammoMagTextBox;
    public TMP_Text ammoResTextBox;
    public TMP_Text weaponTierTextBox;

    void Start() {
        weaponCooldown = 1 / weaponFireRate;
        weaponMagAmmo = weaponMag;
        weaponReserveAmmo = weaponReserve;
        playerCamera = Camera.main.transform;

        ammoMagTextBox.SetText(weaponMagAmmo.ToString());
        ammoResTextBox.SetText("| " + weaponReserveAmmo.ToString());
        weaponTierTextBox.SetText("TIER " + weaponLevel.ToString());
    }

    void Update() {
        if (weaponIsAuto) {
            if (Input.GetButton("Fire")) {
                if (weaponCooldown <= 0f) {
                    if ((weaponMagAmmo > 0) && (!isReloading)) {
                        onWeaponFire?.Invoke();
                        weaponShoot();
                        weaponCooldown = 1 / weaponFireRate;
                    } 
                }
            }
        } else {
            if (Input.GetButtonDown("Fire")) {
                if (weaponCooldown <= 0f) {
                    if ((weaponMagAmmo > 0) && (!isReloading)) {
                        onWeaponFire?.Invoke();
                        weaponShoot();
                        weaponCooldown = 1 / weaponFireRate;
                    }
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.R)) {
            if ((weaponMagAmmo != weaponMag) && !isReloading) {
                weaponReload();
            }
        }

        ammoMagTextBox.SetText(weaponMagAmmo.ToString());
        ammoResTextBox.SetText("| " + weaponReserveAmmo.ToString());

        weaponCooldown -= Time.deltaTime;
        if (isReloading) { reloadCooldown -= Time.deltaTime;  }
        if (reloadCooldown <= 0) { isReloading = false; obj.GetComponent<MeshRenderer>().enabled = true; }
    }

    void weaponShoot() {
        Ray weaponRaycast = new Ray(playerCamera.position, playerCamera.forward);
        if (Physics.Raycast(weaponRaycast, out RaycastHit weaponHitInfo, weaponRange)) {
            if (weaponHitInfo.collider.transform.root.TryGetComponent(out Enemy enemy)) {
                enemy.TakeDamage(calcDamage(), weaponHitInfo.collider.gameObject);
            }
        }
        
        weaponMagAmmo -= 1;
    }

    void weaponReload() {
        obj.GetComponent<MeshRenderer>().enabled = false;

        isReloading = true;
        reloadCooldown = weaponReloadTime;

        weaponReserveAmmo += weaponMagAmmo;
        if ((weaponReserveAmmo - weaponMag) > 0) {
            weaponMagAmmo = weaponMag;
            weaponReserveAmmo -= weaponMag;
        } else {
            weaponMagAmmo = weaponReserveAmmo;
            weaponReserveAmmo = 0;
        }
    }

    float calcDamage() {
        return weaponDamage + ((weaponDamage * weaponLevel) / 2);
    }
}
