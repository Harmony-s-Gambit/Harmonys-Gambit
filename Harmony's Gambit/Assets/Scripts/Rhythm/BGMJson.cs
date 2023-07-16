using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMJsonFile
{
    public string bgmName; //곡 이름
    public double bpm; //곡 bpm
    public double delay; //곡 delay, 노트가 판정 범위 가운데를 지나고 몇 초 후에 곡을 재생 시킬 것인가
    public List<double> beatList; //박자들
}

public class BGMJson : MonoBehaviour
{
    public static BGMJson instance;
    public BGMJsonFile[] bgmJsonFiles = new BGMJsonFile[1]; //곡 추가할 때마다 인덱스 증가 필요

    private const int bgm1 = 0; //곡 추가할 때마다 상수 추가 필요

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
        MakeJson_BGM1(); //곡 추가할 때마다 함수 추가 필요
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
