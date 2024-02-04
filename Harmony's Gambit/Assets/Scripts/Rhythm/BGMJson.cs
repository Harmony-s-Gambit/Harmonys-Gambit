using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMJsonFile
{
    public string bgmName; //�� �̸�
    public double bpm; //�� bpm
    public double time; //�� �ð�(��)
    public float delay; //�� delay, ��Ʈ�� ���� ���� ����� ������ �� �� �Ŀ� ���� ��� ��ų ���ΰ�
    public List<double> beatList; //���ڵ�

    public double beatAfterTimeOver; //Ÿ�� ���� ���� ����
    public string bgmNameAfterTimeOver; //Ÿ�� ���� ���� ����
}

public class BGMJson : MonoBehaviour
{
    public static BGMJson instance;
    public BGMJsonFile[] bgmJsonFiles = new BGMJsonFile[3]; //�� �߰��� ������ �ε��� ���� �ʿ�

    private const int bgm1 = 0; //�� �߰��� ������ ��� �߰� �ʿ�
    private const int offset = 1; //�� �߰��� ������ ��� �߰� �ʿ�
    private const int discoHeart = 2; //�� �߰��� ������ ��� �߰� �ʿ�

    private void Awake()
    {
        for (int i = 0; i < bgmJsonFiles.Length; i++)
        {
            bgmJsonFiles[i] = new BGMJsonFile();
        }
    }

    private void Start()
    {
        instance = this;
        MakeJson_BGM1(); //�� �߰��� ������ �Լ� �߰� �ʿ�
        MakeJson_Offset();
        MakeJson_DiscoHeart();
    }

    public int CurrentBGMindex(string bgmName)
    {
        if (bgmName == "BGM1") //�� �߰��� ������ ���ǹ� �߰� �ʿ�
        {
            return bgm1;
        }
        else if (bgmName == "Offset")
        {
            return offset;
        }
        else if (bgmName == "DiscoHeart")
        {
            return discoHeart;
        }
        else
        {
            return -1;
        }
    }

    private void MakeJson_BGM1()
    {
        bgmJsonFiles[bgm1].bgmName = "BGM1";
        bgmJsonFiles[bgm1].bpm = 128d;
        bgmJsonFiles[bgm1].delay = 0f;
        bgmJsonFiles[bgm1].time = 10f;
        bgmJsonFiles[bgm1].beatList = new List<double>() { 0, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60,
                                                           60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60,
                                                           60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60,
                                                           60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60,
                                                           60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60,
                                                           60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60,
                                                           60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60,
                                                           60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60};
        bgmJsonFiles[bgm1].beatAfterTimeOver = 60;
        bgmJsonFiles[bgm1].bgmNameAfterTimeOver = "BGM1";
        string jsondata = JsonUtility.ToJson(bgmJsonFiles[bgm1]);
        bgmJsonFiles[bgm1] = JsonUtility.FromJson<BGMJsonFile>(jsondata);
    }

    private void MakeJson_Offset()
    {
        bgmJsonFiles[offset].bgmName = "Offset";
        bgmJsonFiles[offset].bpm = 128d;
        bgmJsonFiles[offset].delay = 0f;
        bgmJsonFiles[offset].time = 99999999f;
        bgmJsonFiles[offset].beatList = new List<double>() { 0, 60, 60, 60, 60, 60, 60, 60, 60, 60};
        bgmJsonFiles[offset].beatAfterTimeOver = 60;
        bgmJsonFiles[offset].bgmNameAfterTimeOver = "Offset";
        string jsondata = JsonUtility.ToJson(bgmJsonFiles[offset]);
        bgmJsonFiles[offset] = JsonUtility.FromJson<BGMJsonFile>(jsondata);
    }

    private void MakeJson_DiscoHeart()
    {
        bgmJsonFiles[discoHeart].bgmName = "DiscoHeart";
        bgmJsonFiles[discoHeart].bpm = 129d;
        bgmJsonFiles[discoHeart].delay = 0f;
        bgmJsonFiles[discoHeart].time = 150f;
        bgmJsonFiles[discoHeart].beatList = new List<double>() { 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, //27
                                                                 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120,
                                                                 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120,
                                                                 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120,
                                                                 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120,
                                                                 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120 }; //162
        //bgmJsonFiles[discoHeart].beatList = new List<double>() { 120, 120, 120, 120 };
        bgmJsonFiles[discoHeart].beatAfterTimeOver = 60;
        bgmJsonFiles[discoHeart].bgmNameAfterTimeOver = "BGM1";
        string jsondata = JsonUtility.ToJson(bgmJsonFiles[discoHeart]);
        bgmJsonFiles[discoHeart] = JsonUtility.FromJson<BGMJsonFile>(jsondata);
    }
}
