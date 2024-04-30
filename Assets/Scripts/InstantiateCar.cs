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
        camera.GetComponent<CameraController>().player = player.transform;
        player.GetComponent<CarController2>().FR = player.GetComponent<Transform>().Find("WheelMeshes").Find("FR").gameObject;
        player.GetComponent<CarController2>().FL = player.GetComponent<Transform>().Find("WheelMeshes").Find("FL").gameObject;
    }
}
