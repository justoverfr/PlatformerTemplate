using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonObject : MonoBehaviour
{
    [SerializeField] private GameObject m_Target;
    [SerializeField] private bool m_SinglePress = false;

    private SpriteRenderer _plateUpSprite;
    private SpriteRenderer _plateDownSprite;
    private SpriteRenderer _baseSprite;

    //dropdown menu to select if the button can either be pressed by a player or a box or both
    enum ButtonType
    {
        Both,
        Player,
        Cube,
    }
    [SerializeField] private ButtonType m_ButtonType;
    private string _buttonType;

    // Start is called before the first frame update
    private void Start()
    {
        _buttonType = m_ButtonType.ToString();

        _plateUpSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _plateDownSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        _baseSprite = transform.GetChild(2).GetComponent<SpriteRenderer>();

        SetColors();
    }

    // Update is called once per frame
    private void Update()
    {

    }

    public void OnButtonPressed()
    {
        m_Target.GetComponent<ButtonEvent>().AddButtonPressed();
    }

    public void OnButtonReleased()
    {
        m_Target.GetComponent<ButtonEvent>().RemoveButtonPressed();
    }

    private void SetColors()
    {
        SetPlateColor();
        SetBaseColor();
    }

    private void SetPlateColor()
    {
        switch (_buttonType)
        {
            case "Both":
                _plateUpSprite.color = Color.white;
                _plateDownSprite.color = Color.white;
                break;
            case "Player":
                _plateUpSprite.color = Color.cyan;
                _plateDownSprite.color = Color.cyan;
                break;
            case "Cube":
                _plateUpSprite.color = Color.red;
                _plateDownSprite.color = Color.red;
                break;
        }
    }

    private void SetBaseColor()
    {
        if (!m_SinglePress)
        {
            _baseSprite.color = Color.gray;
        }
        else
        {
            _baseSprite.color = Color.green;
        }
    }

    public GameObject GetTarget()
    {
        return m_Target;
    }

    public string GetButtonType()
    {
        return _buttonType;
    }

    public bool IsSinglePress()
    {
        return m_SinglePress;
    }
}
