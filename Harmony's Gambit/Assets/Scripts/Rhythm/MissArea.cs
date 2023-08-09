using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissArea : MonoBehaviour
{
    TimingManager _timingManager;
    GameManager _gameManager;
    StructureManager _structureManager;
    CameraMoving _cameraMoving;

    private void Start()
    {
        _timingManager = FindObjectOfType<TimingManager>();
        _gameManager = FindObjectOfType<GameManager>();
        _structureManager = FindObjectOfType<StructureManager>();
        _cameraMoving = FindObjectOfType<CameraMoving>();
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
                _cameraMoving.rhythm = true;
                StartCoroutine(_structureManager.rhythmTure());
                _timingManager.SuccessOrFailure(); //µø±‚»≠
            }
        }
    }
}
