using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public enum WeaponType {
    Pistol,
    Rifle,
    Shotgun,
    Submachinegun
}

public class Weapon : MonoBehaviour {

    [SerializeField] public GameObject obj;
    public WeaponType type;

    [SerializeField] public UnityEvent onWeaponFire;
    [SerializeField] public float weaponFireRate;
    [SerializeField] public bool weaponIsAuto;
    [SerializeField] public float weaponDamage;
    [SerializeField] public float weaponRange;
    [SerializeField] public int weaponMag;
    [SerializeField] public int weaponReserve;
    [SerializeField] public float weaponReloadTime;
    [SerializeField] public int weaponLevel = 1;

    private int weaponMagAmmo;
    private int weaponReserveAmmo;
    private bool isReloading;
    
    private float weaponCooldown;
    private float reloadCooldown;
    private Transform playerCamera;

    public AudioManager.SoundType weaponSound;

    void Start() {
        weaponCooldown = 1 / weaponFireRate;
        weaponMagAmmo = weaponMag;
        weaponReserveAmmo = weaponReserve;
        playerCamera = Camera.main.transform;

        FirstPersonMain.instance.GetComponent<FirstPersonMain>().ammoMag = weaponMagAmmo;
        FirstPersonMain.instance.GetComponent<FirstPersonMain>().ammoRes = weaponReserveAmmo;
        FirstPersonMain.instance.GetComponent<FirstPersonMain>().weaponTier = weaponLevel;
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

        if(Input.GetKeyDown(KeyCode.R) && ((weaponMagAmmo + weaponReserveAmmo) != 0)) {
            if ((weaponMagAmmo != weaponMag) && !isReloading) {
                weaponReload();
            }
        }

        FirstPersonMain.instance.GetComponent<FirstPersonMain>().ammoMag = weaponMagAmmo;
        FirstPersonMain.instance.GetComponent<FirstPersonMain>().ammoRes = weaponReserveAmmo;
        FirstPersonMain.instance.GetComponent<FirstPersonMain>().weaponTier = weaponLevel;

        weaponCooldown -= Time.deltaTime;
        if (isReloading) { reloadCooldown -= Time.deltaTime;  }
        if (reloadCooldown <= 0) { isReloading = false;  obj.GetComponent<MeshRenderer>().enabled = true; }
    }

    void weaponShoot() {
        AudioManager.Instance.Play(weaponSound);

        Ray weaponRaycast = new Ray(playerCamera.position, playerCamera.forward);
        if (Physics.Raycast(weaponRaycast, out RaycastHit weaponHitInfo, weaponRange)) {
            if (weaponHitInfo.collider.transform.root.TryGetComponent(out Enemy enemy)) {
                enemy.TakeDamage(calcDamage(), weaponHitInfo.collider.gameObject);
            }
        }
        
        weaponMagAmmo -= 1;
    }

    void weaponReload() {
        AudioManager.Instance.Play(AudioManager.SoundType.Reload);
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

    public void Refill() {
        Debug.Log("REFILL");
        weaponMagAmmo = weaponMag;
        weaponReserveAmmo = weaponReserve;
    }

    public void Upgrade() {
        weaponLevel += 1;
    }
}
