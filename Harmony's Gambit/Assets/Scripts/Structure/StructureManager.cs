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
        if (!doorSetIndex.Contains(index)) //index���� �ش��ϴ� �� ��Ʈ ���� ��
        {
            doorSetIndex.Add(index); //index���� ������ �� ��Ʈ(��+��ư1+��ư2)�� �����Ǿ��ٴ� ���� ����
            GameObject door = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/Door"));
            GameObject doorOpenButton1 = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton"));
            GameObject doorOpenButton2 = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton")); //��, ��ư 2�� ����
            doors.Add(door);
            doorOpenButtons1.Add(doorOpenButton1);
            doorOpenButtons2.Add(doorOpenButton2); //��, ��ư 2�� ����Ʈ�� ����
            door.GetComponent<Door>().SetXY(x, y); //��ư ���� ���� ��ġ ����
            door.GetComponent<Door>().SetIndex(index);
            doorOpenButton1.GetComponent<DoorOpenButton>().SetIndex(index);
            doorOpenButton2.GetComponent<DoorOpenButton>().SetIndex(index); //������ ��, ��ư 2���� index�� ����
        }
        else //index���� �ش��ϴ� �� ��Ʈ�� ���� ��
        {
            for (int i = 0; i < doors.Count; i++)
            {
                if (doors[i].GetComponent<Door>().doorIndex == index) //index���� ���� �� ã��
                {
                    doors[i].GetComponent<Door>().SetXY(x, y); //��ġ ����
                }
            }
        }
    }

    public void GenerateDoorOpenButton1(int x, int y, int index)
    {
        if (!doorSetIndex.Contains(index)) //index���� �ش��ϴ� �� ��Ʈ ���� ��
        {
            doorSetIndex.Add(index); //index���� ������ �� ��Ʈ(��+��ư1+��ư2)�� �����Ǿ��ٴ� ���� ����
            GameObject door = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/Door"));
            GameObject doorOpenButton1 = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton"));
            GameObject doorOpenButton2 = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton")); //��, ��ư 2�� ����
            doors.Add(door);
            doorOpenButtons1.Add(doorOpenButton1);
            doorOpenButtons2.Add(doorOpenButton2); //��, ��ư 2�� ����Ʈ�� ����
            doorOpenButton1.GetComponent<DoorOpenButton>().SetXY(x, y); //��ư1�� ��ġ ����
            door.GetComponent<Door>().SetIndex(index);
            doorOpenButton1.GetComponent<DoorOpenButton>().SetIndex(index);
            doorOpenButton2.GetComponent<DoorOpenButton>().SetIndex(index); //������ ��, ��ư 2���� index�� ����
        }
        else //index���� �ش��ϴ� �� ��Ʈ�� ���� ��
        {
            for (int i = 0; i < doorOpenButtons1.Count; i++)
            {
                if (doorOpenButtons1[i].GetComponent<DoorOpenButton>().doorOpenButtonIndex == index) //index���� ���� ��ư ã��
                {
                    doorOpenButtons1[i].GetComponent<DoorOpenButton>().SetXY(x, y); //��ġ ����
                }
            }
        }
    }

    public void GenerateDoorOpenButton2(int x, int y, int index)
    {
        if (!doorSetIndex.Contains(index)) //index���� �ش��ϴ� �� ��Ʈ ���� ��
        {
            doorSetIndex.Add(index); //index���� ������ �� ��Ʈ(��+��ư1+��ư2)�� �����Ǿ��ٴ� ���� ����
            GameObject door = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/Door"));
            GameObject doorOpenButton1 = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton"));
            GameObject doorOpenButton2 = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton")); //��, ��ư 2�� ����
            doors.Add(door);
            doorOpenButtons1.Add(doorOpenButton1);
            doorOpenButtons2.Add(doorOpenButton2); //��, ��ư 2�� ����Ʈ�� ����
            doorOpenButton2.GetComponent<DoorOpenButton>().SetXY(x, y); //��ư2�� ��ġ ����
            door.GetComponent<Door>().SetIndex(index);
            doorOpenButton1.GetComponent<DoorOpenButton>().SetIndex(index);
            doorOpenButton2.GetComponent<DoorOpenButton>().SetIndex(index); //������ ��, ��ư 2���� index�� ����
        }
        else //index���� �ش��ϴ� �� ��Ʈ�� ���� ��
        {
            for (int i = 0; i < doorOpenButtons2.Count; i++)
            {
                if (doorOpenButtons2[i].GetComponent<DoorOpenButton>().doorOpenButtonIndex == index) //index���� ���� ��ư ã��
                {
                    doorOpenButtons2[i].GetComponent<DoorOpenButton>().SetXY(x, y); //��ġ ����
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
            if (doors[i].GetComponent<Door>().doorIndex == index) //index���� ���� �� ã��
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
                throw new System.Exception(structures[i].name + "�� ��ġ���� �ʾҽ��ϴ�.");
            }
        }
    }
}
