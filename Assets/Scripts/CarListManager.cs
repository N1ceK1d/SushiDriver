using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;

public class CarListManager : MonoBehaviour
{
    private string allCarsURL = "http://sushidriver/php/getCars.php";
    private string playerCarsURL = "http://sushidriver/php/getPlayerCars.php";

    public void DisplayAllCars(Action<List<Car>> callback)
    {
        StartCoroutine(GetAllCars(callback));
    }

    public void DisplayPlayerCars(string playerId, Action<List<Car>> callback)
    {
        StartCoroutine(GetPlayerCars(playerId, callback));
    }

    private IEnumerator GetAllCars(Action<List<Car>> callback)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(allCarsURL))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
                callback(null);
            }
            else
            {
                string carsData = www.downloadHandler.text;
                List<Car> carsList = ParseCarList(carsData);
                callback(carsList);
            }
        }
    }

    private IEnumerator GetPlayerCars(string playerId, Action<List<Car>> callback)
    {
        string playerCarsURLWithId = playerCarsURL + "?playerId=" + playerId;

        using (UnityWebRequest www = UnityWebRequest.Get(playerCarsURLWithId))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
                callback(null);
            }
            else
            {
                string carsData = www.downloadHandler.text;
                List<Car> carsList = ParseCarList(carsData);
                callback(carsList);
            }
        }
    }

    private List<Car> ParseCarList(string carsData)
    {
        List<Car> carsList = new List<Car>();

        try
        {
            JSONNode json = JSON.Parse(carsData);

            if (json != null && json.IsArray)
            {
                foreach (JSONNode carNode in json.AsArray)
                {
                    Car car = new Car();
                    car.id = carNode["id"].AsInt;
                    car.name = carNode["name"];
                    car.model_path = carNode["model_path"];
                    car.price = carNode["price"].AsInt;
                    carsList.Add(car);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error parsing JSON: " + e.Message);
            return null;
        }

        return carsList;
    }
}

[System.Serializable]
public class Car
{
    public int id;
    public string name;
    public string model_path;
    public int price;
}
