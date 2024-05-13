using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.UI;

public class BuyCar : MonoBehaviour
{
    private string postURL = "http://sushidriver/php/buyCar.php";
    public Text price_car_text;

    // Метод для отправки данных на сервер
    public void SendPurchaseData()
    {
        int playerId = PlayerPrefs.GetInt("user_id");
        int playerScore = PlayerPrefs.GetInt("Scrote");
        int car_price = PlayerPrefs.GetInt("current_car_price");
        int car_id = PlayerPrefs.GetInt("current_car_id");
        
        if(playerScore >= car_price)
        {
            Dictionary<string, int> data = new Dictionary<string, int>();
            data.Add("userId", playerId);
            data.Add("carId", car_id);

            Debug.Log(playerId + " - " + car_id);

            StartCoroutine(PostData(data));

            PlayerPrefs.SetInt("Scrote", playerScore - car_price);
            
            if(PlayerPrefs.HasKey("user_id"))
            {
                int totalScore = PlayerPrefs.GetInt("Scrote");
                WWWForm regForm = new WWWForm();
                regForm.AddField("user_id", PlayerPrefs.GetInt("user_id"));
		        regForm.AddField("score", totalScore);

		        WWW www = new WWW("http://sushidriver/php/updateScore.php", regForm);
		        StartCoroutine(UpdateFunc(www));
                price_car_text.text = "Куплено";
            }
            
        } else 
        {
            Debug.Log("Недостаточно средств!");
        }
        
    }

    // Корутина для отправки данных
    private IEnumerator PostData(Dictionary<string, int> data)
    {
        // Преобразование данных в формат, понятный для сервера
        WWWForm form = new WWWForm();
        foreach (KeyValuePair<string, int> entry in data)
        {
            form.AddField(entry.Key, entry.Value);
        }

        // Отправка данных на сервер
        using (WWW www = new WWW(postURL, form))
        {
            yield return www;

            if (www.error != null)
            {
                Debug.LogError("Ошибка отправки данных: " + www.error);
            }
            else
            {
                Debug.Log("Данные успешно отправлены!");
                // В этом месте можно обрабатывать ответ от сервера, если необходимо
            }
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
