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
        if (!doorSetIndex_Basic.Contains(index)) //index���� �ش��ϴ� �� ��Ʈ ���� ��
        {
            doorSetIndex_Basic.Add(index); //index���� ������ �� ��Ʈ(��+��ư1+��ư2)�� �����Ǿ��ٴ� ���� ����
            GameObject door_Basic = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/Door_Basic"));
            GameObject doorOpenButton1_Basic = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton_Basic"));
            GameObject doorOpenButton2_Basic = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton_Basic")); //��, ��ư 2�� ����
            doors_Basic.Add(door_Basic);
            doorOpenButtons1_Basic.Add(doorOpenButton1_Basic);
            doorOpenButtons2_Basic.Add(doorOpenButton2_Basic); //��, ��ư 2�� ����Ʈ�� ����
            door_Basic.GetComponent<Door_Basic>().SetXY(x, y); //��ư ���� ���� ��ġ ����
            door_Basic.GetComponent<Door_Basic>().SetIndex(index);
            doorOpenButton1_Basic.GetComponent<DoorOpenButton_Basic>().SetIndex(index);
            doorOpenButton2_Basic.GetComponent<DoorOpenButton_Basic>().SetIndex(index); //������ ��, ��ư 2���� index�� ����
        }
        else //index���� �ش��ϴ� �� ��Ʈ�� ���� ��
        {
            for (int i = 0; i < doors_Basic.Count; i++)
            {
                if (doors_Basic[i].GetComponent<Door_Basic>().doorIndex == index) //index���� ���� �� ã��
                {
                    doors_Basic[i].GetComponent<Door_Basic>().SetXY(x, y); //��ġ ����
                }
            }
        }
    }

    public void GenerateDoorOpenButton1_Basic(int x, int y, int index)
    {
        if (!doorSetIndex_Basic.Contains(index)) //index���� �ش��ϴ� �� ��Ʈ ���� ��
        {
            doorSetIndex_Basic.Add(index); //index���� ������ �� ��Ʈ(��+��ư1+��ư2)�� �����Ǿ��ٴ� ���� ����
            GameObject door_Basic = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/Door_Basic"));
            GameObject doorOpenButton1_Basic = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton_Basic"));
            GameObject doorOpenButton2_Basic = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton_Basic")); //��, ��ư 2�� ����
            doors_Basic.Add(door_Basic);
            doorOpenButtons1_Basic.Add(doorOpenButton1_Basic);
            doorOpenButtons2_Basic.Add(doorOpenButton2_Basic); //��, ��ư 2�� ����Ʈ�� ����
            doorOpenButton1_Basic.GetComponent<DoorOpenButton_Basic>().SetXY(x, y); //��ư1�� ��ġ ����
            door_Basic.GetComponent<Door_Basic>().SetIndex(index);
            doorOpenButton1_Basic.GetComponent<DoorOpenButton_Basic>().SetIndex(index);
            doorOpenButton2_Basic.GetComponent<DoorOpenButton_Basic>().SetIndex(index); //������ ��, ��ư 2���� index�� ����
        }
        else //index���� �ش��ϴ� �� ��Ʈ�� ���� ��
        {
            for (int i = 0; i < doorOpenButtons1_Basic.Count; i++)
            {
                if (doorOpenButtons1_Basic[i].GetComponent<DoorOpenButton_Basic>().doorOpenButtonIndex == index) //index���� ���� ��ư ã��
                {
                    doorOpenButtons1_Basic[i].GetComponent<DoorOpenButton_Basic>().SetXY(x, y); //��ġ ����
                }
            }
        }
    }

    public void GenerateDoorOpenButton2_Basic(int x, int y, int index)
    {
        if (!doorSetIndex_Basic.Contains(index)) //index���� �ش��ϴ� �� ��Ʈ ���� ��
        {
            doorSetIndex_Basic.Add(index); //index���� ������ �� ��Ʈ(��+��ư1+��ư2)�� �����Ǿ��ٴ� ���� ����
            GameObject door_Basic = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/Door_Basic"));
            GameObject doorOpenButton1_Basic = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton_Basic"));
            GameObject doorOpenButton2_Basic = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton_Basic")); //��, ��ư 2�� ����
            doors_Basic.Add(door_Basic);
            doorOpenButtons1_Basic.Add(doorOpenButton1_Basic);
            doorOpenButtons2_Basic.Add(doorOpenButton2_Basic); //��, ��ư 2�� ����Ʈ�� ����
            doorOpenButton2_Basic.GetComponent<DoorOpenButton_Basic>().SetXY(x, y); //��ư2�� ��ġ ����
            door_Basic.GetComponent<Door_Basic>().SetIndex(index);
            doorOpenButton1_Basic.GetComponent<DoorOpenButton_Basic>().SetIndex(index);
            doorOpenButton2_Basic.GetComponent<DoorOpenButton_Basic>().SetIndex(index); //������ ��, ��ư 2���� index�� ����
        }
        else //index���� �ش��ϴ� �� ��Ʈ�� ���� ��
        {
            for (int i = 0; i < doorOpenButtons2_Basic.Count; i++)
            {
                if (doorOpenButtons2_Basic[i].GetComponent<DoorOpenButton_Basic>().doorOpenButtonIndex == index) //index���� ���� ��ư ã��
                {
                    doorOpenButtons2_Basic[i].GetComponent<DoorOpenButton_Basic>().SetXY(x, y); //��ġ ����
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
            if (doors_Basic[i].GetComponent<Door_Basic>().doorIndex == index) //index���� ���� �� ã��
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
                throw new System.Exception(structures[i].name + "�� ��ġ���� �ʾҽ��ϴ�.");
            }
        }
    }

    public void GenerateDoor_Simultaneous(int x, int y, int index,bool isVertical)
    {
        if (!doorSetIndex_Simultaneous.Contains(index)) //index���� �ش��ϴ� �� ��Ʈ ���� ��
        {
            doorSetIndex_Simultaneous.Add(index); //index���� ������ �� ��Ʈ�� �����Ǿ��ٴ� ���� ����
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
            GameObject doorOpenButton3_Simultaneous = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton_Simultaneous")); //��, ��ư 3�� ����
            doors_Simultaneous.Add(door_Simultaneous);
            doorOpenButtons1_Simultaneous.Add(doorOpenButton1_Simultaneous);
            doorOpenButtons2_Simultaneous.Add(doorOpenButton2_Simultaneous);
            doorOpenButtons3_Simultaneous.Add(doorOpenButton3_Simultaneous); //��, ��ư 3�� ����Ʈ�� ����
            door_Simultaneous.GetComponent<Door_Simultaneous>().SetXY(x, y); //��ư ���� ���� ��ġ ����
            door_Simultaneous.GetComponent<Door_Simultaneous>().SetIndex(index);
            doorOpenButton1_Simultaneous.GetComponent<DoorOpenButton_Simultaneous>().SetIndex(index);
            doorOpenButton2_Simultaneous.GetComponent<DoorOpenButton_Simultaneous>().SetIndex(index);
            doorOpenButton3_Simultaneous.GetComponent<DoorOpenButton_Simultaneous>().SetIndex(index); //������ ��, ��ư 3���� index�� ����
        }
        else //index���� �ش��ϴ� �� ��Ʈ�� ���� ��
        {
            for (int i = 0; i < doors_Simultaneous.Count; i++)
            {
                if (doors_Simultaneous[i].GetComponent<Door_Simultaneous>().doorIndex == index) //index���� ���� �� ã��
                {
                    doors_Simultaneous[i].GetComponent<Door_Simultaneous>().SetXY(x, y); //��ġ ����
                }
            }
        }
    }

    public void GenerateDoorOpenButton1_Simultaneous(int x, int y, int index)
    {
        if (!doorSetIndex_Simultaneous.Contains(index)) //index���� �ش��ϴ� �� ��Ʈ ���� ��
        {
            doorSetIndex_Simultaneous.Add(index); //index���� ������ �� ��Ʈ�� �����Ǿ��ٴ� ���� ����
            GameObject door_Simultaneous = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/Door_Simultaneous"));
            GameObject doorOpenButton1_Simultaneous = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton_Simultaneous"));
            GameObject doorOpenButton2_Simultaneous = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton_Simultaneous"));
            GameObject doorOpenButton3_Simultaneous = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton_Simultaneous")); //��, ��ư 3�� ����
            doors_Simultaneous.Add(door_Simultaneous);
            doorOpenButtons1_Simultaneous.Add(doorOpenButton1_Simultaneous);
            doorOpenButtons2_Simultaneous.Add(doorOpenButton2_Simultaneous);
            doorOpenButtons3_Simultaneous.Add(doorOpenButton3_Simultaneous); //��, ��ư 2�� ����Ʈ�� ����
            doorOpenButton1_Simultaneous.GetComponent<DoorOpenButton_Simultaneous>().SetXY(x, y); //��ư1�� ��ġ ����
            door_Simultaneous.GetComponent<Door_Simultaneous>().SetIndex(index);
            doorOpenButton1_Simultaneous.GetComponent<DoorOpenButton_Simultaneous>().SetIndex(index);
            doorOpenButton2_Simultaneous.GetComponent<DoorOpenButton_Simultaneous>().SetIndex(index);
            doorOpenButton3_Simultaneous.GetComponent<DoorOpenButton_Simultaneous>().SetIndex(index); //������ ��, ��ư 3���� index�� ����
        }
        else //index���� �ش��ϴ� �� ��Ʈ�� ���� ��
        {
            for (int i = 0; i < doorOpenButtons1_Simultaneous.Count; i++)
            {
                if (doorOpenButtons1_Simultaneous[i].GetComponent<DoorOpenButton_Simultaneous>().doorOpenButtonIndex == index) //index���� ���� ��ư ã��
                {
                    doorOpenButtons1_Simultaneous[i].GetComponent<DoorOpenButton_Simultaneous>().SetXY(x, y); //��ġ ����
                }
            }
        }
    }

    public void GenerateDoorOpenButton2_Simultaneous(int x, int y, int index)
    {
        if (!doorSetIndex_Simultaneous.Contains(index)) //index���� �ش��ϴ� �� ��Ʈ ���� ��
        {
            doorSetIndex_Simultaneous.Add(index); //index���� ������ �� ��Ʈ�� �����Ǿ��ٴ� ���� ����
            GameObject door_Simultaneous = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/Door_Simultaneous"));
            GameObject doorOpenButton1_Simultaneous = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton_Simultaneous"));
            GameObject doorOpenButton2_Simultaneous = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton_Simultaneous"));
            GameObject doorOpenButton3_Simultaneous = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton_Simultaneous")); //��, ��ư 3�� ����
            doors_Simultaneous.Add(door_Simultaneous);
            doorOpenButtons1_Simultaneous.Add(doorOpenButton1_Simultaneous);
            doorOpenButtons2_Simultaneous.Add(doorOpenButton2_Simultaneous);
            doorOpenButtons3_Simultaneous.Add(doorOpenButton3_Simultaneous); //��, ��ư 2�� ����Ʈ�� ����
            doorOpenButton2_Simultaneous.GetComponent<DoorOpenButton_Simultaneous>().SetXY(x, y); //��ư2�� ��ġ ����
            door_Simultaneous.GetComponent<Door_Simultaneous>().SetIndex(index);
            doorOpenButton1_Simultaneous.GetComponent<DoorOpenButton_Simultaneous>().SetIndex(index);
            doorOpenButton2_Simultaneous.GetComponent<DoorOpenButton_Simultaneous>().SetIndex(index);
            doorOpenButton3_Simultaneous.GetComponent<DoorOpenButton_Simultaneous>().SetIndex(index); //������ ��, ��ư 3���� index�� ����
        }
        else //index���� �ش��ϴ� �� ��Ʈ�� ���� ��
        {
            for (int i = 0; i < doorOpenButtons2_Simultaneous.Count; i++)
            {
                if (doorOpenButtons2_Simultaneous[i].GetComponent<DoorOpenButton_Simultaneous>().doorOpenButtonIndex == index) //index���� ���� ��ư ã��
                {
                    doorOpenButtons2_Simultaneous[i].GetComponent<DoorOpenButton_Simultaneous>().SetXY(x, y); //��ġ ����
                }
            }
        }
    }

    public void GenerateDoorOpenButton3_Simultaneous(int x, int y, int index)
    {
        if (!doorSetIndex_Simultaneous.Contains(index)) //index���� �ش��ϴ� �� ��Ʈ ���� ��
        {
            doorSetIndex_Simultaneous.Add(index); //index���� ������ �� ��Ʈ�� �����Ǿ��ٴ� ���� ����
            GameObject door_Simultaneous = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/Door_Simultaneous"));
            GameObject doorOpenButton1_Simultaneous = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton_Simultaneous"));
            GameObject doorOpenButton2_Simultaneous = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton_Simultaneous"));
            GameObject doorOpenButton3_Simultaneous = (GameObject)Instantiate(Resources.Load("Prefabs/Structures/DoorOpenButton_Simultaneous")); //��, ��ư 3�� ����
            doors_Simultaneous.Add(door_Simultaneous);
            doorOpenButtons1_Simultaneous.Add(doorOpenButton1_Simultaneous);
            doorOpenButtons2_Simultaneous.Add(doorOpenButton2_Simultaneous);
            doorOpenButtons3_Simultaneous.Add(doorOpenButton3_Simultaneous); //��, ��ư 2�� ����Ʈ�� ����
            doorOpenButton3_Simultaneous.GetComponent<DoorOpenButton_Simultaneous>().SetXY(x, y); //��ư3�� ��ġ ����
            door_Simultaneous.GetComponent<Door_Simultaneous>().SetIndex(index);
            doorOpenButton1_Simultaneous.GetComponent<DoorOpenButton_Simultaneous>().SetIndex(index);
            doorOpenButton2_Simultaneous.GetComponent<DoorOpenButton_Simultaneous>().SetIndex(index);
            doorOpenButton3_Simultaneous.GetComponent<DoorOpenButton_Simultaneous>().SetIndex(index); //������ ��, ��ư 3���� index�� ����
            doorOpenButton3_Simultaneous.GetComponent<DoorOpenButton_Simultaneous>().SetIndex2(1);
        }
        else //index���� �ش��ϴ� �� ��Ʈ�� ���� ��
        {
            for (int i = 0; i < doorOpenButtons3_Simultaneous.Count; i++)
            {
                if (doorOpenButtons3_Simultaneous[i].GetComponent<DoorOpenButton_Simultaneous>().doorOpenButtonIndex == index) //index���� ���� ��ư ã��
                {
                    doorOpenButtons3_Simultaneous[i].GetComponent<DoorOpenButton_Simultaneous>().SetXY(x, y); //��ġ ����
                    doorOpenButtons3_Simultaneous[i].GetComponent<DoorOpenButton_Simultaneous>().SetIndex2(1);
                }
            }
        }
    }

    public bool TryOpenDoor_Simultaneous(int index)
    {
        for (int i = 0; i < doors_Simultaneous.Count; i++)
        {
            if (doors_Simultaneous[i].GetComponent<Door_Simultaneous>().doorIndex == index) //index���� ���� �� ã��
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
