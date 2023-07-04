using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    public List<GameObject> boxNoteListP1 = new List<GameObject>();
    public List<GameObject> boxNoteListP2 = new List<GameObject>();

    [SerializeField] Transform _centerP1;
    [SerializeField] Transform _centerP2;
    [Tooltip("좋은 판정부터 나쁜판정순으로 입력")]
    [SerializeField] RectTransform[] _timingRectP1;
    [Tooltip("좋은 판정부터 나쁜판정순으로 입력")]
    [SerializeField] RectTransform[] _timingRectP2;
    Vector2[] _timingBoxsP1;
    Vector2[] _timingBoxsP2;

    private void Start()
    {
        _timingBoxsP1 = new Vector2[_timingRectP1.Length];
        for (int i = 0; i < _timingRectP1.Length; i++)
        {
            _timingBoxsP1[i].Set(_centerP1.localPosition.x - _timingRectP1[i].rect.width / 2, _centerP1.localPosition.x + _timingRectP1[i].rect.width / 2);
        }

        _timingBoxsP2 = new Vector2[_timingRectP2.Length];
        for (int i = 0; i < _timingRectP2.Length; i++)
        {
            _timingBoxsP2[i].Set(_centerP2.localPosition.x - _timingRectP2[i].rect.width / 2, _centerP2.localPosition.x + _timingRectP2[i].rect.width / 2);
        }
    }

    public void CheckTiming(int playerNum, string key)
    {
        if (playerNum == 1)
        {
            for (int i = 0; i < boxNoteListP1.Count; i++)
            {
                float t_notePosX = boxNoteListP1[i].transform.localPosition.x;
                for (int j = 0; j < _timingBoxsP1.Length; j++)
                {
                    if (_timingBoxsP1[j].x <= t_notePosX && t_notePosX <= _timingBoxsP1[j].y)
                    {
                        ObjectPool.instance.noteQueueP1.Enqueue(boxNoteListP1[i].gameObject);
                        boxNoteListP1[i].gameObject.SetActive(false);
                        boxNoteListP1.RemoveAt(i);
                        //print("P1" + j + key);
                        return;
                    }
                }
            }
            //print("P1Miss");
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
                        ObjectPool.instance.noteQueueP1.Enqueue(boxNoteListP2[i].gameObject);
                        boxNoteListP2[i].gameObject.SetActive(false);
                        boxNoteListP2.RemoveAt(i);
                        //print("P2" + j + key);
                        return;
                    }
                }
            }
            //print("P2Miss");
        }
    }
}
