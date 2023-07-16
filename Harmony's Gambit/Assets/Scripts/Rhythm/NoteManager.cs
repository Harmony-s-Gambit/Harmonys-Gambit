using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public static NoteManager instance;

    private double bpm = 0; //���� �� bpm
    public double delay = 0; //���� �� delay, ���� �ڿ� ���� ����� ���ΰ�
    private int currentBGMindex; //���� ���� ����� � ���� �����°�, BGMJson ��ũ��Ʈ�� ����. ��: BGM1�� 0
    private double _currentTimeP1 = 0d; //�÷��̾� 1(���� ��Ʈ) ���� �ð�
    private double _currentTimeP2 = 0d; //�÷��̾� 2(�Ʒ��� ��Ʈ) ���� �ð�
    public int bgmListindex = 0; //���° ��Ʈ�� �����ΰ�

    public string currentBGM; //�� ������ ���� � ������ �Ǵ�

    [SerializeField] Transform _tfNoteAppearP1; //�÷��̾� 1(���� ��Ʈ) ���� ��ġ
    [SerializeField] Transform _tfNoteAppearP2; ////�÷��̾� 2(�Ʒ��� ��Ʈ) ���� ��ġ

    TimingManager _timingManager;
    BGMJson _bgmJson;

    private void Start()
    {
        instance = this;
        _timingManager = GetComponent<TimingManager>();
        _bgmJson = FindObjectOfType<BGMJson>();
    }

    public void GenerateNote()
    {
        if (bgmListindex < BGMJson.instance.bgmJsonFiles[currentBGMindex].beatList.Count) //���� ���� ���� �� ��ŭ �ݺ�
        {
            _currentTimeP1 += Time.deltaTime;
            if (_currentTimeP1 >= BGMJson.instance.bgmJsonFiles[currentBGMindex].beatList[bgmListindex] / bpm) //���� ���ڰ� ������
            {
                GameObject t_note = ObjectPool.instance.noteQueueP1.Dequeue();
                t_note.transform.position = _tfNoteAppearP1.position;
                t_note.SetActive(true); //��Ʈ ����
                _timingManager.boxNoteListP1.Add(t_note);
                _currentTimeP1 -= BGMJson.instance.bgmJsonFiles[currentBGMindex].beatList[bgmListindex] / bpm;
            }

            _currentTimeP2 += Time.deltaTime;
            if (_currentTimeP2 >= BGMJson.instance.bgmJsonFiles[currentBGMindex].beatList[bgmListindex] / bpm) //���� ���ڰ� ������
            {
                GameObject t_note = ObjectPool.instance.noteQueueP2.Dequeue();
                t_note.transform.position = _tfNoteAppearP2.position;
                t_note.SetActive(true);
                _timingManager.boxNoteListP2.Add(t_note); //��Ʈ ����
                _currentTimeP2 -= BGMJson.instance.bgmJsonFiles[currentBGMindex].beatList[bgmListindex] / bpm;
                bgmListindex++;
            }
        }
    }

    public void SetBGMValue(string bgmName)
    {
        currentBGMindex = _bgmJson.CurrentBGMindex(bgmName); //���� ���� �ε��� ����
        bpm = BGMJson.instance.bgmJsonFiles[currentBGMindex].bpm; //���� ���� bpm ����
        delay = BGMJson.instance.bgmJsonFiles[currentBGMindex].delay; //���� ���� delay ����
    }

    private void OnTriggerExit2D(Collider2D collision) //��Ʈ�� ȭ�� ���� ������ ����
    {
        if (collision.CompareTag("NoteP1"))
        {
            _timingManager.boxNoteListP1.Remove(collision.gameObject);
            ObjectPool.instance.noteQueueP1.Enqueue(collision.gameObject);
            collision.gameObject.SetActive(false); //�ı�
        }
        if (collision.CompareTag("NoteP2"))
        {
            _timingManager.boxNoteListP2.Remove(collision.gameObject);
            ObjectPool.instance.noteQueueP2.Enqueue(collision.gameObject);
            collision.gameObject.SetActive(false); //�ı�
        }
    }
}
