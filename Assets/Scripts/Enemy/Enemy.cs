using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : MonoBehaviour {

    [SerializeField] float healthMax, healthCurrent = 10f;
    public GameObject criticalObject;
    public float criticalMultiplier;

    [SerializeField] EnemyHealthBar healthBar;

    public UnityEvent onDeath;

    public NavMeshAgent agent;
    public Transform target;

    [SerializeField] Animator anim;

    void Awake() {
        healthBar = GetComponentInChildren<EnemyHealthBar>();
        target = FirstPersonMain.instance.transform;
    }

    void Start() {
        healthBar.gameObject.SetActive(false);
        healthCurrent = healthMax;
        healthBar.UpdateHealthBar(healthCurrent, healthMax);
        anim.Play("ZombieWalk", 0, 0.0f);
        anim.speed = Random.Range(1, 2);
    }

    void Update() {
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
        onDeath?.Invoke();
        Destroy(gameObject);
    }

}
