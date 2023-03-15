using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    [SerializeField] private bool m_isKillZone = false;
    [SerializeField] private bool m_isCubeKillZone = false;
    [SerializeField] private bool m_isAntiJumpZone = false;
    [SerializeField] private bool m_isAntiDashZone = false;
    [SerializeField] private bool m_isAntiGravityZone = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && m_isKillZone)
        {
            FindObjectOfType<GameManager>().GameOver();
        }

        if (other.gameObject.tag == "Cube" && m_isCubeKillZone)
        {
            if (other.GetComponent<BoxController>().IsKillCube()) {
                other.gameObject.GetComponent<BoxController>().OnDeath();
            }
        }
        if (other.gameObject.tag == "Player" && m_isAntiJumpZone)
        {
            other.gameObject.GetComponent<PlayerController>().SetJumpStatus(false);
        }
        if (other.gameObject.tag == "Player" && m_isAntiDashZone)
        {
            other.gameObject.GetComponent<PlayerController>().SetDashStatus(false);
        }
        if (other.gameObject.tag == "Player" && m_isAntiGravityZone)
        {
            other.gameObject.GetComponent<GravityController>().SetGravityActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && m_isAntiJumpZone)
        {
            other.gameObject.GetComponent<PlayerController>().SetJumpStatus(true);
        }
        if (other.gameObject.tag == "Player" && m_isAntiDashZone)
        {
            other.gameObject.GetComponent<PlayerController>().SetDashStatus(true);
        }
        if (other.gameObject.tag == "Player" && m_isAntiGravityZone)
        {
            other.gameObject.GetComponent<GravityController>().SetGravityActive(true);
        }
    }
}
