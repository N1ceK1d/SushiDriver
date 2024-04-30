using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameObject pause;
    public GameObject pauseButton;

    private void Awake() {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        if(pause != null)
        {
            pause.SetActive(false);
        }
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pause.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void Play()
    {
        Time.timeScale = 1f;
        pause.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void ToProfile()
    {
        SceneManager.LoadScene("Profile");
    }
}
