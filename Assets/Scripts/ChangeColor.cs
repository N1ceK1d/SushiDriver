using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ChangeColor : MonoBehaviour
{
    public ColorWheelControl wheel;
    private GameObject carBody;
    public GameObject colorPicker;
    public GameObject changeColorBtn;

    void Start()
    {
        // Находим дочерний объект "Body" у объекта с тегом "Player"
        carBody = GameObject.Find("Body");
        if (carBody == null)
        {
            Debug.LogError("Body GameObject not found under the Player!");
        }
    }

    void Update()
    {
        if(colorPicker.activeSelf)
        {
            carBody = GameObject.Find("Body");
            // Проверяем, что объект найден
            if (carBody != null)
            {
                // Присваиваем выбранный цвет из ColorWheelControl к материалу объекта "Body"
                carBody.GetComponent<Renderer>().material.color = wheel.Selection;
            }
            else
            {
                Debug.LogError("Body GameObject not found under the Player!");
            }
        }
    }

    public void SaveColor()
    {
        // Получаем цвет из ColorWheelControl
        Color selectedColor = wheel.Selection;

        // Создаем форму для отправки данных
        WWWForm form = new WWWForm();
        // Добавляем данные цвета в форму
        form.AddField("color", ColorToHex(selectedColor).ToString());
        form.AddField("user_id", PlayerPrefs.GetInt("user_id").ToString());
        form.AddField("car_id", PlayerPrefs.GetInt("selected_car_id").ToString());

        // Отправляем данные на PHP скрипт
        StartCoroutine(SendColorToPHP(form));
        colorPicker.SetActive(false);
        changeColorBtn.SetActive(true);
    }

    IEnumerator SendColorToPHP(WWWForm form)
    {
        // Отправляем POST запрос на сервер
        using (UnityWebRequest www = UnityWebRequest.Post("http://sushidriver/php/updateCarColor.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to send color data: " + www.error);
            }
            else
            {
                Debug.Log("Color data sent successfully!");
            }
        }
    }

    public string ColorToHex(Color color)
    {
        // Умножаем каждый канал цвета на 255 и преобразуем в шестнадцатеричное значение
        int r = (int)(color.r * 255f);
        int g = (int)(color.g * 255f);
        int b = (int)(color.b * 255f);
        int a = (int)(color.a * 255f);

        // Собираем HEX значение в строку
        string hex = "#" + r.ToString("X2") + g.ToString("X2") + b.ToString("X2") + a.ToString("X2");

        return hex;
    }

    public void ChangeColorBtn()
    {
        colorPicker.SetActive(true);
        changeColorBtn.SetActive(false);
    }
}
