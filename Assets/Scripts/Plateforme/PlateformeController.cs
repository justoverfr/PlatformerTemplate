using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformeController : MonoBehaviour
{
    [SerializeField] private bool m_canMoove = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_canMoove) {
            transform.Translate(new Vector2(0.1f, 0) * Time.deltaTime * 30f * Mathf.Sin(Time.time * 2f));
        }
    }
}
