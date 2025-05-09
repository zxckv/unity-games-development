using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Numerics;

public interface IInteractable {
    public void Interact(FirstPersonInteraction interactor);
    public void View(FirstPersonInteraction interactor);
}

public class FirstPersonInteraction : MonoBehaviour {

    [SerializeField] public FirstPersonMain main;
    
    [SerializeField] public TMP_Text messageBox;
    [SerializeField] public Transform source;
    [SerializeField] public float range = 1.5f;

    void Start() {
        FirstPersonInteraction interactor = new FirstPersonInteraction();
        interactor = this;
    }

    void Update() {
        Ray r = new Ray(source.position, source.forward);
        if (Physics.Raycast(r, out RaycastHit hitInfo, range)) {
            Debug.DrawRay(source.position, source.forward, Color.green);  
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable obj)) {
                if (Input.GetKeyDown(KeyCode.E)) {
                    obj.Interact(this);
                    messageBox.SetText("");
                } else {
                    obj.View(this);
                }
            } else {
                messageBox.SetText("");
            }
        } else {
            messageBox.SetText("");
        }
    }

    public int getPoints() {
        return main.points;
    }

    public void setPoints(int newPoints) {
        main.points = newPoints;
    }

    public void addPoints(int add) {
        main.points += add;
    }
}
