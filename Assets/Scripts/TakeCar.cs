using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeCar : MonoBehaviour
{
    public Text car_price;
    // public GameObject colorPicker;
    // private void Update()
    // {
    //     if(PlayerPrefs.HasKey("car_select") && PlayerPrefs.GetInt("car_select") == 1)
    //     {
    //         colorPicker.SetActive(true);
    //     } else 
    //     {
    //         colorPicker.SetActive(false);
    //     }
    // }
    public void Take()
    {
        PlayerPrefs.SetString("car_prefab", "Prefabs/" + PlayerPrefs.GetString("current_car_model"));
        PlayerPrefs.SetInt("car_select", 1);
        PlayerPrefs.SetInt("selected_car_id", PlayerPrefs.GetInt("current_car_id"));
        PlayerPrefs.Save();
        car_price.text = "Машина взята";
        Debug.Log("Машина взята");
        // if(PlayerPrefs.HasKey("car_select") && PlayerPrefs.GetInt("car_select") == 1)
        // {
        //     colorPicker.SetActive(true);
        // } else 
        // {
        //     colorPicker.SetActive(false);
        // }
    }
}
