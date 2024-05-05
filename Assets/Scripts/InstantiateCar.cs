using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateCar : MonoBehaviour
{
    public Transform player_spawn;
    public GameObject camera;

    void Awake()
    {
        var car = Resources.Load(PlayerPrefs.GetString("car_prefab")) as GameObject;
        GameObject player = Instantiate(car, player_spawn);
        player.AddComponent<CarController2>();
        player.GetComponent<CarController2>().Traction = 0.1f;
        player.tag = "Player";
        Color newCol;
        ColorUtility.TryParseHtmlString(PlayerPrefs.GetString("current_car_color"), out newCol);
        player.transform.Find("Body").GetComponent<Renderer>().material.color = newCol;
        camera.GetComponent<CameraController>().player = player.transform;
    }
}
