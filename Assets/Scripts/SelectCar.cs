using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;

public enum SelectCarType
{
    userCar,
    shopCar
}

public class SelectCar : MonoBehaviour
{
    public CarListManager carListManager;
    private List<Car> allCars = new List<Car>();
    public SelectCarType carType = SelectCarType.userCar;
    private int carIndex = 0;
    public Transform carSpawn;
    public Text screenName;
    public Text car_name;
    public Text car_price;

    private void Awake()
    {
        if(PlayerPrefs.HasKey("user_id"))
        {
            ShowUserCars();
        }
    }

    public void changeScreenName()
    {
        screenName.text = carType == SelectCarType.userCar ? "Гараж" : "Магазин";
    }

    public void ShowNextCar() 
    {
        if(carIndex < allCars.Count - 1)
        {
            carIndex++;
            ShowCurrentCar();
        }
    }

    public void ShowPrevCar()
    {
        if(carIndex > 0)
        {
            carIndex--;
            ShowCurrentCar();
        }
    }

    public void ShowAllCars()
    {
        carType = SelectCarType.shopCar;
        carListManager.DisplayAllCars(OnGetAllCarsCallback);
    }

    public void ShowUserCars()
    {
        carType = SelectCarType.userCar;
        carListManager.DisplayPlayerCars(PlayerPrefs.GetInt("user_id").ToString(), OnGetAllCarsCallback);
    }

    public void OnGetAllCarsCallback(List<Car> carsList)
    {
        if (carsList != null)
        {
            allCars = carsList;
            carIndex = Mathf.Clamp(carIndex, 0, allCars.Count - 1);
            ShowCurrentCar();
        }
        else
        {
            Debug.Log("Failed to get all cars!");
        }
    }

    public void ShowCurrentCar()
    {
        if (allCars == null || allCars.Count == 0 || carIndex < 0 || carIndex >= allCars.Count)
        {
            Debug.Log("No car to display");
            return;
        }
        
        if(carSpawn.childCount > 0)
        {
            Destroy(carSpawn.GetChild(0).gameObject);
        }
        
        Car currentCar = allCars.ElementAt(carIndex);
        PlayerPrefs.SetString("current_car_model", currentCar.model_path);
        PlayerPrefs.SetInt("current_car_id", currentCar.id);
        PlayerPrefs.SetInt("current_car_price", currentCar.price);
        PlayerPrefs.SetString("current_car_color", currentCar.color);
        PlayerPrefs.Save();

        var prefab = Resources.Load("Prefabs/" + currentCar.model_path) as GameObject;
        
        GameObject newCar = Instantiate(prefab, carSpawn);
        newCar.tag = "Player";
        newCar.GetComponent<Rigidbody>().isKinematic = true;
        Color newCol;
        ColorUtility.TryParseHtmlString(currentCar.color, out newCol);

        newCar.transform.Find("Body").GetComponent<Renderer>().material.color = newCol;
        Debug.Log(newCar.transform.Find("Body").GetComponent<Renderer>().material.color);
        car_name.text = currentCar.name;

        if(carType == SelectCarType.userCar)
        {
            car_price.text = "Купленно";
            car_price.color = Color.white;
        }
        else 
        {
            if(currentCar.price > PlayerPrefs.GetInt("Scrote"))
            {
                car_price.color = Color.red;
            }
            else 
            {
                car_price.color = Color.green;
            }
            car_price.text = currentCar.price.ToString();
        }
    }
}
