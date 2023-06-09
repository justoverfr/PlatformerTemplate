using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    private float _gravityValue = 9.81f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Physics2D.gravity = new Vector2(0, _gravityValue);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Physics2D.gravity = new Vector2(0, -_gravityValue);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Physics2D.gravity = new Vector2(-_gravityValue, 0);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Physics2D.gravity = new Vector2(_gravityValue, 0);
        }
    }
}
