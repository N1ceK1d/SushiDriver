using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeCar : MonoBehaviour
{
    public Text car_price;
    public void Take()
    {
        PlayerPrefs.SetString("car_prefab", "Prefabs/" + PlayerPrefs.GetString("current_car_model"));
        PlayerPrefs.SetInt("car_select", 1);
        PlayerPrefs.SetInt("selected_car_id", PlayerPrefs.GetInt("current_car_id"));
        PlayerPrefs.SetString("selected_car_model", PlayerPrefs.GetString("current_car_model"));
        PlayerPrefs.SetString("selected_car_color", PlayerPrefs.GetString("current_car_color"));
        PlayerPrefs.Save();
        car_price.text = "Машина взята";
        Debug.Log("Машина взята");
    }
}
