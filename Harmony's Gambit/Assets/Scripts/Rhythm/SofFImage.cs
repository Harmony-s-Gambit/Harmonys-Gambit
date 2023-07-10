using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SofFImage : MonoBehaviour
{
    private Image image;
    private float fadeSpeed;
    private float elapsedTime;

    void Start()
    {
        elapsedTime = 0f;
        fadeSpeed = 5f;
        image = GetComponent<Image>();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        float alpha = 1 - elapsedTime * fadeSpeed;
        Color newColor = image.color;
        newColor.a = alpha;
        image.color = newColor;

        if (alpha <= 0)
        {
            Destroy(gameObject);
        }
    }
}
