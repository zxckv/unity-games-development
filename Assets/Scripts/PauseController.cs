using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour {

    public GameObject panel;

    public void Pause() {
        panel.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Continue() {
        AudioManager.Instance.Play(AudioManager.SoundType.Click);
        panel.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Exit() {
        AudioManager.Instance.Play(AudioManager.SoundType.Click);
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
