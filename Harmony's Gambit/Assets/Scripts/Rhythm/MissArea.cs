using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissArea : MonoBehaviour
{
    TimingManager _timingManager;
    GameManager _gameManager;
    StructureManager _structureManager;
    CameraMoving _cameraMoving;
    SightManager _sightManager;
    PlayerManager _playerManager;

    private void Start()
    {
        _timingManager = FindObjectOfType<TimingManager>();
        _gameManager = FindObjectOfType<GameManager>();
        _structureManager = FindObjectOfType<StructureManager>();
        _cameraMoving = FindObjectOfType<CameraMoving>();
        _sightManager = FindObjectOfType<SightManager>();
        _playerManager = FindObjectOfType<PlayerManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NoteP2"))
        {
            _timingManager.boxNoteListP1.Remove(collision.gameObject);
            StartCoroutine(collision.gameObject.GetComponent<Note2>().FadeOutImage());
        }
        if (collision.CompareTag("NoteIn"))
        {
            if (NoteManager.instance.currentBGM != "Offset")
            {
                _gameManager.rhythm = true;
                _cameraMoving.rhythm = true;
                _sightManager.rhythm = true;
                ScoreManager.instance.rhythm = true;
                StartCoroutine(_structureManager.rhythmTure());
                _timingManager.SuccessOrFailure(); //µø±‚»≠
            }
        }
    }
}
