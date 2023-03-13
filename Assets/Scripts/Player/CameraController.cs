using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    private Transform m_Target;

    [SerializeField]
    private float m_Speed;

    [SerializeField]
    private Vector3 m_Offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, m_Target.position + m_Offset, m_Speed * Time.deltaTime);
    }
}
