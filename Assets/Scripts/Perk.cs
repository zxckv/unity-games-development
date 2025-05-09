using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perk : MonoBehaviour, IInteractable {
    
    [SerializeField] private string objName;
    [SerializeField] private int price; 
    [SerializeField] private Color colour;
    
    private bool perkAquired = false;

    public void Interact(FirstPersonInteraction interactor) {
        if (perkAquired == false) {
            if (price <= interactor.getPoints()) {
                interactor.setPoints(interactor.getPoints() - price);
                // interactor.perks.Add($"{objName}");
                perkAquired = true;
            }
        }
    }

    public void View(FirstPersonInteraction interactor) {
        if (perkAquired == false) {
            interactor.messageBox.SetText($"[E] Buy {objName} for {price}");
        }
    }
}
