using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissArea : MonoBehaviour
{
    TimingManager _timingManager;

    private void Start()
    {
        _timingManager = FindObjectOfType<TimingManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NoteP1"))
        {
            //print("P1Miss");
            ObjectPool.instance.noteQueueP1.Enqueue(collision.gameObject);
            collision.gameObject.gameObject.SetActive(false);
            _timingManager.boxNoteListP1.Remove(collision.gameObject);
        }
        else if (collision.CompareTag("NoteP2"))
        {
            ObjectPool.instance.noteQueueP2.Enqueue(collision.gameObject);
            collision.gameObject.gameObject.SetActive(false);
            _timingManager.boxNoteListP2.Remove(collision.gameObject);
            //print("P2Miss");
        }
        else
        {
            _timingManager.SuccessOrFailure();
        }
    }
}
