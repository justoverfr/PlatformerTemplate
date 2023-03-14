using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigBod;
    private bool isFacingRight = true;

    /* ------------------------------ Déplacements ------------------------------ */
    [SerializeField] private float m_MoveSpeed = 8f;
    private float _horizontalSpeed = 0f;

    /* ---------------------------------- Saut ---------------------------------- */
    [SerializeField] private LayerMask m_GroundLayer;
    [SerializeField] private float m_JumpForce = 16f;
    bool _canDoubleJump = true;

    /* ---------------------------------- Dash ---------------------------------- */
    [SerializeField] private float m_DashForce = 24f;
    [SerializeField] private float m_DashTime = 0.2f;
    [SerializeField] private float m_GroundDashCooldown = 0.5f;
    private bool _canDashOnGround = true;
    private bool _canDashInAir = true;
    private bool _isDashing = false;

    // [SerializeField] private TrailRenderer tr;

    private void Awake()
    {
        _rigBod = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_isDashing) return;

        _horizontalSpeed = Input.GetAxis("Horizontal");

        if (IsGrounded() && !Input.GetButton("Jump"))
        {
            _canDoubleJump = true;
            _canDashInAir = true;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded() || _canDoubleJump)
            {
                _rigBod.velocity = new Vector2(_rigBod.velocity.x, m_JumpForce);

                if (!IsGrounded())
                {
                    _canDoubleJump = false;
                }
            }
        }

        if (Input.GetButtonUp("Jump") && _rigBod.velocity.y > 0f)
        {
            _rigBod.velocity = new Vector2(_rigBod.velocity.x, _rigBod.velocity.y * 0.5f);
        }

        if (_canDashOnGround && Input.GetKeyDown(KeyCode.LeftShift))
        {
            if ((IsGrounded() && _canDashOnGround) ||
                (!IsGrounded() && _canDashInAir))
            {
                StartCoroutine(Dash());
            }
        }

        Flip();
    }

    private void FixedUpdate()
    {
        if (_isDashing) return;

        float _verticalSpeed = _rigBod.velocity.y;

        if (_verticalSpeed < 0f && !IsGrounded())
        {
            _verticalSpeed += Physics2D.gravity.y * _rigBod.gravityScale * Time.deltaTime;
        }

        _rigBod.velocity = new Vector2(_horizontalSpeed * m_MoveSpeed, _verticalSpeed);
    }

    private bool IsGrounded()
    {
        // return Physics2D.OverlapCircle(m_GroundDetector.position, 0.2f, m_groundLayer);
        return Physics2D.Raycast(transform.position, Vector2.down, 1f, m_GroundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && _horizontalSpeed < 0f || !isFacingRight && _horizontalSpeed > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Dash()
    {
        _isDashing = true;
        float originalGravity = _rigBod.gravityScale;
        _rigBod.gravityScale = 0f;

        _rigBod.velocity = new Vector2(transform.localScale.x * m_DashForce, 0f);
        // tr.emitting = true;
        yield return new WaitForSeconds(m_DashTime);
        // tr.emitting = false;
        _rigBod.gravityScale = originalGravity;
        _isDashing = false;

        if (IsGrounded())
        {
            _canDashOnGround = false;
            yield return new WaitForSeconds(m_GroundDashCooldown);
            _canDashOnGround = true;
        }
        else
        {
            _canDashInAir = false;
        }
    }

    public void Stop()
    {
        _rigBod.velocity = new Vector2(0, _rigBod.velocity.y);
        enabled = false;
    }
}
