using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeOverFieldManager : MonoBehaviour
{
    private GameObject nextStageDoorObj;
    private GridSlotInfo[] gridSlotInfos;

    private List<GameObject> timeOverField = new List<GameObject>(); //�ڱ����� ����� ����Ʈ
    private List<int> timeOverFieldFirstPosition = new List<int>(); //�ڱ����� ó�� ��ġ
    private int firstMovingDistance = 1000; //ó�� �̵��� �Ÿ�
    private int firstMovingSpeed = 1000; //ó�� �̵� �ӵ�
    private float time = 15000f; //�� ���� ������ ���ΰ�
    private bool secondMove = false; //�ι�° �̵� ����
    private float timer = 0; //�ι�° �̵� �� �ʿ�
    private bool test = false;

    void Update()
    {
        if (nextStageDoorObj == null) //���� �������� �� ���ӿ�����Ʈ �����ϱ�
        {
            try
            {
                if (StageInfo.instance.GetStageName().Contains("Stage1_1"))
                {
                    nextStageDoorObj = GameObject.Find("NextStageDoor(Clone)");
                }
            }
            catch (System.Exception)
            {
                return;
            }
        }

        if (gridSlotInfos == null) //��� �׸��彽������ ��ũ��Ʈ �����ϱ�
        {
            try
            {
                gridSlotInfos = FindObjectsOfType<GridSlotInfo>();
            }
            catch (System.Exception)
            {
                return;
            }

            GenerateTimeOverField();
        }


        if (ScoreManager.instance.isTimeOver && !secondMove)
        {
            FirstMoving();
        }

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    test = true;
        //}
    }

    private void FixedUpdate()
    {
        if (secondMove)
        {
            SecondMoving();
        }
    }

    private void SecondMoving()
    {
        //timeOverField[0].transform.Translate(new Vector3(Mathf.Abs(nextStageDoorObj.transform.position.x - timeOverFieldFirstPosition[0]) / time * -1, 0, 0) * Time.deltaTime);
        timer += Time.deltaTime / time;

        timeOverField[0].transform.position = new Vector3(Mathf.Lerp(timeOverField[0].transform.position.x, nextStageDoorObj.transform.position.x + timeOverField[0].transform.localScale.x / 2, timer), timeOverField[0].transform.position.y, 0);
        timeOverField[1].transform.position = new Vector3(Mathf.Lerp(timeOverField[1].transform.position.x, nextStageDoorObj.transform.position.x - timeOverField[1].transform.localScale.x / 2, timer), timeOverField[1].transform.position.y, 0);
        timeOverField[2].transform.position = new Vector3(timeOverField[2].transform.position.x, Mathf.Lerp(timeOverField[2].transform.position.y, nextStageDoorObj.transform.position.y + timeOverField[2].transform.localScale.y / 2, timer), 0);
        timeOverField[3].transform.position = new Vector3(timeOverField[3].transform.position.x, Mathf.Lerp(timeOverField[3].transform.position.y, nextStageDoorObj.transform.position.y - timeOverField[3].transform.localScale.y / 2, timer), 0);
    }

    private void FirstMoving()
    {
        if (timeOverField[0].transform.position.x > timeOverFieldFirstPosition[0] - firstMovingDistance)
        {
            timeOverField[0].transform.Translate(new Vector3(-firstMovingSpeed, 0, 0) * Time.deltaTime);
        }

        if (timeOverField[1].transform.position.x < timeOverFieldFirstPosition[1] + firstMovingDistance)
        {
            timeOverField[1].transform.Translate(new Vector3(firstMovingSpeed, 0, 0) * Time.deltaTime);
        }

        if (timeOverField[2].transform.position.y > timeOverFieldFirstPosition[2] - firstMovingDistance)
        {
            timeOverField[2].transform.Translate(new Vector3(0, -firstMovingSpeed, 0) * Time.deltaTime);
        }

        if (timeOverField[3].transform.position.y < timeOverFieldFirstPosition[3] + firstMovingDistance)
        {
            timeOverField[3].transform.Translate(new Vector3(0, firstMovingSpeed, 0) * Time.deltaTime);
        }

        if (timeOverField[0].transform.position.x < timeOverFieldFirstPosition[0] - firstMovingDistance)
        {
            secondMove = true;
        }
    }

    private void GenerateTimeOverField()
    {
        GameObject temp;

        timeOverField.Add((GameObject)Instantiate(Resources.Load("Prefabs/Map/Field")));
        temp = GameObject.Find(MaxMinXY(true, true, true) + "_" + MaxMinXY(true, true, false));
        timeOverField[0].transform.position = new Vector3(temp.transform.position.x + timeOverField[0].transform.localScale.x / 2 + firstMovingDistance, temp.transform.position.y, 0);

        timeOverField.Add((GameObject)Instantiate(Resources.Load("Prefabs/Map/Field")));
        temp = GameObject.Find(MaxMinXY(true, false, true) + "_" + MaxMinXY(true, false, false));
        timeOverField[1].transform.position = new Vector3(temp.transform.position.x - timeOverField[1].transform.localScale.x / 2 - firstMovingDistance, temp.transform.position.y, 0);

        timeOverField.Add((GameObject)Instantiate(Resources.Load("Prefabs/Map/Field")));
        temp = GameObject.Find(MaxMinXY(false, true, true) + "_" + MaxMinXY(false, true, false));
        timeOverField[2].transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y + timeOverField[2].transform.localScale.y / 2 + firstMovingDistance, 0);

        timeOverField.Add((GameObject)Instantiate(Resources.Load("Prefabs/Map/Field")));
        temp = GameObject.Find(MaxMinXY(false, false, true) + "_" + MaxMinXY(false, false, false));
        timeOverField[3].transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y - timeOverField[3].transform.localScale.y / 2 - firstMovingDistance, 0);

        timeOverFieldFirstPosition.Add((int)timeOverField[0].transform.position.x);
        timeOverFieldFirstPosition.Add((int)timeOverField[1].transform.position.x);
        timeOverFieldFirstPosition.Add((int)timeOverField[2].transform.position.y);
        timeOverFieldFirstPosition.Add((int)timeOverField[3].transform.position.y);
    }

    private string MaxMinXY(bool _xy, bool _maxmin, bool _index) //�ִ� x��ǥ�� y��ǥ ã��, true�� x��ǥ�� �ִ��� Ÿ�� �� �ϳ��� ��ġ�� ��ȯ��, false�� y, true�� max, false�� min, true�� 0, false�� 1
    {
        Vector2 _max = new Vector2(gridSlotInfos[0].x, gridSlotInfos[0].y);
        Vector2 _min = new Vector2(gridSlotInfos[0].x, gridSlotInfos[0].y);

        if (_maxmin)
        {
            if (_xy)
            {
                for (int i = 0; i < gridSlotInfos.Length; i++)
                {
                    if (gridSlotInfos[i].x > _max[0])
                    {
                        _max[0] = gridSlotInfos[i].x;
                        _max[1] = gridSlotInfos[i].y;
                    }
                }
            }
            else
            {
                for (int i = 0; i < gridSlotInfos.Length; i++)
                {
                    if (gridSlotInfos[i].y > _max[1])
                    {
                        _max[0] = gridSlotInfos[i].x;
                        _max[1] = gridSlotInfos[i].y;
                    }
                }
            }

            if (_index)
            {
                return ((int)_max[0]).ToString();
            }
            else
            {
                return ((int)_max[1]).ToString();
            }
        }
        else
        {
            if (_xy)
            {
                for (int i = 0; i < gridSlotInfos.Length; i++)
                {
                    if (gridSlotInfos[i].x < _min[0])
                    {
                        _min[0] = gridSlotInfos[i].x;
                        _min[1] = gridSlotInfos[i].y;
                    }
                }
            }
            else
            {
                for (int i = 0; i < gridSlotInfos.Length; i++)
                {
                    if (gridSlotInfos[i].y < _min[1])
                    {
                        _min[0] = gridSlotInfos[i].x;
                        _min[1] = gridSlotInfos[i].y;
                    }
                }
            }

            if (_index)
            {
                return ((int)_min[0]).ToString();
            }
            else
            {
                return ((int)_min[1]).ToString();
            }
        }
    }
}
