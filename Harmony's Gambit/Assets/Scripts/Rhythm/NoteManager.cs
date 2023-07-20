using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public static NoteManager instance;

    private double bpm = 0; //현재 곡 bpm
    public float delay = 0; //현재 곡 delay, 몇초 뒤에 곡을 재생할 것인가
    private int currentBGMindex; //현재 곡이 상수로 어떤 값을 가지는가, BGMJson 스크립트에 있음. 예: BGM1은 0
    private double _currentTimeP1 = 0d; //플레이어 1(위쪽 노트) 생성 시간
    private double _currentTimeP2 = 0d; //플레이어 2(아래쪽 노트) 생성 시간
    private double _currentTimeIn = 0d;
    public int bgmListindex = 0; //몇번째 노트의 박자인가
    public List<double> currentBeatList = new List<double>(); //현재 곡의 비트리스트

    public string currentBGM; //이 변수로 현재 어떤 곡인지 판단

    [SerializeField] Transform _tfNoteAppearP1; //플레이어 1(위쪽 노트) 생성 위치
    [SerializeField] Transform _tfNoteAppearP2; ////플레이어 2(아래쪽 노트) 생성 위치
    [SerializeField] Transform _tfNoteAppearIn;

    TimingManager _timingManager;

    private void Start()
    {
        instance = this;
        _timingManager = GetComponent<TimingManager>();
    }

    public void GenerateNote()
    {
        if (bgmListindex < currentBeatList.Count) //현재 곡의 박자 수 만큼 반복
        {
            _currentTimeP1 += Time.deltaTime;
            if (_currentTimeP1 >= currentBeatList[bgmListindex] / bpm) //일정 박자가 지나면
            {
                GameObject t_note = ObjectPool.instance.noteQueueP1.Dequeue();
                t_note.transform.position = _tfNoteAppearP1.position;
                t_note.SetActive(true); //노트 생성
                _timingManager.boxNoteListP1.Add(t_note);
                _currentTimeP1 -= currentBeatList[bgmListindex] / bpm;
            }

            _currentTimeP2 += Time.deltaTime;
            if (_currentTimeP2 >= currentBeatList[bgmListindex] / bpm) //일정 박자가 지나면
            {
                GameObject t_note = ObjectPool.instance.noteQueueP2.Dequeue();
                t_note.transform.position = _tfNoteAppearP2.position;
                t_note.SetActive(true);
                _timingManager.boxNoteListP2.Add(t_note); //노트 생성
                _currentTimeP2 -= currentBeatList[bgmListindex] / bpm;
            }

            _currentTimeIn += Time.deltaTime;
            if (_currentTimeIn >= currentBeatList[bgmListindex] / bpm) //일정 박자가 지나면
            {
                GameObject t_note = ObjectPool.instance.noteQueueIn.Dequeue();
                t_note.transform.position = _tfNoteAppearIn.position;
                t_note.SetActive(true);
                _currentTimeIn -= currentBeatList[bgmListindex] / bpm;
                bgmListindex++;
            }
        }
        else
        {
            PlayAudio.instance._isGameStart = false;
            PlayAudio.instance._isMusiceStart = false;
        }
    }

    public void SetBGMValue(string bgmName)
    {
        currentBGMindex = BGMJson.instance.CurrentBGMindex(bgmName); //현재 곡의 인덱스 설정
        bpm = BGMJson.instance.bgmJsonFiles[currentBGMindex].bpm; //현재 곡의 bpm 설정
        delay = BGMJson.instance.bgmJsonFiles[currentBGMindex].delay; //현재 곡의 delay 설정
        currentBGM = BGMJson.instance.bgmJsonFiles[currentBGMindex].bgmName; //현재 곡의 이름 설정
        currentBeatList = BGMJson.instance.bgmJsonFiles[currentBGMindex].beatList; //현재 곡의 비트 리스트 설정
        bgmListindex = 0; //초기화
    }

    private void OnTriggerExit2D(Collider2D collision) //노트가 화면 밖을 나가면 실행
    {
        if (collision.CompareTag("NoteP1"))
        {
            _timingManager.boxNoteListP1.Remove(collision.gameObject);
            ObjectPool.instance.noteQueueP1.Enqueue(collision.gameObject);
            collision.gameObject.SetActive(false); //파괴
        }
        else if (collision.CompareTag("NoteP2"))
        {
            _timingManager.boxNoteListP2.Remove(collision.gameObject);
            ObjectPool.instance.noteQueueP2.Enqueue(collision.gameObject);
            collision.gameObject.SetActive(false); //파괴
        }
        else if (collision.CompareTag("NoteIn"))
        {
            ObjectPool.instance.noteQueueIn.Enqueue(collision.gameObject);
            collision.gameObject.SetActive(false); //파괴
        }
    }
}
