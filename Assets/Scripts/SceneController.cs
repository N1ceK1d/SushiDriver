using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class SceneController : MonoBehaviour
{
    public GameObject pause;
    public GameObject pauseButton;

    private void Awake() {
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
        Time.timeScale = 1f;
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

    public void EndGame()
    {
        SceneManager.LoadScene("Profile");
        if(PlayerPrefs.HasKey("user_id"))
        {
            Text score = GameObject.Find("Score").GetComponent<Text>();
            int resultString;
            int.TryParse(Regex.Match(score.text, @"\d+").Value, out resultString);

            PlayerPrefs.SetInt("Scrote", PlayerPrefs.GetInt("Scrote") + resultString);
            WWWForm regForm = new WWWForm();
            regForm.AddField("user_id", PlayerPrefs.GetInt("user_id"));
		    regForm.AddField("score", resultString);

		    WWW www = new WWW("http://sushidriver/php/updateScore.php", regForm);
		    StartCoroutine(UpdateFunc(www));
        }
    }

    private IEnumerator UpdateFunc(WWW www)
	{
		yield return www;
        Debug.Log(www.text);
		if(www.error != null)
		{
			Debug.Log("Ошибка: " + www.error);
			yield break;
		}
	}
}
