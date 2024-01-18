using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    GameManager _gameManager;
    PlayerManager _playerManager;

    public List<GameObject> boxNoteListP1 = new List<GameObject>(); //���� �����Ǿ���(hierarchy���� setActive�� true�� �Ǿ���) ��ħ ����(Miss Area) ���� Ÿ�ϵ��� ����Ʈ
    public List<GameObject> boxNoteListP2 = new List<GameObject>();

    [SerializeField] Transform _centerP1; //���� ������ ��� ��ġ
    [SerializeField] Transform _centerP2;
    [Tooltip("���� �������� ���� ���������� �Է�")]
    [SerializeField] RectTransform[] _timingRectP1; //������ �߰��� �� �ʿ�. ��: perfect, good, bad ��, ����� �ϳ���
    [Tooltip("���� �������� ���� ���������� �Է�")]
    [SerializeField] RectTransform[] _timingRectP2;
    Vector2[] _timingBoxsP1; //���� ������ x��ǥ, boxNoteList�� ��Ʈ�� �� �� x��ǥ �ȿ� �ִ� ��Ʈ�� �ִٸ� ����
    Vector2[] _timingBoxsP2;

    int _keyInputNumP1 = 0; //Ű �Է� ��, 2�� �̻� ��Ȯ�� Ÿ�ֿ̹� �������� �Ǵ�
    int _keyInputNumP2 = 0;
    private bool _IsSuccessP1 = false; //�����ߴ°� �����ߴ°�
    private bool _IsSuccessP2 = false;
    private Queue<string> _whatKeyP1 = new Queue<string>(); //� Ű�� �����°�
    private Queue<string> _whatKeyP2 = new Queue<string>();

    [SerializeField] GameObject _successImage; //����, ���� ���� �̹��� �׽�Ʈ��
    [SerializeField] GameObject _failureImage;
    [SerializeField] GameObject _tfSoFImage;

    private GameObject[] judgmentUIs = new GameObject[3];

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _playerManager = FindObjectOfType<PlayerManager>();

        _timingBoxsP1 = new Vector2[_timingRectP1.Length]; //���� �� ��ŭ x��ǥ ���� ����
        _timingBoxsP2 = new Vector2[_timingRectP2.Length];
        SetTimingBoxs();

        //���� ui
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
            _timingBoxsP1[i].Set(_centerP1.localPosition.x - _timingRectP1[i].rect.width / 2 - offsetP1, _centerP1.localPosition.x + _timingRectP1[i].rect.width / 2 - offsetP1); //�� ������ x��ǥ ���� ����, ��Ȯ�� �����ϼ��� ���� ����
        }
        
        for (int i = 0; i < _timingRectP2.Length; i++)
        {
            _timingBoxsP2[i].Set(_centerP2.localPosition.x - _timingRectP2[i].rect.width / 2 - offsetP2, _centerP2.localPosition.x + _timingRectP2[i].rect.width / 2 - offsetP2);
        }
    }

    public void CheckTiming(int playerNum, string key, bool forceTrue = false) //Ű�� ������ �� ����
    {
        if (playerNum == 1) //�÷��̾� 1�̶��
        {
            for (int i = 0; i < boxNoteListP1.Count; i++) //���� ���� ���� �ִ� ��Ʈ���� Ȯ���� ��Ʈ�鸸ŭ �ݺ�
            {
                float t_notePosX = boxNoteListP1[i].transform.localPosition.x; //��Ʈ�� x��ǥ �޾ƿ���
                for (int j = 0; j < _timingBoxsP1.Length; j++) //���� ������ŭ ����, ���� 1���̹Ƿ� �� ���� �����ϰ� ��
                {
                    if ((_timingBoxsP1[j].x <= t_notePosX && t_notePosX <= _timingBoxsP1[j].y) || forceTrue) //���� ���� �ȿ� �ִ°�
                    {
                        boxNoteListP1[i].GetComponent<Note>().HideNote(); //��Ʈ �̹��� ����
                        if (_keyInputNumP1 == 0) //1�� ������ ��
                        {
                            _whatKeyP1.Clear();
                            _whatKeyP1.Enqueue(key); //� Ű�� �������� ����
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

    private void IsSuccessManage() //�������� �������� �����ϴ� �Լ�
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

    public void SuccessOrFailure() //����ȭ �ÿ� ����, �������� ���������� � �������� �����ϴ� �Լ�
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

    private void RecordNoteXPos(int player, float xPos) //������ ���� �� �ʿ���, ���� ��Ʈ�� x��ǥ�� �Ѱ���
    {
        if (NoteManager.instance.currentBGM == "Offset")
        {
            GenerateNoteTest.instance.RecordingXPos(player, xPos);
        }
    }
}
