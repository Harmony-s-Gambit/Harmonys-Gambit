using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeOverField : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Player2"))
        {
            other.GetComponent<Player>().SetIsCollidedTimeOverField(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Player2"))
        {
            collision.GetComponent<Player>().SetIsCollidedTimeOverField(false);
        }
    }
}
