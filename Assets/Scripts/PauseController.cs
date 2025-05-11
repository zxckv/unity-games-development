using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour {

    public GameObject panel;

    void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            Pause();
        }
    }

    public void Pause() {
        panel.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Continue() {
        panel.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Exit() {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
