using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    private bool _isMusiceStart = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isMusiceStart)
        {
            if (collision.CompareTag("NoteP1"))
            {
                AudioManager.instance.PlayBGM(NoteManager.instance.currentBGM);
                _isMusiceStart = true;
            }
        }
    }
}
