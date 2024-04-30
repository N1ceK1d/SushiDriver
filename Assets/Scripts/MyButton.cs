using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyButton : MonoBehaviour
{

    public bool isPressed;
    public float dumpenPress = 0;
    public float sensetivity = 2f;
    
    void Start()
    {
        SetUpButton();
    }

    void Update()
    {
        if(isPressed)
        {
            dumpenPress += sensetivity * Time.deltaTime;
        }
        else 
        {
            dumpenPress -= sensetivity * Time.deltaTime;
        }
        dumpenPress = Mathf.Clamp01(dumpenPress);
    }

    public void SetUpButton()
    {
        EventTrigger trigger = gameObject.AddComponent<EventTrigger>();
        var pointerDown = new EventTrigger.Entry();
        pointerDown.eventID = EventTriggerType.PointerDown;
        pointerDown.callback.AddListener((e) => OnClickDown());

        var pointerUp = new EventTrigger.Entry();
        pointerUp.eventID = EventTriggerType.PointerUp;
        pointerUp.callback.AddListener((e) => OnClickUp());

        trigger.triggers.Add(pointerDown);
        trigger.triggers.Add(pointerUp);
    }

    public void OnClickDown()
    {
        isPressed = true;
    }

    public void OnClickUp()
    {
        isPressed = false;
    }
}
