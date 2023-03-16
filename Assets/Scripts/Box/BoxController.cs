using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField] private bool m_canBePushed = true;
    [SerializeField] private bool m_inversedGravity = false;
    [SerializeField] private bool m_floatable = false;
    [SerializeField] private bool m_KillCube = false;


    // Start is called before the first frame update
    void Start()
    {
        if (!m_canBePushed) {
            GetComponent<Rigidbody2D>().mass = 1000f;
        }

        if (m_inversedGravity) {
            GetComponent<Rigidbody2D>().gravityScale *= -1f;
        }

        if (m_floatable) {
            GetComponent<Rigidbody2D>().gravityScale = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDeath() {
        Destroy(this.gameObject);
    }

    public bool IsKillCube() {
        return m_KillCube;
    }
}
