using UnityEngine;

public class weakspot : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(transform.parent.parent.gameObject);
        }
    }
}