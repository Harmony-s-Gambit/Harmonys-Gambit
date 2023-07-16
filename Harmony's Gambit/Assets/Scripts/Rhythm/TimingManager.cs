using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    GameManager _gameManager;

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
    private Queue<bool> _IsSuccessP1 = new Queue<bool>(); //�����ߴ°� �����ߴ°�
    private Queue<bool> _IsSuccessP2 = new Queue<bool>();
    private Queue<string> _whatKeyP1 = new Queue<string>(); //� Ű�� �����°�
    private Queue<string> _whatKeyP2 = new Queue<string>();

    [SerializeField] GameObject _successImage; //����, ���� ���� �̹��� �׽�Ʈ��
    [SerializeField] GameObject _failureImage;
    [SerializeField] GameObject _tfSoFImage;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();

        _timingBoxsP1 = new Vector2[_timingRectP1.Length]; //���� �� ��ŭ x��ǥ ���� ����
        for (int i = 0; i < _timingRectP1.Length; i++)
        {
            _timingBoxsP1[i].Set(_centerP1.localPosition.x - _timingRectP1[i].rect.width / 2, _centerP1.localPosition.x + _timingRectP1[i].rect.width / 2); //�� ������ x��ǥ ���� ����, ��Ȯ�� �����ϼ��� ���� ����
        }

        _timingBoxsP2 = new Vector2[_timingRectP2.Length];
        for (int i = 0; i < _timingRectP2.Length; i++)
        {
            _timingBoxsP2[i].Set(_centerP2.localPosition.x - _timingRectP2[i].rect.width / 2, _centerP2.localPosition.x + _timingRectP2[i].rect.width / 2);
        }
    }

    public void CheckTiming(int playerNum, string key) //Ű�� ������ �� ����
    {
        if (playerNum == 1) //�÷��̾� 1�̶��
        {
            for (int i = 0; i < boxNoteListP1.Count; i++) //���� ���� ���� �ִ� ��Ʈ���� Ȯ���� ��Ʈ�鸸ŭ �ݺ�
            {
                float t_notePosX = boxNoteListP1[i].transform.localPosition.x; //��Ʈ�� x��ǥ �޾ƿ���
                for (int j = 0; j < _timingBoxsP1.Length; j++) //���� ������ŭ ����, ���� 1���̹Ƿ� �� ���� �����ϰ� ��
                {
                    if (_timingBoxsP1[j].x <= t_notePosX && t_notePosX <= _timingBoxsP1[j].y) //���� ���� �ȿ� �ִ°�
                    {
                        boxNoteListP1[i].GetComponent<Note>().HideNote(); //��Ʈ �̹��� ����
                        if (_keyInputNumP1 == 0) //1�� ������ ��
                        {
                            _whatKeyP1.Clear();
                            _whatKeyP1.Enqueue(key); //� Ű�� �������� ����
                        }
                        _keyInputNumP1++;

                        IsSuccessManage();

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
                    if (_timingBoxsP2[j].x <= t_notePosX && t_notePosX <= _timingBoxsP2[j].y)
                    {
                        boxNoteListP2[i].GetComponent<Note>().HideNote();
                        if (_keyInputNumP2 == 0)
                        {
                            _whatKeyP2.Clear();
                            _whatKeyP2.Enqueue(key);
                        }
                        _keyInputNumP2++;

                        IsSuccessManage();

                        return;
                    }
                }
            }
            //print("P2Miss);
        }
    }

    private void IsSuccessManage() //�������� �������� �����ϴ� �Լ�
    {
        _IsSuccessP1.Clear();
        _IsSuccessP2.Clear();

        if (_keyInputNumP1 == 1)
        {
            _IsSuccessP1.Enqueue(true);
        }
        else
        {
            _IsSuccessP1.Enqueue(false);
        }

        if ( _keyInputNumP2 == 1)
        {
            _IsSuccessP2.Enqueue(true);
        }
        else
        {
            _IsSuccessP2.Enqueue(false);
        }
    }

    public void SuccessOrFailure() //����ȭ �ÿ� ����, �������� ���������� � �������� �����ϴ� �Լ�
    {
        if (_IsSuccessP1.Count != 0)
        {
            _gameManager.isRedValid = _IsSuccessP1.Dequeue();
            if (_whatKeyP1.Count != 0)
            {
                string key = _whatKeyP1.Dequeue();
                if (key == "W")
                {
                    _gameManager.redPlayer.direction = DIRECTION.UP;
                }
                if (key == "A")
                {
                    _gameManager.redPlayer.direction = DIRECTION.LEFT;
                }
                if (key == "S")
                {
                    _gameManager.redPlayer.direction = DIRECTION.DOWN;
                }
                if (key == "D")
                {
                    _gameManager.redPlayer.direction = DIRECTION.RIGHT;
                }
            }
        }
        else
        {
            _gameManager.isRedValid = false;
        }

        if (_IsSuccessP2.Count != 0)
        {
            _gameManager.isBlueValid = _IsSuccessP2.Dequeue();
            if (_whatKeyP2.Count != 0)
            {
                string key = _whatKeyP2.Dequeue();
                if (key == "Up")
                {
                    _gameManager.bluePlayer.direction = DIRECTION.UP;
                }
                if (key == "Left")
                {
                    _gameManager.bluePlayer.direction = DIRECTION.LEFT;
                }
                if (key == "Down")
                {
                    _gameManager.bluePlayer.direction = DIRECTION.DOWN;
                }
                if (key == "Right")
                {
                    _gameManager.bluePlayer.direction = DIRECTION.RIGHT;
                }
            }
        }
        else
        {
            _gameManager.isBlueValid = false;
        }

        _keyInputNumP1 = 0;
        _keyInputNumP2 = 0;
    }
}
