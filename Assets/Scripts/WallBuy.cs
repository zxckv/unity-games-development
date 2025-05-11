using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallbuy : MonoBehaviour, IInteractable {
    
    [SerializeField] private string objName;
    [SerializeField] private GameObject obj;
    [SerializeField] private int price; 
    
    public void Interact(FirstPersonInteraction interactor) {
        if (price <= interactor.getPoints()) {
            interactor.setPoints(interactor.getPoints() - price);
            interactor.setWeapon(obj);            
        }
    }

    public void View(FirstPersonInteraction interactor) {
        interactor.messageBox.SetText($"[E] Buy {objName} for {price}");
    }
}
