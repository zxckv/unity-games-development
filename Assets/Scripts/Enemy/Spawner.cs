using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField] private Transform[] spawners;
    [SerializeField] private GameObject enemy;

    [SerializeField] private int round = 0; 
    [SerializeField] private int enemyCurrentCount = 0; 
    [SerializeField] private float enemyDelay = 1f; 

    [SerializeField] private float roundTransitionTime = 3f;
    [SerializeField] private float roundTransitionCountdown;

    private int enemiesThisRound;

    public static Spawner Instance;

    void Awake() {
        Instance = this;
    }

    void Start() {
        roundTransitionCountdown = roundTransitionTime;
    }

    void Update() {
        if (roundTransitionCountdown <= 0) {
            if (enemyCurrentCount == 0) {
                round += 1;
                FirstPersonMain.instance.GetComponent<FirstPersonMain>().currentRound = round;
                StartCoroutine(SpawnRound());
            }
            roundTransitionCountdown = roundTransitionTime;
        } else {
            roundTransitionCountdown -= Time.deltaTime;
        }
    }

    private IEnumerator SpawnRound() {
        for (int i = 0; i < CalcualteEnemies(); i++) {
            Spawn();
            yield return new WaitForSeconds(enemyDelay);
        }

        yield break;
    }

    private int CalcualteEnemies() {
        if (round == 0) { 
            return 0; 
        } else {
            return enemiesThisRound = 6 + ((round - 1) * 3);
        }
    }

    private void Spawn() {
        int randInt = Random.Range(0, spawners.Length);
        Instantiate(enemy, spawners[randInt].position, spawners[randInt].rotation);
        enemyCurrentCount += 1;
    }

    public void EnemyKilled() {
        enemyCurrentCount -= 1;
    }
}