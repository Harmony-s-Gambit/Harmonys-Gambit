using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureManager : MonoBehaviour
{
    public bool rhythm;

    private List<GameObject> doors = new List<GameObject>();
    private List<GameObject> doorOpenButtons1 = new List<GameObject>();
    private List<GameObject> doorOpenButtons2 = new List<GameObject>();
    private List<int> doorSetIndex = new List<int>();

    void Start()
    {
        StartCoroutine(Delay());
    }

    public IEnumerator rhythmTure()
    {
        rhythm = true;
        yield return null;
        rhythm = false;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.1f);
        //GenerateDoorOpenButton2(4, 5, 2);
        //GenerateDoor(4, 3, 2);

        //GenerateDoorOpenButton2(3, 5, 1);
        //GenerateDoor(3, 3, 1);
        //GenerateDoorOpenButton1(3, 4, 1);

        //GenerateNextStageDoor(5, 5, 1);
        //GenerateNextStageDoor(5, 6, 2);

        //GenerateDoorOpenButton1(4, 4, 2);

        CheckStructures();
    }

    public void GenerateDoor(int x, int y, int index)
    {
        if (!doorSetIndex.Contains(index)) //index값에 해당하는 문 세트 없을 때
        {
            doorSetIndex.Add(index); //index값을 가지는 문 세트(문+버튼1+버튼2)가 생성되었다는 것을 저장
            GameObject door = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/Door"));
            GameObject doorOpenButton1 = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton"));
            GameObject doorOpenButton2 = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton")); //문, 버튼 2개 생성
            doors.Add(door);
            doorOpenButtons1.Add(doorOpenButton1);
            doorOpenButtons2.Add(doorOpenButton2); //문, 버튼 2개 리스트에 저장
            door.GetComponent<Door>().SetXY(x, y); //버튼 말고 문만 위치 설정
            door.GetComponent<Door>().SetIndex(index);
            doorOpenButton1.GetComponent<DoorOpenButton>().SetIndex(index);
            doorOpenButton2.GetComponent<DoorOpenButton>().SetIndex(index); //생성한 문, 버튼 2개에 index값 설정
        }
        else //index값에 해당하는 문 세트가 있을 때
        {
            for (int i = 0; i < doors.Count; i++)
            {
                if (doors[i].GetComponent<Door>().doorIndex == index) //index값과 같은 문 찾기
                {
                    doors[i].GetComponent<Door>().SetXY(x, y); //위치 설정
                }
            }
        }
    }

    public void GenerateDoorOpenButton1(int x, int y, int index)
    {
        if (!doorSetIndex.Contains(index)) //index값에 해당하는 문 세트 없을 때
        {
            doorSetIndex.Add(index); //index값을 가지는 문 세트(문+버튼1+버튼2)가 생성되었다는 것을 저장
            GameObject door = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/Door"));
            GameObject doorOpenButton1 = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton"));
            GameObject doorOpenButton2 = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton")); //문, 버튼 2개 생성
            doors.Add(door);
            doorOpenButtons1.Add(doorOpenButton1);
            doorOpenButtons2.Add(doorOpenButton2); //문, 버튼 2개 리스트에 저장
            doorOpenButton1.GetComponent<DoorOpenButton>().SetXY(x, y); //버튼1만 위치 설정
            door.GetComponent<Door>().SetIndex(index);
            doorOpenButton1.GetComponent<DoorOpenButton>().SetIndex(index);
            doorOpenButton2.GetComponent<DoorOpenButton>().SetIndex(index); //생성한 문, 버튼 2개에 index값 설정
        }
        else //index값에 해당하는 문 세트가 있을 때
        {
            for (int i = 0; i < doorOpenButtons1.Count; i++)
            {
                if (doorOpenButtons1[i].GetComponent<DoorOpenButton>().doorOpenButtonIndex == index) //index값과 같은 버튼 찾기
                {
                    doorOpenButtons1[i].GetComponent<DoorOpenButton>().SetXY(x, y); //위치 설정
                }
            }
        }
    }

    public void GenerateDoorOpenButton2(int x, int y, int index)
    {
        if (!doorSetIndex.Contains(index)) //index값에 해당하는 문 세트 없을 때
        {
            doorSetIndex.Add(index); //index값을 가지는 문 세트(문+버튼1+버튼2)가 생성되었다는 것을 저장
            GameObject door = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/Door"));
            GameObject doorOpenButton1 = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton"));
            GameObject doorOpenButton2 = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton")); //문, 버튼 2개 생성
            doors.Add(door);
            doorOpenButtons1.Add(doorOpenButton1);
            doorOpenButtons2.Add(doorOpenButton2); //문, 버튼 2개 리스트에 저장
            doorOpenButton2.GetComponent<DoorOpenButton>().SetXY(x, y); //버튼2만 위치 설정
            door.GetComponent<Door>().SetIndex(index);
            doorOpenButton1.GetComponent<DoorOpenButton>().SetIndex(index);
            doorOpenButton2.GetComponent<DoorOpenButton>().SetIndex(index); //생성한 문, 버튼 2개에 index값 설정
        }
        else //index값에 해당하는 문 세트가 있을 때
        {
            for (int i = 0; i < doorOpenButtons2.Count; i++)
            {
                if (doorOpenButtons2[i].GetComponent<DoorOpenButton>().doorOpenButtonIndex == index) //index값과 같은 버튼 찾기
                {
                    doorOpenButtons2[i].GetComponent<DoorOpenButton>().SetXY(x, y); //위치 설정
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

    public bool TryOpenDoor(int index)
    {
        for (int i = 0; i < doors.Count; i++)
        {
            if (doors[i].GetComponent<Door>().doorIndex == index) //index값과 같은 문 찾기
            {
                if (doorOpenButtons1[i].GetComponent<DoorOpenButton>().isPressed && doorOpenButtons2[i].GetComponent<DoorOpenButton>().isPressed)
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
}
