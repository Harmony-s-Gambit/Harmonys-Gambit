using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NoteP1"))
        {
            print("P1Miss");
        }
        else if (collision.CompareTag("NoteP2"))
        {
            print("P2Miss");
        }
    }
}
