using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    private GameObject _pressurePlate;
    private SpriteRenderer _pressurePlateSprite;
    private ButtonObject _button;

    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponentInParent<ButtonObject>();

        _pressurePlate = GameObject.Find("PressurePlate");
        _pressurePlateSprite = _pressurePlate.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == _button.GetButtonType() ||
            _button.GetButtonType() == "Both")
        {
            _pressurePlateSprite.enabled = false;

            _button.OnButtonPressed();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == _button.GetButtonType() ||
            _button.GetButtonType() == "Both")
        {
            _pressurePlateSprite.enabled = true;

            _button.OnButtonReleased();
        }
    }
}
