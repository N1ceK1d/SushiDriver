using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    public GameObject playerData;
    public GameObject loginBtn;
    public GameObject exitBtn;
    public Text playerName;
    public Text playerScore;
    private void Awake()
    {
        CheckLoggined();
        PlayerPrefs.SetInt("car_select", 0);
    }

    private void Update()
    {
        CheckLoggined();
    }

    public void CheckLoggined()
    {
        if(PlayerPrefs.HasKey("user_id"))
        {
            playerData.SetActive(true);
            exitBtn.SetActive(true);
            loginBtn.SetActive(false);
            playerName.text = PlayerPrefs.GetString("Email");
            playerScore.text = "Очки: " + PlayerPrefs.GetInt("Scrote").ToString();
        } else 
        {
            playerData.SetActive(false);
            exitBtn.SetActive(false);
            loginBtn.SetActive(true);
        }
    }
    public void StartGame()
    {
        if((PlayerPrefs.HasKey("car_prefab") && PlayerPrefs.GetString("car_prefab") != null) && PlayerPrefs.HasKey("user_id") && PlayerPrefs.GetInt("car_select") == 1)
        {
            SceneManager.LoadScene("SampleScene");
        } else 
        {
            SceneManager.LoadScene("GuestScene");
        }
        
    }

    public void ExitGame()
    {
        PlayerPrefs.DeleteAll();
        CheckLoggined();
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene("Game");
    }
}
