using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigBod;

    [SerializeField]
    private LayerMask m_GroundLayer;

    [SerializeField]
    private float m_MoveSpeed;

    private Vector2 _moveVector = Vector2.zero;

    [SerializeField]
    private float m_MinJumpForce;

    [SerializeField]
    private float m_MaxJumpForce;

    [SerializeField]
    private float m_GravityScale = 1f;

    [SerializeField]
    private int m_JumpCount = 2;

    public void JumpCount()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, 1f, m_GroundLayer))
        {
            m_JumpCount = 2;
        }
    }
    // private float _movement;
    private bool _isJumpButtonPressed;

    private void Awake()
    {
        _rigBod = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Déplacements horizontals
        _moveVector = Vector2.right * Input.GetAxis("Horizontal") * m_MoveSpeed;
        // Déplacements verticaux   
        _moveVector.y = _rigBod.velocity.y;
        // Game Over si le joueur tombe
        if (_moveVector.y <= -50){
            FindObjectOfType<GameManager>().GameOver();
        }
        // Reset double saut si le joueur touche le sol
        if (Physics2D.Raycast(transform.position, Vector2.down, 1f, m_GroundLayer))
        {
            m_JumpCount = 2;
        }
        // Saut
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m_JumpCount > 0)
            {
                _isJumpButtonPressed = true;
                m_JumpCount--;
            }
            if (m_JumpCount <= 0) {
                _isJumpButtonPressed = false;
            }
        }

        _rigBod.velocity = _moveVector;
    }

    private void FixedUpdate()
    {
        // Quand le bouton saut est pressé, on applique une force verticale
        if (_isJumpButtonPressed)
        {
            _moveVector.y = CalculateJumpImpulse();
            _isJumpButtonPressed = false;

            // _rigBod.AddForce(Vector2.up * m_JumpForce, ForceMode2D.Impulse);
        }

        if (_moveVector.y < -Mathf.Epsilon)
        {
            _moveVector.y += Physics2D.gravity.y * _rigBod.gravityScale * m_GravityScale * Time.deltaTime;
        }
        else if (_moveVector.y > Mathf.Epsilon && !Input.GetKey(KeyCode.Space))
        {
            _moveVector.y += Physics2D.gravity.y * _rigBod.gravityScale * m_MinJumpForce * Time.deltaTime;
        }

        // _rigBod.velocity = new Vector2(m_MoveSpeed * _movement, _rigBod.velocity.y);

        _rigBod.velocity = _moveVector;
    }

    public void Stop()
    {
        _rigBod.velocity = new Vector2(0, _rigBod.velocity.y);
        enabled = false;
    }

    public float CalculateJumpImpulse()
    {
        float jumpImpulse = Mathf.Sqrt(-2f * Physics2D.gravity.y * _rigBod.gravityScale * m_MaxJumpForce);
        return jumpImpulse;
    }
}
