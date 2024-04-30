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
    public SelectCarType carType;
    private int carIndex = 0;
    public Transform carSpawn;

    private void Start()
    {
        ShowAllCars();
    }

    private void DisplayCars()
    {
        if(carType == SelectCarType.userCar)
        {
            ShowAllCars();
        } else 
        {
            ShowUserCars();
        }
    }

    public void ShowNextCar() 
    {
        if(carIndex < allCars.Count() - 1)
        {
            carIndex++;
        }
        Debug.Log(carIndex);
        ShowCurrentCar();
    }

    public void ShowPrevCar()
    {
        if(carIndex > 0 )
        {
            carIndex--;
        }
        Debug.Log(carIndex);
        ShowCurrentCar();
    }

    public void ShowAllCars()
    {
        Debug.Log("All Cars");
        carListManager.DisplayAllCars(OnGetAllCarsCallback);
    }

    public void ShowUserCars()
    {
        Debug.Log("User Cars");
        carListManager.DisplayPlayerCars("1", OnGetAllCarsCallback);
    }

    private void OnGetAllCarsCallback(List<Car> carsList)
    {
        if (carsList != null)
        {
            foreach (Car car in carsList)
            {
                Debug.Log("All Car: " + car.name);
            }
            allCars.AddRange(carsList);
        }
        else
        {
            Debug.Log("Failed to get all cars!");
            //allCarsText.text = "Failed to get all cars!";
        }
    }

    public void ShowCurrentCar()
    {
        if (allCars == null || carIndex < 0 || carIndex >= allCars.Count())
        {
            Debug.Log("No car to display");
            return;
        }
        Car currentCar = allCars.ElementAt(carIndex);
        // carName.text = currentCar.name;
        if(carSpawn.childCount > 0)
        {
            Destroy(carSpawn.GetChild(0).gameObject);
        }
        PlayerPrefs.SetString("car_prefab", "Prefabs/" + currentCar.model_path);
        PlayerPrefs.Save();
        var prefab = Resources.Load("Prefabs/" + currentCar.model_path) as GameObject;
        Instantiate(prefab, carSpawn);
    }
}
