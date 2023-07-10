using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BGMTextReader : MonoBehaviour
{
    public static BGMTextReader instance;

    private void Start()
    {
        BGMTextRead("BGM1");
        instance = this;
    }

    public List<double> BGMTextRead(string bgmName)
    {
        List<double> bgmTextList = new List<double>();

        // 파일 경로 지정
        string filePath = "Assets/Resources/BGMNotes/" + bgmName + ".txt";

        // StreamReader 객체 생성
        StreamReader reader = new StreamReader(filePath);

        // 파일 내용을 문자열 변수에 저장
        string fileContent = reader.ReadToEnd();

        int infoNum = 0;

        for (int i = 0; i < fileContent.Length; i++) //fileContent.Length
        {
            if (infoNum < 10)
            {
                if (fileContent[i].ToString() == ":")
                {
                    bgmTextList.Add(InfoText(fileContent, i));
                    infoNum++;
                }
            }
            else
            {
                if (fileContent[i].ToString() == "," && fileContent[i + 1].ToString() != ";")
                {
                    bgmTextList.Add(IntervalText(fileContent, i));
                }
            }
        }

        // StreamReader 닫기
        reader.Close();
        //TestPrint(bgmTextList);
        return bgmTextList;
    }

    private double InfoText(string content, int index)
    {
        int num = 0;
        for (int i = 0; i < 10; i++)
        {
            if (content[index + i].ToString() != ",")
            {
                num++;
            }
            else
            {
                break;
            }
        }

        string info = "";
        for (int i = index + 1; i < index + num; i++)
        {
            info += content[i];
        }
        
        try
        {
            return double.Parse(info);
        }
        catch (System.Exception)
        {
            return 0;
        }
    }

    private double IntervalText(string content, int index)
    {
        int num = 0;
        for (int i = 0; i < 10; i++)
        {
            if (content[index + i + 2].ToString() != ",")
            {
                num++;
            }
            else
            {
                break;
            }
        }

        string info = "";
        for (int i = index + 2; i < index + 2 + num; i++)
        {
            info += content[i];
        }
        return double.Parse(info);
    }

    private void TestPrint(List<double> testList)
    {
        for (int i = 0; i < testList.Count; i++)
        {
            print("[" + i + "]" + testList[i]);
        }
    }
}
