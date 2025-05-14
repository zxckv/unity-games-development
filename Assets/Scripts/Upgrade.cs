using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour, IInteractable {
    
    [SerializeField] private string objName;
    [SerializeField] private int price; 
    
    public void Interact(FirstPersonInteraction interactor) {
        if (price <= interactor.getPoints()) {
            interactor.setPoints(interactor.getPoints() - price);
            FirstPersonMain.instance.GetComponent<FirstPersonMain>().UpgradeWeapon();
            AudioManager.Instance.Play(AudioManager.SoundType.Purchase);
            AudioManager.Instance.Play(AudioManager.SoundType.Oven);
        }
    }

    public void View(FirstPersonInteraction interactor) {
        interactor.messageBox.SetText($"[E] Buy {objName} for {price}");
    }
}
