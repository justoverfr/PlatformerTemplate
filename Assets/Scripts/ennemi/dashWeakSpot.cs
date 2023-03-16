using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashWeakSpot : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController.IsDashing())
            {
                Destroy(transform.parent.parent.gameObject);
            } else {
                FindObjectOfType<GameManager>().GameOver();
            }
        }
    }
}
