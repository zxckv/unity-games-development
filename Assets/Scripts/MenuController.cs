using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    [Header ("Levels to load")]
    public string nextScene;

    public void PlayGame() {
        SceneManager.LoadScene(nextScene);
    }

    public void ExitButton() {
        Application.Quit();
    }
}