using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvent : MonoBehaviour
{
    private int _buttonsCount;
    private int _buttonsPressed;

    // Start is called before the first frame update
    void Start()
    {
        ButtonObject[] buttonsList = FindObjectsOfType<ButtonObject>();

        foreach (ButtonObject button in buttonsList)
        {
            if (Array.IndexOf(button.GetTargets(), this.gameObject) != -1)
            {
                _buttonsCount++;
            }
        }
    }

    virtual protected void OnAllButtonsPressed() { }

    virtual protected void OnNotAllButtonsPressed() { }

    public void AddButtonPressed()
    {
        _buttonsPressed++;
        if (_buttonsPressed == _buttonsCount)
        {
            OnAllButtonsPressed();
        }
    }

    public void RemoveButtonPressed()
    {
        _buttonsPressed--;
        if (_buttonsPressed == _buttonsCount - 1)
        {
            OnNotAllButtonsPressed();
        }
    }
}
