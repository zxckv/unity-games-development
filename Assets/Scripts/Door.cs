using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable {

    [SerializeField] private string objName;
    [SerializeField] private int price; 

    public void Interact(FirstPersonInteraction interactor) {
        Debug.Log($"{objName} interaction.");

        if (price <= interactor.getPoints()) {
            interactor.setPoints(interactor.getPoints() - price);
            AudioManager.Instance.Play(AudioManager.SoundType.Purchase);
            Destroy(gameObject);
        }
    }

    public void View(FirstPersonInteraction interactor) {
        interactor.messageBox.SetText($"[E] Buy {objName} for {price}");
    }

}
