using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum PerkType {
    Regen,
    Reload,
    Health,
    Speed,
    Multi
}

public class Perk : MonoBehaviour, IInteractable {
    
    [SerializeField] private string objName;
    [SerializeField] private int price; 
    [SerializeField] private Color colour;
    [SerializeField] public UnityEvent effect;
    [SerializeField] public PerkType type;

    private bool perkAquired = false;
    
    public void Interact(FirstPersonInteraction interactor) {
        if (perkAquired == false) {
            if (price <= interactor.getPoints()) {
                interactor.setPoints(interactor.getPoints() - price);
                interactor.perks.Add(type);
                effect?.Invoke();
                AudioManager.Instance.Play(AudioManager.SoundType.Purchase);
                AudioManager.Instance.Play(AudioManager.SoundType.Drink);
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
