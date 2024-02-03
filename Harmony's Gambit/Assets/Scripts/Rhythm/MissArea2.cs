using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissArea2 : MonoBehaviour
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
            _timingManager.boxNoteListP1.Remove(collision.gameObject);
            StartCoroutine(collision.gameObject.GetComponent<Note>().FadeOutImage());
        }
    }
}
