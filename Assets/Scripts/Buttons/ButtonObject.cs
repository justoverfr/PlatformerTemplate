using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonObject : MonoBehaviour
{
    [SerializeField] private GameObject m_Target;

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

    public GameObject GetTarget()
    {
        return m_Target;
    }

    public string GetButtonType()
    {
        return _buttonType;
    }
}
