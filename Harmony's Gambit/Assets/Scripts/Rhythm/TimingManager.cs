using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    GameManager _gameManager;
    PlayerManager _playerManager;

    public List<GameObject> boxNoteListP1 = new List<GameObject>(); //현재 생성되었고(hierarchy에서 setActive가 true가 되었고) 놓침 구간(Miss Area) 전의 타일들의 리스트
    public List<GameObject> boxNoteListP2 = new List<GameObject>();

    [SerializeField] Transform _centerP1; //판정 범위의 가운데 위치
    [SerializeField] Transform _centerP2;
    [Tooltip("좋은 판정부터 나쁜 판정순으로 입력")]
    [SerializeField] RectTransform[] _timingRectP1; //판정들 추가할 때 필요. 예: perfect, good, bad 등, 현재는 하나뿐
    [Tooltip("좋은 판정부터 나쁜 판정순으로 입력")]
    [SerializeField] RectTransform[] _timingRectP2;
    Vector2[] _timingBoxsP1; //판정 범위의 x좌표, boxNoteList의 노트들 중 이 x좌표 안에 있는 노트가 있다면 성공
    Vector2[] _timingBoxsP2;

    int _keyInputNumP1 = 0; //키 입력 수, 2번 이상 정확한 타이밍에 눌렀는지 판단
    int _keyInputNumP2 = 0;
    private bool _IsSuccessP1 = false; //성공했는가 실패했는가
    private bool _IsSuccessP2 = false;
    private Queue<string> _whatKeyP1 = new Queue<string>(); //어떤 키를 눌렀는가
    private Queue<string> _whatKeyP2 = new Queue<string>();

    [SerializeField] GameObject _successImage; //성공, 실패 여부 이미지 테스트용
    [SerializeField] GameObject _failureImage;
    [SerializeField] GameObject _tfSoFImage;

    private GameObject[] judgmentUIs = new GameObject[3];

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _playerManager = FindObjectOfType<PlayerManager>();

        _timingBoxsP1 = new Vector2[_timingRectP1.Length]; //판정 수 만큼 x좌표 범위 생성
        _timingBoxsP2 = new Vector2[_timingRectP2.Length];
        SetTimingBoxs();

        //판정 ui
        judgmentUIs[0] = GameObject.Find("Perfect");
        judgmentUIs[1] = GameObject.Find("Good");
        judgmentUIs[2] = GameObject.Find("Miss");
        for (int i = 0; i < judgmentUIs.Length; i++)
        {
            //print(judgmentUIs[i].gameObject.name);
            judgmentUIs[i].SetActive(false);
        }
    }

    public void SetTimingBoxs(float offsetP1 = 0, float offsetP2 = 0)
    {
        for (int i = 0; i < _timingRectP1.Length; i++)
        {
            _timingBoxsP1[i].Set(_centerP1.localPosition.x - _timingRectP1[i].rect.width / 2 - offsetP1, _centerP1.localPosition.x + _timingRectP1[i].rect.width / 2 - offsetP1); //각 판정의 x좌표 범위 설정, 정확한 판정일수록 범위 작음
        }
        
        for (int i = 0; i < _timingRectP2.Length; i++)
        {
            _timingBoxsP2[i].Set(_centerP2.localPosition.x - _timingRectP2[i].rect.width / 2 - offsetP2, _centerP2.localPosition.x + _timingRectP2[i].rect.width / 2 - offsetP2);
        }
    }

    public void CheckTiming(int playerNum, string key, bool forceTrue = false) //키를 눌렀을 때 실행
    {
        if (playerNum == 1) //플레이어 1이라면
        {
            for (int i = 0; i < boxNoteListP1.Count; i++) //판정 범위 내에 있는 노트인지 확일할 노트들만큼 반복
            {
                float t_notePosX = boxNoteListP1[i].transform.localPosition.x; //노트의 x좌표 받아오기
                for (int j = 0; j < _timingBoxsP1.Length; j++) //판정 범위만큼 실행, 현재 1개이므로 한 번만 실행하게 됨
                {
                    if ((_timingBoxsP1[j].x <= t_notePosX && t_notePosX <= _timingBoxsP1[j].y) || forceTrue) //판정 범위 안에 있는가
                    {
                        boxNoteListP1[i].GetComponent<Note>().HideNote(); //노트 이미지 제거
                        if (_keyInputNumP1 == 0) //1번 눌렀을 때
                        {
                            _whatKeyP1.Clear();
                            _whatKeyP1.Enqueue(key); //어떤 키를 눌렀는지 저장
                        }
                        _keyInputNumP1++;

                        IsSuccessManage();

                        if (NoteManager.instance.currentBGM == "Offset")
                        {
                            RecordNoteXPos(1, t_notePosX);
                        }

                        return;
                    }
                }
            }
            //print("P1Miss);
        }
        else
        {
            for (int i = 0; i < boxNoteListP2.Count; i++)
            {
                float t_notePosX = boxNoteListP2[i].transform.localPosition.x;
                for (int j = 0; j < _timingBoxsP2.Length; j++)
                {
                    if ((_timingBoxsP2[j].x <= t_notePosX && t_notePosX <= _timingBoxsP2[j].y) || forceTrue)
                    {
                        boxNoteListP2[i].GetComponent<Note2>().HideNote();
                        if (_keyInputNumP2 == 0)
                        {
                            _whatKeyP2.Clear();
                            _whatKeyP2.Enqueue(key);
                        }
                        _keyInputNumP2++;

                        IsSuccessManage();

                        if (NoteManager.instance.currentBGM == "Offset")
                        {
                            RecordNoteXPos(2, t_notePosX);
                        }

                        return;
                    }
                }
            }
            //print("P2Miss);
        }
    }

    private void IsSuccessManage() //성공인지 실패인지 저장하는 함수
    {
        //print(_playerManager.GameOver);
        if (!(_playerManager.P1_HP <= 0 || _playerManager.P2_HP <= 0))
        {
            if (_gameManager.isRedPlayerPlaying)
            {
                if (_keyInputNumP1 == 1)
                {
                    _IsSuccessP1 = true;
                }
                else
                {
                    _IsSuccessP1 = false;
                }
            }
            else
            {
                _IsSuccessP1 = true;
            }

            if (_gameManager.isBluePlayerPlaying)
            {
                if (_keyInputNumP2 == 1)
                {
                    _IsSuccessP2 = true;
                }
                else
                {
                    _IsSuccessP2 = false;
                }
            }
            else
            {
                _IsSuccessP2 = true;
            }
        }
    }

    public void SuccessOrFailure() //동기화 시에 실행, 성공인지 실패인지와 어떤 방향인지 설정하는 함수
    {
        for (int i = 0; i < judgmentUIs.Length; i++)
        {
            //print(judgmentUIs[i].gameObject.name);
            judgmentUIs[i].SetActive(false);
        }

        if (_IsSuccessP1 && _IsSuccessP2)
        {
            judgmentUIs[0].SetActive(true);
        }
        else if (_IsSuccessP1 || _IsSuccessP2)
        {
            judgmentUIs[1].SetActive(true);
        }
        else
        {
            judgmentUIs[2].SetActive(true);
        }

        _gameManager.isRedValid = _IsSuccessP1;
        if (_whatKeyP1.Count != 0)
        {
            string key = _whatKeyP1.Dequeue();
            if (key == "Up")
            {
                _gameManager.redPlayer.direction = DIRECTION.UP;
            }
            if (key == "Left")
            {
                _gameManager.redPlayer.direction = DIRECTION.LEFT;
            }
            if (key == "Down")
            {
                _gameManager.redPlayer.direction = DIRECTION.DOWN;
            }
            if (key == "Right")
            {
                _gameManager.redPlayer.direction = DIRECTION.RIGHT;
            }
        }

        _gameManager.isBlueValid = _IsSuccessP2;
        if (_whatKeyP2.Count != 0)
        {
            string key = _whatKeyP2.Dequeue();
            if (key == "W")
            {
                _gameManager.bluePlayer.direction = DIRECTION.UP;
            }
            if (key == "A")
            {
                _gameManager.bluePlayer.direction = DIRECTION.LEFT;
            }
            if (key == "S")
            {
                _gameManager.bluePlayer.direction = DIRECTION.DOWN;
            }
            if (key == "D")
            {
                _gameManager.bluePlayer.direction = DIRECTION.RIGHT;
            }
        }

        _keyInputNumP1 = 0;
        _keyInputNumP2 = 0;

        ScoreManager.instance.NoteScore(_IsSuccessP1, _IsSuccessP2);
        ScoreManager.instance.ComboScore(_IsSuccessP1, _IsSuccessP2);

        _IsSuccessP1 = false;
        _IsSuccessP2 = false;
    }

    private void RecordNoteXPos(int player, float xPos) //오프셋 설정 시 필요함, 현재 노트의 x좌표를 넘겨줌
    {
        if (NoteManager.instance.currentBGM == "Offset")
        {
            GenerateNoteTest.instance.RecordingXPos(player, xPos);
        }
    }
}
