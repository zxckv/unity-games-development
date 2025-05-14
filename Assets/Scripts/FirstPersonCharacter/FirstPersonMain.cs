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
    [SerializeField] public TMP_Text weaponTierTextBox;
    [SerializeField] public TMP_Text roundTextBox;

    [SerializeField] public PlayerHealthBar healthBar;

    [SerializeField] public int points;
    [SerializeField] public int currentRound;
    [SerializeField] public int ammoMag;
    [SerializeField] public int ammoRes;
    [SerializeField] public int weaponTier;

    [SerializeField] public float pointMult = 1;

    [SerializeField] public int healthMax = 100;
    [SerializeField] public int healthCurrent;
    [SerializeField] public bool isDead = false;

    [SerializeField] private float regenTimer = 0;
    [SerializeField] private float attackTimeReset = 3;

    private List<PerkType> perks;

    private bool hasSaved;

    public PauseController pc;
    public GameOver go;

    public int kills;
    public int rounds;

    public GameObject hand;
    public GameObject currentWeapon;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;

        healthCurrent = healthMax;
        healthBar.setMaxHealth(healthMax);

        kills = 0;
        rounds = 1;

        pointsTextBox.SetText("\\ " + points.ToString());
        ammoMagTextBox.SetText(ammoMag.ToString());
        ammoResTextBox.SetText("| " + ammoRes.ToString());
        weaponTierTextBox.SetText("TIER " + weaponTier.ToString());
        roundTextBox.SetText(currentRound.ToString());
    }   

    void Update() {
        if (Input.GetKeyDown(KeyCode.P)) { pc.Pause(); }
        if (healthCurrent <= 0) { Dead(); }
        if ((healthCurrent < healthMax) && (regenTimer <= 0)) { Regen(); }

        rounds = currentRound;

        pointsTextBox.SetText("\\ " + points.ToString());
        ammoMagTextBox.SetText(ammoMag.ToString());
        ammoResTextBox.SetText("| " + ammoRes.ToString());
        weaponTierTextBox.SetText("TIER " + weaponTier.ToString());
        roundTextBox.SetText(currentRound.ToString());

        regenTimer -= Time.deltaTime;
    }

    public void TakeDamage(int damage ) {
        AudioManager.Instance.Play(AudioManager.SoundType.Hit);
        healthCurrent -= damage;
        healthBar.setHealth(healthCurrent);
        regenTimer = attackTimeReset;
    }

    public void AddPoints(int add) {
        points += (int)(add * pointMult);
    }

    void Dead() {
        if (!hasSaved) { hasSaved = true; Save(); }
        go.SetGameOver();
    }

    private void Save() {
        int totalKills = PlayerPrefs.GetInt("kills", 0);
        PlayerPrefs.SetInt("kills", totalKills + kills);

        int highRound = PlayerPrefs.GetInt("highest", 1);
        if (rounds > highRound) { PlayerPrefs.SetInt("highest", rounds); }
        
        int games = PlayerPrefs.GetInt("games", 0);
        PlayerPrefs.SetInt("games", games += 1);

        PlayerPrefs.Save();
    }

    public void SwapWeapon(GameObject newWeapon) {
        if (currentWeapon.gameObject.GetComponent<Weapon>().type == newWeapon.GetComponent<Weapon>().type) {
            currentWeapon.gameObject.GetComponent<Weapon>().Refill();
        } else {
            var children = new List<Transform>();
            for (int i = 0; i < hand.transform.childCount; i++) {
                children.Add(hand.transform.GetChild(i));
            }

            for (int i = 0; i < children.Count; i++) {
                Destroy(children[i].gameObject);
            } 

            currentWeapon = Instantiate(newWeapon, hand.transform);   
        }
    }

    public void UpgradeWeapon() {
        currentWeapon.GetComponent<Weapon>().Upgrade();
    }

    void Regen() {
        Debug.Log("REGEN");
        if (healthCurrent + 5 <= healthMax) {
            healthCurrent += 5;
        } else {
            healthCurrent = healthMax;
        }

        regenTimer = 1;
        healthBar.setHealth(healthCurrent);
    }

    public void Perk_Regen() {
        Debug.Log("REGEN");
        attackTimeReset /= 2;
    }

    public void Perk_Health() {
        Debug.Log("HEALTH");
        healthMax = 150;
        healthCurrent = healthMax;
        healthBar.setHealth(healthCurrent);
    }

    public void Perk_Speed() {
        Debug.Log("SPEED");
        gameObject.GetComponent<FirstPersonMovement>().speed *= 1.25f;
    }

    public void Perk_Reload() {
        Debug.Log("RELOAD");
        currentWeapon.gameObject.GetComponent<Weapon>().weaponReloadTime /= 2;
    }

    public void Perk_Multi() {
        Debug.Log("MULTI");
        pointMult = 1.5f;
    }
}