using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewValue : MonoBehaviour
{
    GameManager _gameManager;
    float time = 0;

    [SerializeField] private Text[] texts;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (time < 0.1f)
        {
            time += Time.deltaTime;
        }
        else
        {
            texts[0].text = _gameManager.rhythm.ToString();
            texts[1].text = _gameManager.isRedValid.ToString();
            texts[2].text = _gameManager.isBlueValid.ToString();
            texts[3].text = _gameManager.isStunned.ToString();
        }
    }
}
