using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour {

    public TMP_Text kills;
    public TMP_Text highest;
    public TMP_Text games;

    void Start() {
        kills.SetText($"{PlayerPrefs.GetInt("kills", 0)}");
        highest.SetText($"{PlayerPrefs.GetInt("highest", 1)}");
        games.SetText($"{PlayerPrefs.GetInt("games", 0)}");
    }

    public void PlayGame(string scene) {
        SceneManager.LoadScene(scene);
    }

    public void ExitButton() {
        Application.Quit();
    }
}