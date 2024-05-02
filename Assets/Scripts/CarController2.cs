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

    private float driftScore = 0; // Счётчик очков за дрифт
    public GameObject driftScoreText;
    private int totalScore = 0;

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

        if(PlayerPrefs.HasKey("user_id"))
        {
            driftScoreText = GameObject.Find("Score");
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.4f)
            {
                // Увеличиваем счётчик очков за дрифт
                driftScore+=0.05f;
                totalScore += (int)Mathf.Round(driftScore);
            }
            else
            {
                // Когда машина не дрифтует, выводим счёт в консоль и сбрасываем его
                if (driftScore > 0)
                {
                    Debug.Log("Очки за дрифт: " + driftScore);
                    driftScore = 0;
                }
            }
            driftScoreText.GetComponent<Text>().text = "Счёт: " + totalScore.ToString();
        }

        
    }
}
