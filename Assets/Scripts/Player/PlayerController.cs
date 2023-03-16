using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigBod;
    private bool isFacingRight = true;

    /* ------------------------------ Déplacements ------------------------------ */
    [SerializeField] private float m_MoveSpeed = 8f;
    private float _horizontalSpeed;
    private float _verticalSpeed;
    private Vector2 _moveVector = Vector2.zero;

    /* ---------------------------------- Saut ---------------------------------- */
    [SerializeField] private LayerMask m_GroundLayer;
    [SerializeField] private float m_JumpForce = 16f;
    bool _isJumpActive = true;
    bool _canDoubleJump = true;

    /* ---------------------------------- Dash ---------------------------------- */
    [SerializeField] private float m_DashForce = 24f;
    [SerializeField] private float m_DashTime = 0.2f;
    [SerializeField] private float m_GroundDashCooldown = 0.5f;
    private bool _canDashOnGround = true;
    private bool _canDashInAir = true;
    private bool _isDashing = false;
    private bool _isDashActive = true;

    /* --------------------------------- Gravité -------------------------------- */
    private Vector2 _gravityVector;
    private bool _isGravityVertical;

    // [SerializeField] private TrailRenderer tr;

    private void Awake()
    {
        _rigBod = GetComponent<Rigidbody2D>();
        _gravityVector = Physics2D.gravity.normalized;
        UpdateGravityVector();
    }

    private void Update()
    {
        /* -------------------------------- Contrôles ------------------------------- */
        if (_isDashing) return;

        if (IsGrounded() && !Input.GetButton("Jump"))
        {
            _canDoubleJump = true;
            _canDashInAir = true;
        }

        /* ------------------------------ Déplacements ------------------------------ */
        if (_isGravityVertical)
        {
            _horizontalSpeed = Input.GetAxis("Horizontal") * m_MoveSpeed;
        }
        else
        {
            _verticalSpeed = Input.GetAxis("Vertical") * m_MoveSpeed;
        }

        if (Input.GetButtonDown("Jump") && _isJumpActive)
        {
            if (IsGrounded() || _canDoubleJump)
            {
                if (_isGravityVertical)
                {
                    _rigBod.velocity = new Vector2(_rigBod.velocity.x, m_JumpForce * -_gravityVector.y);
                }
                else
                {
                    _rigBod.velocity = new Vector2(m_JumpForce * -_gravityVector.x, _rigBod.velocity.y);
                }

                if (!IsGrounded())
                {
                    _canDoubleJump = false;
                }
            }
        }

        /* ---------------------------------- Saut ---------------------------------- */
        if (Input.GetButtonUp("Jump"))
        {
            if (_isGravityVertical)
            {
                if (Mathf.Sign(_rigBod.velocity.y) == Mathf.Sign(-_gravityVector.y))
                {
                    _rigBod.velocity = new Vector2(_rigBod.velocity.x, _rigBod.velocity.y * 0.5f);
                }
            }
            else
            {
                if (Mathf.Sign(_rigBod.velocity.x) == Mathf.Sign(-_gravityVector.x))
                {
                    _rigBod.velocity = new Vector2(_rigBod.velocity.x * 0.5f, _rigBod.velocity.y);
                }
            }
        }

        if (_canDashOnGround && Input.GetKeyDown(KeyCode.LeftShift) && _isDashActive)
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

        /* ----------------------------- Gestion gravité ---------------------------- */
        if (_isGravityVertical)
        {
            _verticalSpeed = _rigBod.velocity.y;

            if (Mathf.Sign(_verticalSpeed) == Mathf.Sign(_gravityVector.y) && !IsGrounded())
            {
                _verticalSpeed += Physics2D.gravity.y * _rigBod.gravityScale * Time.deltaTime;
            }
        }
        else
        {
            _horizontalSpeed = _rigBod.velocity.x;

            if (Mathf.Sign(_horizontalSpeed) == Mathf.Sign(_gravityVector.x) && !IsGrounded())
            {
                _horizontalSpeed += Physics2D.gravity.x * _rigBod.gravityScale * Time.deltaTime;
            }
        }

        _rigBod.velocity = new Vector2(_horizontalSpeed, _verticalSpeed);
    }

    private bool IsGrounded()
    {
        // return Physics2D.OverlapCircle(m_GroundDetector.position, 0.2f, m_groundLayer);
        return Physics2D.Raycast(transform.position, _gravityVector, 1f, m_GroundLayer);
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
        if (_isGravityVertical)
        {
            if (Mathf.Abs(_horizontalSpeed) < Mathf.Epsilon) yield break;

            _horizontalSpeed = Mathf.Sign(_horizontalSpeed) * m_DashForce;
            _rigBod.velocity = new Vector2(_horizontalSpeed, 0f);
        }
        else
        {
            if (Mathf.Abs(_verticalSpeed) < Mathf.Epsilon) yield break;

            _verticalSpeed = Mathf.Sign(_verticalSpeed) * m_DashForce;
            _rigBod.velocity = new Vector2(0f, _verticalSpeed);
        }

        _isDashing = true;
        float originalGravity = _rigBod.gravityScale;
        _rigBod.gravityScale = 0f;

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
        this.enabled = false;
    }

    public bool IsDashing()
    {
        return _isDashing;
    }

    public void SetJumpStatus(bool jumpStatus)
    {
        _isJumpActive = jumpStatus;
    }
    public void SetDashStatus(bool dashStatus)
    {
        _isDashActive = dashStatus;
    }

    public void UpdateGravityVector()
    {
        _gravityVector = Physics2D.gravity.normalized;
        _isGravityVertical = _gravityVector.y != 0f;
    }
}
