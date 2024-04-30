using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class ButtonHandler : MonoBehaviour
{
    public CarListManager carListManager;

    public Text allCarsText;
    public Text playerCarsText;

    private List<Car> allCars = new List<Car>(); // List to store all cars

    private void Start()
    {
        // Call function to display all cars
        carListManager.DisplayAllCars(OnGetAllCarsCallback);
    }

    // Callback function for getting all cars
    private void OnGetAllCarsCallback(List<Car> carsList)
    {
        if (carsList != null)
        {
            // Print all cars to console
            foreach (Car car in carsList)
            {
                Debug.Log("All Car: " + car.name);
            }

            // Update UI text
            allCarsText.text = string.Join("\n", carsList);

            // Add cars to the list of all cars
            allCars.AddRange(carsList);
        }
        else
        {
            allCarsText.text = "Failed to get all cars!";
        }
    }

    // Callback function for getting player cars
    private void OnGetPlayerCarsCallback(List<Car> carsList)
    {
        if (carsList != null)
        {
            // Print player cars to console
            foreach (Car car in carsList)
            {
                Debug.Log("Player Car: " + car);
            }

            // Update UI text
            playerCarsText.text = string.Join("\n", carsList);
        }
        else
        {
            playerCarsText.text = "Failed to get player cars!";
        }
    }

    // Function to handle button click for displaying player cars
    public void OnGetPlayerCarsButtonClicked(string playerId)
    {
        // Call function to display player cars
        carListManager.DisplayPlayerCars(playerId, OnGetPlayerCarsCallback);
    }

    // Function to create a list of all cars
    public void CreateAllCarsList()
    {
        // Print all cars in the list to console
        foreach (Car car in allCars)
        {
            Debug.Log("All Car: " + car.name);
        }
    }
}
