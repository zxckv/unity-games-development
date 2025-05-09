using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField] private Transform[] spawners;
    [SerializeField] private GameObject enemy;

    [SerializeField] private int round = 1; 
    [SerializeField] private int enemyMaxCount = 20; 
    [SerializeField] private int enemyCurrentCount = 0; 
    [SerializeField] private float enemyDelay = 1f; 

    [SerializeField] private float roundTransitionTime = 3f;
    [SerializeField] private float roundTransitionCountdown;

    private int enemiesThisRound;

    void Start() {
        roundTransitionCountdown = roundTransitionTime;
    }

    void Update() {
        if (roundTransitionCountdown <= 0) {
            if (enemyCurrentCount != CalcualteEnemies()) {
                StartCoroutine(SpawnRound());
            }
            roundTransitionCountdown = roundTransitionTime;
        } else {
            roundTransitionCountdown -= Time.deltaTime;
        }
    }

    private IEnumerator SpawnRound() {
        for (int i = 0; i < CalcualteEnemies(); i++) {
            if (enemyCurrentCount < enemyMaxCount) {
                Spawn();
                yield return new WaitForSeconds(enemyDelay);
            }
        }

        yield break;
    }

    private int CalcualteEnemies() {
        return enemiesThisRound = 6 + ((round - 1) * 3);
    }

    private void Spawn() {
        int randInt = Random.Range(0, spawners.Length);
        Instantiate(enemy, spawners[randInt].position, spawners[randInt].rotation);
        enemyCurrentCount += 1;
    }
}

// Repeat this until Round = 0
// Need to track the zombies killed
//
// if (Round < Limit) {
//     Spawn(Round);
// } else {
//     Round -= Limit;
//     Spawn(Limit);
// }