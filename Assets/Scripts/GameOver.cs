using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    public GameObject panel;

    public TMP_Text killsText;
    public TMP_Text roundsText;

    public void Exit() {
        AudioManager.Instance.Play(AudioManager.SoundType.Click);
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void SetGameOver() {
        int kills = FirstPersonMain.instance.GetComponent<FirstPersonMain>().kills;
        int rounds = FirstPersonMain.instance.GetComponent<FirstPersonMain>().rounds;

        killsText.SetText($"{kills} kills");
        roundsText.SetText($"You survived {rounds} rounds");

        panel.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
