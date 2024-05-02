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
        PlayerPrefs.Save();
        car_price.text = "Машина взята";
        Debug.Log("Машина взята");
    }
}
