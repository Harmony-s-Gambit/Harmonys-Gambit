using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureManager : MonoBehaviour
{
    public bool rhythm;

    private List<GameObject> doors_Basic = new List<GameObject>();
    private List<GameObject> doorOpenButtons1_Basic = new List<GameObject>();
    private List<GameObject> doorOpenButtons2_Basic = new List<GameObject>();
    private List<int> doorSetIndex_Basic = new List<int>();

    private List<GameObject> doors_Simultaneous = new List<GameObject>();
    private List<GameObject> doorOpenButtons1_Simultaneous = new List<GameObject>();
    private List<GameObject> doorOpenButtons2_Simultaneous = new List<GameObject>();
    private List<GameObject> doorOpenButtons3_Simultaneous = new List<GameObject>();
    private List<int> doorSetIndex_Simultaneous = new List<int>();

    public void MakeStructure()
    {
        GenerateNextStageDoor(20, 0, 0);

        GenerateDoor_Simultaneous(14, 16, 1, true);
        GenerateDoorOpenButton1_Simultaneous(13, 17, 1);
        GenerateDoorOpenButton2_Simultaneous(13, 15, 1);
        GenerateDoorOpenButton3_Simultaneous(15, 17, 1);

        GenerateDoor_Simultaneous(19, 6, 2, false);
        GenerateDoorOpenButton1_Simultaneous(18, 7, 2);
        GenerateDoorOpenButton2_Simultaneous(20, 7, 2);
        GenerateDoorOpenButton3_Simultaneous(20, 5, 2);
    }

    public IEnumerator rhythmTure()
    {
        rhythm = true;
        yield return null;
        rhythm = false;
    }

    public void GenerateDoor_Basic(int x, int y, int index)
    {
        if (!doorSetIndex_Basic.Contains(index)) //index값에 해당하는 문 세트 없을 때
        {
            doorSetIndex_Basic.Add(index); //index값을 가지는 문 세트(문+버튼1+버튼2)가 생성되었다는 것을 저장
            GameObject door_Basic = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/Door_Basic"));
            GameObject doorOpenButton1_Basic = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton_Basic"));
            GameObject doorOpenButton2_Basic = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton_Basic")); //문, 버튼 2개 생성
            doors_Basic.Add(door_Basic);
            doorOpenButtons1_Basic.Add(doorOpenButton1_Basic);
            doorOpenButtons2_Basic.Add(doorOpenButton2_Basic); //문, 버튼 2개 리스트에 저장
            door_Basic.GetComponent<Door_Basic>().SetXY(x, y); //버튼 말고 문만 위치 설정
            door_Basic.GetComponent<Door_Basic>().SetIndex(index);
            doorOpenButton1_Basic.GetComponent<DoorOpenButton_Basic>().SetIndex(index);
            doorOpenButton2_Basic.GetComponent<DoorOpenButton_Basic>().SetIndex(index); //생성한 문, 버튼 2개에 index값 설정
        }
        else //index값에 해당하는 문 세트가 있을 때
        {
            for (int i = 0; i < doors_Basic.Count; i++)
            {
                if (doors_Basic[i].GetComponent<Door_Basic>().doorIndex == index) //index값과 같은 문 찾기
                {
                    doors_Basic[i].GetComponent<Door_Basic>().SetXY(x, y); //위치 설정
                }
            }
        }
    }

    public void GenerateDoorOpenButton1_Basic(int x, int y, int index)
    {
        if (!doorSetIndex_Basic.Contains(index)) //index값에 해당하는 문 세트 없을 때
        {
            doorSetIndex_Basic.Add(index); //index값을 가지는 문 세트(문+버튼1+버튼2)가 생성되었다는 것을 저장
            GameObject door_Basic = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/Door_Basic"));
            GameObject doorOpenButton1_Basic = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton_Basic"));
            GameObject doorOpenButton2_Basic = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton_Basic")); //문, 버튼 2개 생성
            doors_Basic.Add(door_Basic);
            doorOpenButtons1_Basic.Add(doorOpenButton1_Basic);
            doorOpenButtons2_Basic.Add(doorOpenButton2_Basic); //문, 버튼 2개 리스트에 저장
            doorOpenButton1_Basic.GetComponent<DoorOpenButton_Basic>().SetXY(x, y); //버튼1만 위치 설정
            door_Basic.GetComponent<Door_Basic>().SetIndex(index);
            doorOpenButton1_Basic.GetComponent<DoorOpenButton_Basic>().SetIndex(index);
            doorOpenButton2_Basic.GetComponent<DoorOpenButton_Basic>().SetIndex(index); //생성한 문, 버튼 2개에 index값 설정
        }
        else //index값에 해당하는 문 세트가 있을 때
        {
            for (int i = 0; i < doorOpenButtons1_Basic.Count; i++)
            {
                if (doorOpenButtons1_Basic[i].GetComponent<DoorOpenButton_Basic>().doorOpenButtonIndex == index) //index값과 같은 버튼 찾기
                {
                    doorOpenButtons1_Basic[i].GetComponent<DoorOpenButton_Basic>().SetXY(x, y); //위치 설정
                }
            }
        }
    }

    public void GenerateDoorOpenButton2_Basic(int x, int y, int index)
    {
        if (!doorSetIndex_Basic.Contains(index)) //index값에 해당하는 문 세트 없을 때
        {
            doorSetIndex_Basic.Add(index); //index값을 가지는 문 세트(문+버튼1+버튼2)가 생성되었다는 것을 저장
            GameObject door_Basic = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/Door_Basic"));
            GameObject doorOpenButton1_Basic = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton_Basic"));
            GameObject doorOpenButton2_Basic = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton_Basic")); //문, 버튼 2개 생성
            doors_Basic.Add(door_Basic);
            doorOpenButtons1_Basic.Add(doorOpenButton1_Basic);
            doorOpenButtons2_Basic.Add(doorOpenButton2_Basic); //문, 버튼 2개 리스트에 저장
            doorOpenButton2_Basic.GetComponent<DoorOpenButton_Basic>().SetXY(x, y); //버튼2만 위치 설정
            door_Basic.GetComponent<Door_Basic>().SetIndex(index);
            doorOpenButton1_Basic.GetComponent<DoorOpenButton_Basic>().SetIndex(index);
            doorOpenButton2_Basic.GetComponent<DoorOpenButton_Basic>().SetIndex(index); //생성한 문, 버튼 2개에 index값 설정
        }
        else //index값에 해당하는 문 세트가 있을 때
        {
            for (int i = 0; i < doorOpenButtons2_Basic.Count; i++)
            {
                if (doorOpenButtons2_Basic[i].GetComponent<DoorOpenButton_Basic>().doorOpenButtonIndex == index) //index값과 같은 버튼 찾기
                {
                    doorOpenButtons2_Basic[i].GetComponent<DoorOpenButton_Basic>().SetXY(x, y); //위치 설정
                }
            }
        }
    }

    public void GenerateNextStageDoor(int x, int y, int index)
    {
        GameObject nextStageDoor = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/NextStageDoor"));
        nextStageDoor.GetComponent<NextStageDoor>().SetXY(x, y);
        nextStageDoor.GetComponent<NextStageDoor>().SetIndex(index);
    }

    public bool TryOpenDoor_Basic(int index)
    {
        for (int i = 0; i < doors_Basic.Count; i++)
        {
            if (doors_Basic[i].GetComponent<Door_Basic>().doorIndex == index) //index값과 같은 문 찾기
            {
                if (doorOpenButtons1_Basic[i].GetComponent<DoorOpenButton_Basic>().isPressed && doorOpenButtons2_Basic[i].GetComponent<DoorOpenButton_Basic>().isPressed)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void CheckStructures()
    {
        GameObject[] structures = GameObject.FindGameObjectsWithTag("Structure");

        for (int i = 0; i < structures.Length; i++)
        {
            if (!structures[i].GetComponent<Structure>().isPlaced)
            {
                throw new System.Exception(structures[i].name + "이 설치되지 않았습니다.");
            }
        }
    }

    public void GenerateDoor_Simultaneous(int x, int y, int index,bool isVertical)
    {
        if (!doorSetIndex_Simultaneous.Contains(index)) //index값에 해당하는 문 세트 없을 때
        {
            doorSetIndex_Simultaneous.Add(index); //index값을 가지는 문 세트가 생성되었다는 것을 저장
            GameObject door_Simultaneous;
            if (isVertical)
            {
                door_Simultaneous = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/Door_Simultaneous_Vertical"));
            }
            else
            {
                door_Simultaneous = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/Door_Simultaneous"));
            }
            GameObject doorOpenButton1_Simultaneous = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton_Simultaneous"));
            GameObject doorOpenButton2_Simultaneous = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton_Simultaneous"));
            GameObject doorOpenButton3_Simultaneous = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton_Simultaneous")); //문, 버튼 3개 생성
            doors_Simultaneous.Add(door_Simultaneous);
            doorOpenButtons1_Simultaneous.Add(doorOpenButton1_Simultaneous);
            doorOpenButtons2_Simultaneous.Add(doorOpenButton2_Simultaneous);
            doorOpenButtons3_Simultaneous.Add(doorOpenButton3_Simultaneous); //문, 버튼 3개 리스트에 저장
            door_Simultaneous.GetComponent<Door_Simultaneous>().SetXY(x, y); //버튼 말고 문만 위치 설정
            door_Simultaneous.GetComponent<Door_Simultaneous>().SetIndex(index);
            doorOpenButton1_Simultaneous.GetComponent<DoorOpenButton_Simultaneous>().SetIndex(index);
            doorOpenButton2_Simultaneous.GetComponent<DoorOpenButton_Simultaneous>().SetIndex(index);
            doorOpenButton3_Simultaneous.GetComponent<DoorOpenButton_Simultaneous>().SetIndex(index); //생성한 문, 버튼 3개에 index값 설정
        }
        else //index값에 해당하는 문 세트가 있을 때
        {
            for (int i = 0; i < doors_Simultaneous.Count; i++)
            {
                if (doors_Simultaneous[i].GetComponent<Door_Simultaneous>().doorIndex == index) //index값과 같은 문 찾기
                {
                    doors_Simultaneous[i].GetComponent<Door_Simultaneous>().SetXY(x, y); //위치 설정
                }
            }
        }
    }

    public void GenerateDoorOpenButton1_Simultaneous(int x, int y, int index)
    {
        if (!doorSetIndex_Simultaneous.Contains(index)) //index값에 해당하는 문 세트 없을 때
        {
            doorSetIndex_Simultaneous.Add(index); //index값을 가지는 문 세트가 생성되었다는 것을 저장
            GameObject door_Simultaneous = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/Door_Simultaneous"));
            GameObject doorOpenButton1_Simultaneous = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton_Simultaneous"));
            GameObject doorOpenButton2_Simultaneous = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton_Simultaneous"));
            GameObject doorOpenButton3_Simultaneous = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton_Simultaneous")); //문, 버튼 3개 생성
            doors_Simultaneous.Add(door_Simultaneous);
            doorOpenButtons1_Simultaneous.Add(doorOpenButton1_Simultaneous);
            doorOpenButtons2_Simultaneous.Add(doorOpenButton2_Simultaneous);
            doorOpenButtons3_Simultaneous.Add(doorOpenButton3_Simultaneous); //문, 버튼 2개 리스트에 저장
            doorOpenButton1_Simultaneous.GetComponent<DoorOpenButton_Simultaneous>().SetXY(x, y); //버튼1만 위치 설정
            door_Simultaneous.GetComponent<Door_Simultaneous>().SetIndex(index);
            doorOpenButton1_Simultaneous.GetComponent<DoorOpenButton_Simultaneous>().SetIndex(index);
            doorOpenButton2_Simultaneous.GetComponent<DoorOpenButton_Simultaneous>().SetIndex(index);
            doorOpenButton3_Simultaneous.GetComponent<DoorOpenButton_Simultaneous>().SetIndex(index); //생성한 문, 버튼 3개에 index값 설정
        }
        else //index값에 해당하는 문 세트가 있을 때
        {
            for (int i = 0; i < doorOpenButtons1_Simultaneous.Count; i++)
            {
                if (doorOpenButtons1_Simultaneous[i].GetComponent<DoorOpenButton_Simultaneous>().doorOpenButtonIndex == index) //index값과 같은 버튼 찾기
                {
                    doorOpenButtons1_Simultaneous[i].GetComponent<DoorOpenButton_Simultaneous>().SetXY(x, y); //위치 설정
                }
            }
        }
    }

    public void GenerateDoorOpenButton2_Simultaneous(int x, int y, int index)
    {
        if (!doorSetIndex_Simultaneous.Contains(index)) //index값에 해당하는 문 세트 없을 때
        {
            doorSetIndex_Simultaneous.Add(index); //index값을 가지는 문 세트가 생성되었다는 것을 저장
            GameObject door_Simultaneous = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/Door_Simultaneous"));
            GameObject doorOpenButton1_Simultaneous = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton_Simultaneous"));
            GameObject doorOpenButton2_Simultaneous = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton_Simultaneous"));
            GameObject doorOpenButton3_Simultaneous = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton_Simultaneous")); //문, 버튼 3개 생성
            doors_Simultaneous.Add(door_Simultaneous);
            doorOpenButtons1_Simultaneous.Add(doorOpenButton1_Simultaneous);
            doorOpenButtons2_Simultaneous.Add(doorOpenButton2_Simultaneous);
            doorOpenButtons3_Simultaneous.Add(doorOpenButton3_Simultaneous); //문, 버튼 2개 리스트에 저장
            doorOpenButton2_Simultaneous.GetComponent<DoorOpenButton_Simultaneous>().SetXY(x, y); //버튼2만 위치 설정
            door_Simultaneous.GetComponent<Door_Simultaneous>().SetIndex(index);
            doorOpenButton1_Simultaneous.GetComponent<DoorOpenButton_Simultaneous>().SetIndex(index);
            doorOpenButton2_Simultaneous.GetComponent<DoorOpenButton_Simultaneous>().SetIndex(index);
            doorOpenButton3_Simultaneous.GetComponent<DoorOpenButton_Simultaneous>().SetIndex(index); //생성한 문, 버튼 3개에 index값 설정
        }
        else //index값에 해당하는 문 세트가 있을 때
        {
            for (int i = 0; i < doorOpenButtons2_Simultaneous.Count; i++)
            {
                if (doorOpenButtons2_Simultaneous[i].GetComponent<DoorOpenButton_Simultaneous>().doorOpenButtonIndex == index) //index값과 같은 버튼 찾기
                {
                    doorOpenButtons2_Simultaneous[i].GetComponent<DoorOpenButton_Simultaneous>().SetXY(x, y); //위치 설정
                }
            }
        }
    }

    public void GenerateDoorOpenButton3_Simultaneous(int x, int y, int index)
    {
        if (!doorSetIndex_Simultaneous.Contains(index)) //index값에 해당하는 문 세트 없을 때
        {
            doorSetIndex_Simultaneous.Add(index); //index값을 가지는 문 세트가 생성되었다는 것을 저장
            GameObject door_Simultaneous = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/Door_Simultaneous"));
            GameObject doorOpenButton1_Simultaneous = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton_Simultaneous"));
            GameObject doorOpenButton2_Simultaneous = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton_Simultaneous"));
            GameObject doorOpenButton3_Simultaneous = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton_Simultaneous")); //문, 버튼 3개 생성
            doors_Simultaneous.Add(door_Simultaneous);
            doorOpenButtons1_Simultaneous.Add(doorOpenButton1_Simultaneous);
            doorOpenButtons2_Simultaneous.Add(doorOpenButton2_Simultaneous);
            doorOpenButtons3_Simultaneous.Add(doorOpenButton3_Simultaneous); //문, 버튼 2개 리스트에 저장
            doorOpenButton3_Simultaneous.GetComponent<DoorOpenButton_Simultaneous>().SetXY(x, y); //버튼3만 위치 설정
            door_Simultaneous.GetComponent<Door_Simultaneous>().SetIndex(index);
            doorOpenButton1_Simultaneous.GetComponent<DoorOpenButton_Simultaneous>().SetIndex(index);
            doorOpenButton2_Simultaneous.GetComponent<DoorOpenButton_Simultaneous>().SetIndex(index);
            doorOpenButton3_Simultaneous.GetComponent<DoorOpenButton_Simultaneous>().SetIndex(index); //생성한 문, 버튼 3개에 index값 설정
            doorOpenButton3_Simultaneous.GetComponent<DoorOpenButton_Simultaneous>().SetIndex2(1);
        }
        else //index값에 해당하는 문 세트가 있을 때
        {
            for (int i = 0; i < doorOpenButtons3_Simultaneous.Count; i++)
            {
                if (doorOpenButtons3_Simultaneous[i].GetComponent<DoorOpenButton_Simultaneous>().doorOpenButtonIndex == index) //index값과 같은 버튼 찾기
                {
                    doorOpenButtons3_Simultaneous[i].GetComponent<DoorOpenButton_Simultaneous>().SetXY(x, y); //위치 설정
                    doorOpenButtons3_Simultaneous[i].GetComponent<DoorOpenButton_Simultaneous>().SetIndex2(1);
                }
            }
        }
    }

    public bool TryOpenDoor_Simultaneous(int index)
    {
        for (int i = 0; i < doors_Simultaneous.Count; i++)
        {
            if (doors_Simultaneous[i].GetComponent<Door_Simultaneous>().doorIndex == index) //index값과 같은 문 찾기
            {
                if ((doorOpenButtons1_Simultaneous[i].GetComponent<DoorOpenButton_Simultaneous>().isPressed && doorOpenButtons2_Simultaneous[i].GetComponent<DoorOpenButton_Simultaneous>().isPressed) || doorOpenButtons3_Simultaneous[i].GetComponent<DoorOpenButton_Simultaneous>().isPressed)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
