using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
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

    public Text car_name;
    public Text car_price;

    private void Awake()
    {
        
        ShowUserCars();
        
    }

    public void ShowNextCar() 
    {

        if(carIndex < allCars.Count() - 1)
        {
            carIndex++;
            ShowCurrentCar();
        }
        Debug.Log(carIndex);
        
    }

    public void ShowPrevCar()
    {
        if(carIndex > 0 )
        {
            carIndex--;
            ShowCurrentCar();
        }
        Debug.Log(carIndex);
        
    }

    public void ShowAllCars()
    {
        // Debug.Log("All Cars");
        carType = SelectCarType.shopCar;
        carListManager.DisplayAllCars(OnGetAllCarsCallback);
        ShowCurrentCar();
    }

    public void ShowUserCars()
    {
        // Debug.Log("User Cars");
        carType = SelectCarType.userCar;
        carListManager.DisplayPlayerCars("1", OnGetAllCarsCallback);
        ShowCurrentCar();
    }

    private void OnGetAllCarsCallback(List<Car> carsList)
    {
        if (carsList != null)
        {
            allCars.Clear();
            allCars.AddRange(carsList);
        }
        else
        {
            Debug.Log("Failed to get all cars!");
        }
    }

    public void ShowCurrentCar()
    {
        if (allCars == null || carIndex < 0 || carIndex >= allCars.Count())
        {
            Debug.Log("No car to display");
            if(carSpawn.childCount > 0)
            {
                Destroy(carSpawn.GetChild(0).gameObject);
            }
            return;
        }
        Car currentCar = allCars.ElementAt(carIndex);
        // carName.text = currentCar.name;
        if(carSpawn.childCount > 0)
        {
            Destroy(carSpawn.GetChild(0).gameObject);
        }
        // PlayerPrefs.SetString("car_prefab", "Prefabs/" + currentCar.model_path);
        // PlayerPrefs.Save();
        PlayerPrefs.SetString("current_car_model", currentCar.model_path);
        PlayerPrefs.SetInt("current_car_id", currentCar.id);
        PlayerPrefs.SetInt("current_car_price", currentCar.price);
        PlayerPrefs.Save();

        var prefab = Resources.Load("Prefabs/" + currentCar.model_path) as GameObject;
        Instantiate(prefab, carSpawn);
        car_name.text = currentCar.name;
        if(carType == SelectCarType.userCar)
        {
            car_price.text = "Купленно";
        } else 
        {
            if(currentCar.price > 15000)
            {
                car_price.color = Color.red;
            } else 
            {
                car_price.color = Color.green;
            }

            car_price.text = (currentCar.price).ToString();
        }
        
    }
}
