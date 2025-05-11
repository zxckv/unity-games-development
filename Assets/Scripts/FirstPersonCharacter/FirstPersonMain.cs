using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FirstPersonMain : MonoBehaviour {

    #region
    public static GameObject instance;

    private void Awake() {
        instance = this.gameObject;
    }
    #endregion

    [SerializeField] public TMP_Text pointsTextBox;
    [SerializeField] public TMP_Text ammoMagTextBox;
    [SerializeField] public TMP_Text ammoResTextBox;

    [SerializeField] public PlayerHealthBar healthBar;

    [SerializeField] public int points;
    [SerializeField] public int ammoMag;
    [SerializeField] public int ammoRes;

    [SerializeField] public int healthMax = 100;
    [SerializeField] public int healthCurrent;

    public PauseController pc;

    void Start() {
        healthCurrent = healthMax;
        healthBar.setMaxHealth(healthMax);

        pointsTextBox.SetText("\\ " + points.ToString());
        // ammoMagTextBox.SetText(currentWeapon.weaponMagAmmo.ToString());
        // ammoResTextBox.SetText(currentWeapon.weaponReserveAmmo.ToString());
    }   

    void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            TakeDamage(10);
        } else if (Input.GetKeyDown(KeyCode.P)) {
            pc.Pause();
            
        }

        pointsTextBox.SetText("\\ " + points.ToString());
        // ammoMagTextBox.SetText(currentWeapon.weaponMagAmmo.ToString());
        // ammoResTextBox.SetText(currentWeapon.weaponReserveAmmo.ToString());
    }

    void TakeDamage(int damage ) {
        healthCurrent -= damage;
        healthBar.setHealth(healthCurrent);
    }

    public void AddPoints(int add) {
        Debug.Log("Points added");
        points += add;
    }
}