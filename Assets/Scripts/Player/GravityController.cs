using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    private float _gravityValue = 9.81f;

    [SerializeField] private float m_GravitySwitchCooldown = 0.5f;
    private float _nextGravitySwitch;

    private Vector2[] _gravityVectors;
    private KeyCode[] _keyCodes;

    // Start is called before the first frame update
    void Start()
    {
        _gravityVectors = new Vector2[] { new Vector2(0, _gravityValue), new Vector2(0, -_gravityValue), new Vector2(-_gravityValue, 0), new Vector2(_gravityValue, 0) };
        _keyCodes = new KeyCode[] { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow };
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time < _nextGravitySwitch) return;

        if (!Input.anyKeyDown) return;

        for (int i = 0; i < _keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(_keyCodes[i]))
            {
                Physics2D.gravity = _gravityVectors[i];
                _nextGravitySwitch = Time.time + m_GravitySwitchCooldown;
            }
        }
    }
}
