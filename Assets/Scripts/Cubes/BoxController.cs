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

    enum SingleGravityDirection
    {
        Up,
        Down,
        Left,
        Right
    }
    [SerializeField] private SingleGravityDirection m_SingleGravityDirection;
    float _gravityScale;
    float _horizontalSpeed;
    float _verticalSpeed;
    float _gravityStrength;

    /* -------------------------------- Textures -------------------------------- */
    [SerializeField] private Sprite m_PushableTexture;
    [SerializeField] private Sprite m_UnpushableTexture;

    [SerializeField] private Sprite m_InvertedTexture;
    [SerializeField] private Sprite m_SingleDirectionTexture;

    private SpriteRenderer _textureSprite;
    private SpriteRenderer _companionSprite;
    private SpriteRenderer _gravityTypeSpriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        _rigBod = GetComponent<Rigidbody2D>();
        _textureSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _gravityTypeSpriteRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();
        _companionSprite = transform.GetChild(2).GetComponent<SpriteRenderer>();



        _textureSprite.sprite = m_PushableTexture;
        if (!m_CanBePushed)
        {
            _rigBod.mass = 1000f;
            _textureSprite.sprite = m_UnpushableTexture;
        }

        if (m_IsCompanion)
        {
            _companionSprite.enabled = true;
        }

        if (m_GravityType == GravityType.Normal) return;

        _gravityTypeSpriteRenderer.enabled = true;

        if (m_GravityType == GravityType.Inversed)
        {
            _rigBod.gravityScale *= -1f;
            _gravityTypeSpriteRenderer.sprite = m_InvertedTexture;
        }

        if (m_GravityType == GravityType.Floating ||
            m_GravityType == GravityType.SingleDirection)
        {
            _gravityScale = _rigBod.gravityScale;
            _rigBod.gravityScale = 0f;

            if (m_GravityType == GravityType.SingleDirection)
            {
                _gravityTypeSpriteRenderer.sprite = m_SingleDirectionTexture;

                _rigBod.constraints = RigidbodyConstraints2D.FreezeRotation;
                switch (m_SingleGravityDirection)
                {
                    case SingleGravityDirection.Up:
                        this.transform.rotation = Quaternion.Euler(0, 0, 180);
                        break;
                    case SingleGravityDirection.Left:
                        this.transform.rotation = Quaternion.Euler(0, 0, -90);
                        break;
                    case SingleGravityDirection.Right:
                        this.transform.rotation = Quaternion.Euler(0, 0, 90);
                        break;
                    default:
                        break;
                }
                _gravityStrength = Physics2D.gravity.magnitude;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        _horizontalSpeed = _rigBod.velocity.x;
        _verticalSpeed = _rigBod.velocity.y;

        if (m_GravityType == GravityType.SingleDirection)
        {
            switch (m_SingleGravityDirection)
            {
                case SingleGravityDirection.Up:
                    _verticalSpeed += _gravityStrength * _gravityScale * Time.fixedDeltaTime;
                    break;
                case SingleGravityDirection.Down:
                    _verticalSpeed -= _gravityStrength * _gravityScale * Time.fixedDeltaTime;
                    break;
                case SingleGravityDirection.Left:
                    _horizontalSpeed -= _gravityStrength * _gravityScale * Time.fixedDeltaTime;
                    break;
                case SingleGravityDirection.Right:
                    _horizontalSpeed += _gravityStrength * _gravityScale * Time.fixedDeltaTime;
                    break;
            }

            _rigBod.velocity = new Vector2(_horizontalSpeed, _verticalSpeed);
        }
    }

    public void OnDeath()
    {
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        if (m_IsCompanion)
        {
            FindObjectOfType<GameManager>().GameOver();
        }
    }

    public bool IsCompanion()
    {
        return m_IsCompanion;
    }
}
