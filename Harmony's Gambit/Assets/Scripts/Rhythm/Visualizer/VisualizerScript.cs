using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualizerScript : MonoBehaviour
{
    public static VisualizerScript instance;
    private GameManager _gameManager;

    public float minHeight = 15f;
    public float maxHeight = 200f;
    public float updateSenstivity = 0.5f;
    public Color visualizerColor = Color.gray;
    [Space(15)]
    //public AudioClip audioClip;
    public bool loop = true;
    [Space(15), Range(64, 8192)]
    public int visualizerSimples = 64;

    VisualizerObjectScript[] visualizerObjects;
    [SerializeField] AudioSource m_audioSource;

    private float coefficient = 10f;

    void Start()
    {
        instance = this;
        _gameManager = FindObjectOfType<GameManager>();

        visualizerObjects = GetComponentsInChildren<VisualizerObjectScript>();

        //if (!audioClip)
        //{
        //    return;
        //}

        //m_audioSource = new GameObject("AudioSource").AddComponent<AudioSource>();
        //m_audioSource.loop = loop;
        //m_audioSource.clip = audioClip;
        //m_audioSource.volume = 0.1f;
        //m_audioSource.Play();

        coefficient = 0.0015f * (m_audioSource.volume * 100 - 100) * (m_audioSource.volume * 100 - 100) + 1;
    }

    void Update()
    {
        coefficient = 0.0015f * (m_audioSource.volume * 100 - 100) * (m_audioSource.volume * 100 - 100) + 1;

        if (_gameManager.isGameStart)
        {
            float[] spectrumData = m_audioSource.GetSpectrumData(visualizerSimples, 0, FFTWindow.Rectangular);

            for (int i = 0; i < visualizerObjects.Length; i++)
            {
                Vector2 newSize = visualizerObjects[i].GetComponent<RectTransform>().rect.size;

                newSize.y = Mathf.Clamp(Mathf.Lerp(newSize.y, minHeight + (spectrumData[i] * (maxHeight - minHeight) * 0.5f) * (i + 1) * coefficient, updateSenstivity), minHeight, maxHeight);
                //newSize.y = Mathf.Clamp(Mathf.Lerp(newSize.y, minHeight + (spectrumData[i] * (maxHeight - minHeight) * 0.5f) * 100, updateSenstivity), minHeight, maxHeight);
                visualizerObjects[i].GetComponent<RectTransform>().sizeDelta = newSize;

                //visualizerObjects[i].GetComponent<Image>().color = visualizerColor;
            }
        }
    }
}
