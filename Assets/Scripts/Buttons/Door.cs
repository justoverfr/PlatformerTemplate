using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : ButtonEvent
{
    [SerializeField] private bool m_Inverted = false;

    override protected void OnAllButtonsPressed()
    {
        this.gameObject.SetActive(m_Inverted ? true : false);
    }

    override protected void OnNotAllButtonsPressed()
    {
        this.gameObject.SetActive(m_Inverted ? false : true);
    }
}
