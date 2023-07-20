using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissArea : MonoBehaviour
{
    TimingManager _timingManager;
    GameManager _gameManager;

    private void Start()
    {
        _timingManager = FindObjectOfType<TimingManager>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NoteP1"))
        {
            _timingManager.boxNoteListP1.Remove(collision.gameObject);
        }
        else if (collision.CompareTag("NoteP2"))
        {
            _timingManager.boxNoteListP2.Remove(collision.gameObject);
        }
        else if (collision.CompareTag("NoteIn"))
        {
            _gameManager.rhythm = true;
            _timingManager.SuccessOrFailure(); //µø±‚»≠
        }
    }
}
