using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    public GameObject playerData;
    public GameObject loginBtn;
    public GameObject exitBtn;
    public GameObject garageBtn;
    public GameObject startGameBtn;
    public Text playerName;
    public Text playerScore;
    public Text playerScore2;
    public SelectCar selectCar;

    public Transform carSpawn;
    
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
            garageBtn.SetActive(true);
            startGameBtn.SetActive(true);
            playerName.text = PlayerPrefs.GetString("Email");
            playerScore.text = "Очки: " + PlayerPrefs.GetInt("Scrote").ToString();
            playerScore2.text = "Очки: " + PlayerPrefs.GetInt("Scrote").ToString();
        } else 
        {
            playerData.SetActive(false);
            exitBtn.SetActive(false);
            loginBtn.SetActive(true);
            garageBtn.SetActive(false);
            startGameBtn.SetActive(false);
        }
    }
    public void StartGame()
    {
        if(PlayerPrefs.HasKey("car_prefab") && PlayerPrefs.GetString("car_prefab") != null)
        {
            if(PlayerPrefs.GetInt("car_select") == 1)
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

    public void ExitGame()
    {
        PlayerPrefs.DeleteAll();
        CheckLoggined();
        if(carSpawn.childCount > 0)
        {
            Destroy(carSpawn.GetChild(0).gameObject);
        }
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
