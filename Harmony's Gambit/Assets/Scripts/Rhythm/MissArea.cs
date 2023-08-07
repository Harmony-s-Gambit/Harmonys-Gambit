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
            StartCoroutine(collision.gameObject.GetComponent<Note>().FadeOutImage());
        }
        if (collision.CompareTag("NoteIn"))
        {
            if (NoteManager.instance.currentBGM != "Offset")
            {
                _gameManager.rhythm = true;
                CameraMoving.instance.rhythm = true;
                _timingManager.SuccessOrFailure(); //����ȭ
            }
        }
    }
}
