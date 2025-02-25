using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMain : MonoBehaviour {
    
    public float healthMax = 10f;
    public float healthCurrent = 10f;

    public float speed = 3f;

    [SerializeField] EnemyHealthBar healthBar;

    void Awake() {
        healthBar = GetComponentInChildren<EnemyHealthBar>();
    }

    void Start() {
        healthCurrent = healthMax;
        healthBar.UpdateHealthBar(healthCurrent, healthMax);
    }

    void Update() {
        
    }

    public void TakeDamage(float damage) {
        healthCurrent -= damage;
        healthBar.UpdateHealthBar(healthCurrent, healthMax);

        if (healthCurrent <= 0) {
            KillEnemy();
        }
    }

    public void KillEnemy() {}
}
