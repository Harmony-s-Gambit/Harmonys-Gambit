using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public static NoteManager instance;

    double bpm = 0;
    double _currentTimeP1 = 0d;
    double _currentTimeP2 = 0d;
    public int bgmListindex = 10;

    [SerializeField] Transform _tfNoteAppearP1;
    [SerializeField] Transform _tfNoteAppearP2;

    TimingManager _timingManager;

    private void Start()
    {
        instance = this;
        _timingManager = GetComponent<TimingManager>();
    }

    public void GenerateNote(string bgmName)
    {
        bpm = BGMTextReader.instance.BGMTextRead(bgmName)[1];

        if (bgmListindex < BGMTextReader.instance.BGMTextRead(bgmName).Count)
        {
            _currentTimeP1 += Time.deltaTime;
            if (_currentTimeP1 >= BGMTextReader.instance.BGMTextRead(bgmName)[bgmListindex] / bpm)
            {
                GameObject t_note = ObjectPool.instance.noteQueueP1.Dequeue();
                t_note.transform.position = _tfNoteAppearP1.position;
                t_note.SetActive(true);
                _timingManager.boxNoteListP1.Add(t_note);
                _currentTimeP1 -= BGMTextReader.instance.BGMTextRead(bgmName)[bgmListindex] / bpm;
                
            }

            _currentTimeP2 += Time.deltaTime;
            if (_currentTimeP2 >= BGMTextReader.instance.BGMTextRead(bgmName)[bgmListindex] / bpm)
            {
                GameObject t_note = ObjectPool.instance.noteQueueP2.Dequeue();
                t_note.transform.position = _tfNoteAppearP2.position;
                t_note.SetActive(true);
                _timingManager.boxNoteListP2.Add(t_note);
                _currentTimeP2 -= BGMTextReader.instance.BGMTextRead(bgmName)[bgmListindex] / bpm;
                bgmListindex++;
            }
        }
        
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("NoteP1"))
        {
            _timingManager.boxNoteListP1.Remove(collision.gameObject);
            ObjectPool.instance.noteQueueP1.Enqueue(collision.gameObject);
            collision.gameObject.SetActive(false);
        }
        if (collision.CompareTag("NoteP2"))
        {
            _timingManager.boxNoteListP2.Remove(collision.gameObject);
            ObjectPool.instance.noteQueueP2.Enqueue(collision.gameObject);
            collision.gameObject.SetActive(false);
        }
    }
}
