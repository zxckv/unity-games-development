using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private GameObject fpmain = FirstPersonMain.instance;

    public LayerMask whatIsPlayer;

    [SerializeField] private SphereCollider col;

    public int attackDamage;
    public float attackTime;
    public float attackRange;
    public bool hasAttacked = false;
    public bool inRange;

    void Awake() {
        healthBar = GetComponentInChildren<EnemyHealthBar>();
        target = fpmain.transform;
    }

    void Start() {
        healthBar.gameObject.SetActive(false);
        healthCurrent = healthMax;
        healthBar.UpdateHealthBar(healthCurrent, healthMax);
        anim.Play("ZombieWalk", 0, 0.0f);
        anim.speed = Random.Range(1f, 2f);

        if (Random.Range(0, 10) == 0) { AudioManager.Instance.Play(AudioManager.SoundType.Zombie); }
    }

    void Update() {
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        agent.SetDestination(target.position);
    }

    public void TakeDamage(float damage, GameObject objectHit) {
        if (objectHit == criticalObject) {
            healthCurrent -= damage * criticalMultiplier;
        } else {
            healthCurrent -= damage;
        }

        fpmain.GetComponent<FirstPersonMain>().AddPoints(10);

        healthBar.gameObject.SetActive(true);
        healthBar.UpdateHealthBar(healthCurrent, healthMax);

        if (healthCurrent <= 0) {
            fpmain.GetComponent<FirstPersonMain>().AddPoints(100);
            fpmain.GetComponent<FirstPersonMain>().kills += 1;
            DestroyEnemy();
        }
    }

    public void DestroyEnemy() {
        onDeath?.Invoke();
        Spawner.Instance.EnemyKilled();
        Destroy(gameObject);
    }

    private void Attack() {
        if (!hasAttacked) {
            fpmain.GetComponent<FirstPersonMain>().TakeDamage(attackDamage);

            hasAttacked = true;
            Invoke(nameof(ResetAttack), attackTime);
        }
    }

    private void ResetAttack() {
        hasAttacked = false;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void OnTriggerEnter(Collider other) {
        Attack();
    }

}
