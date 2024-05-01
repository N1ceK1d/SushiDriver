using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class BuyCar : MonoBehaviour
{
    private string postURL = "http://sushidriver/php/buyCar.php";

    // int playerId = PlayerPrefs.GetInt("playerId");
    // int playerScore = PlayerPrefs.GetInt("playerScore");
    

    // Метод для отправки данных на сервер
    public void SendPurchaseData()
    {
        int playerId = 1;
        int playerScore = 15000;
        int car_price = PlayerPrefs.GetInt("current_car_price");
        int car_id = PlayerPrefs.GetInt("current_car_id");
        Debug.Log("Цена: " + car_price.ToString() + " - Очки: " + playerScore.ToString());
        if(playerScore >= car_price)
        {
            Dictionary<string, int> data = new Dictionary<string, int>();
            data.Add("userId", playerId);
            data.Add("carId", car_id);

            StartCoroutine(PostData(data));
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
}
