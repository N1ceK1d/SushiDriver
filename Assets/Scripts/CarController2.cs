using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController2 : MonoBehaviour
{

    // Настройки
    public float MoveSpeed = 15; // Скорость движения
    public float MaxSpeed = 15; // Максимальная скорость
    public float Drag = 0.98f; // Сопротивление движению
    public float SteerAngle = 20; // Угол поворота
    public float Traction = 1; // Сцепление

    private Vector3 MoveForce; // Сила движения

    public TrailRenderer[] trails;
    private bool emiting;

    private Vector2 touch_kord;

    public GameObject FR;
    public GameObject FL; 

    void FixedUpdate()
    {
        // Движение
        MoveForce += transform.forward * MoveSpeed * Time.deltaTime * Input.GetAxis("Vertical");

        // Управление
        float steerInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerInput * MoveForce.magnitude * SteerAngle * (Time.deltaTime * 0.5f));

        // Сопротивление и ограничение максимальной скорости
        MoveForce *= Drag;
        MoveForce = Vector3.ClampMagnitude(MoveForce, MaxSpeed);
        
        // Сцепление
        MoveForce = Vector3.Lerp(MoveForce.normalized, transform.forward, Traction * Time.deltaTime) * MoveForce.magnitude;

        // Инерция
        if (MoveForce.magnitude < 0.1f)
        {
            MoveForce = Vector3.zero;
        }

        // Перемещение
        transform.position += MoveForce * Time.deltaTime;

        WheelRotate(FR);
        WheelRotate(FL);
    }

    public void GetDeviceRotation ()
    {
        // Получение угла поворота устройства по оси z (в градусах)
        float xAngle = Input.acceleration.x * Mathf.Rad2Deg;

        // Вывод угла поворота в консоль
        Debug.Log("Угол поворота по оси x: " + xAngle);
    }

    public void WheelRotate(GameObject wheel)
    {
        // Получение угла поворота устройства по оси z (в радианах)
        float xAngle = Input.acceleration.x * Mathf.PI / 2;
        // Ограничение угла поворота колес до максимально 35 градусов
        xAngle = Mathf.Clamp(xAngle, -Mathf.PI / 4, Mathf.PI / 4);

        // Поворот колес в зависимости от ограниченного угла поворота устройства
        wheel.transform.localRotation = Quaternion.Euler(0, 0, xAngle * Mathf.Rad2Deg);

        // Вывод угла поворота в консоль
        Debug.Log("Угол поворота по оси x: " + xAngle);
    }
}
