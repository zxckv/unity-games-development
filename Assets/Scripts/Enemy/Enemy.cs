using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    
    [SerializeField] float healthMax, healthCurrent = 10f;
    public GameObject criticalObject;
    public float criticalMultiplier;

    [SerializeField] EnemyHealthBar healthBar;

    public NavMeshAgent agent;
    public Transform target;

    void Awake() {
        healthBar = GetComponentInChildren<EnemyHealthBar>();
    }

    void Start() {
        healthBar.gameObject.SetActive(false);
        healthCurrent = healthMax;
        healthBar.UpdateHealthBar(healthCurrent, healthMax);
    }

    void Update() {
        // healthBar.gameObject.SetActive(!healthBar.gameObject.activeSelf);

        gameObject.transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        agent.SetDestination(target.position);
    }

    public void TakeDamage(float damage, GameObject objectHit) {
        if (objectHit == criticalObject) {
            healthCurrent -= damage * criticalMultiplier;
        } else {
            healthCurrent -= damage;
        }

        healthBar.gameObject.SetActive(true);
        healthBar.UpdateHealthBar(healthCurrent, healthMax);

        if (healthCurrent <= 0) {
            DestroyEnemy();
        }
    }

    public void DestroyEnemy() {
        Destroy(gameObject);
    }

    // public void ToggleHealthbar() {
    //     Debug.Log("Looking at enemy");
    //     healthBar.gameObject.SetActive(!healthBar.gameObject.activeSelf);
    // }
}
