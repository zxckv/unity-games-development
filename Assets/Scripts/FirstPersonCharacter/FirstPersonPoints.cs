using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonPoints : MonoBehaviour
{

    public int currentPoints = 500;
    private string textPoints;

    void Start() {

    }

    void Update() {
        textPoints = currentPoints.ToString();
    }

    void addPoints(int points) {
        currentPoints += points;
    }
    
    bool subPoints(int points) {
        if (points >= currentPoints) {
            currentPoints -= points;
            return true;
        } else {
            return false;
        }
    }

}
