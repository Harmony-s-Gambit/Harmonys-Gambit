using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMJsonFile
{
    public string bgmName; //�� �̸�
    public double bpm; //�� bpm
    public double delay; //�� delay, ��Ʈ�� ���� ���� ����� ������ �� �� �Ŀ� ���� ��� ��ų ���ΰ�
    public List<double> beatList; //���ڵ�
}

public class BGMJson : MonoBehaviour
{
    public static BGMJson instance;
    public BGMJsonFile[] bgmJsonFiles = new BGMJsonFile[1]; //�� �߰��� ������ �ε��� ���� �ʿ�

    private const int bgm1 = 0; //�� �߰��� ������ ��� �߰� �ʿ�

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
    }

    public int CurrentBGMindex(string bgmName)
    {
        if (bgmName == "BGM1")
        {
            return bgm1;
        }
        else
        {
            return -1;
        }
    }

    private void MakeJson_BGM1()
    {
        bgmJsonFiles[bgm1].bgmName = "BGM1";
        bgmJsonFiles[bgm1].bpm = 128;
        bgmJsonFiles[bgm1].delay = 0;
        bgmJsonFiles[bgm1].beatList = new List<double>() { 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60,
                                             60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60,
                                             60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60,
                                             60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60,
                                             60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60,
                                             60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60, 60};
        string jsondata = JsonUtility.ToJson(bgmJsonFiles[bgm1]);
        bgmJsonFiles[bgm1] = JsonUtility.FromJson<BGMJsonFile>(jsondata);
    }
}
