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
    [SerializeField] GameObject _noteP1;
    [SerializeField] Transform _tfNoteAppearP2;
    [SerializeField] GameObject _noteP2;

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
                GameObject t_note = Instantiate(_noteP1, _tfNoteAppearP1.position, Quaternion.identity);
                t_note.transform.SetParent(this.transform);
                _timingManager.boxNoteListP1.Add(t_note);
                _currentTimeP1 -= BGMTextReader.instance.BGMTextRead(bgmName)[bgmListindex] / bpm;
                
            }

            _currentTimeP2 += Time.deltaTime;
            if (_currentTimeP2 >= BGMTextReader.instance.BGMTextRead(bgmName)[bgmListindex] / bpm)
            {
                GameObject t_note = Instantiate(_noteP2, _tfNoteAppearP2.position, Quaternion.identity);
                t_note.transform.SetParent(this.transform);
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
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("NoteP2"))
        {
            _timingManager.boxNoteListP2.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
