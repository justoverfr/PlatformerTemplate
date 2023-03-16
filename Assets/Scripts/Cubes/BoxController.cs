using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    Rigidbody2D _rigBod;

    [SerializeField] private bool m_CanBePushed = true;
    [SerializeField] private bool m_IsCompanion = false;

    enum GravityType
    {
        Normal,
        SingleDirection,
        Inversed,
        Floating
    }

    [SerializeField] private GravityType m_GravityType;

    // [SerializeField] private bool m_HasInversedGravity = false;
    // [SerializeField] private bool m_IsFloating = false;


    // Start is called before the first frame update
    void Start()
    {
        _rigBod = GetComponent<Rigidbody2D>();

        if (!m_CanBePushed)
        {
            _rigBod.mass = 1000f;
        }

        if (m_GravityType == GravityType.Inversed)
        {
            _rigBod.gravityScale *= -1f;
        }

        if (m_GravityType == GravityType.Floating ||
            m_GravityType == GravityType.SingleDirection)
        {
            _rigBod.gravityScale = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (m_GravityType == GravityType.SingleDirection)
        {
            _rigBod.AddForce(Vector2.down * 9.81f);
        }
    }



    public void OnDeath()
    {
        Destroy(this.gameObject);
    }

    public bool IsKillCube()
    {
        return m_IsCompanion;
    }
}
