using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour, IInteractable {
    
    [SerializeField] private string objName;
    [SerializeField] private int price; 
    [SerializeField] private Color colour;
    
    private bool aquired = false;

    public void Interact(FirstPersonInteraction interactor) {
        if (price <= interactor.getPoints()) {
            interactor.setPoints(interactor.getPoints() - price);
            // Upgrade weapon
            aquired = true;
        }
    }

    public void View(FirstPersonInteraction interactor) {
        if (aquired == false) {
            interactor.messageBox.SetText($"[E] Buy {objName} for {price}");
        }
    }
}
