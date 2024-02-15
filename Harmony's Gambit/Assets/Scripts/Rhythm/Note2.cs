using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note2 : MonoBehaviour
{
    public float _noteSpeed;
    private Image noteImage;

    private float elapsedTime = 0;
    [SerializeField] private float fadeDuration;
    private Color startColor;
    private Color color;
    private Image imageToFade;

    private void Start()
    {
        GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        noteImage = GetComponent<Image>();
        imageToFade = GetComponent<Image>();
        startColor = imageToFade.color;
        color = imageToFade.color;
    }

    private void OnEnable()
    {
        if (noteImage == null)
        {
            noteImage = GetComponent<Image>();
        }
        noteImage.enabled = true;

        elapsedTime = 0;
        if (color.a <= 1f)
        {
            color.a = 1f;

            try
            {
                imageToFade.color = color;
            }
            catch (System.Exception) { }
        }
    }

    void Update()
    {
        transform.localPosition += Vector3.right * _noteSpeed * Time.deltaTime;
    }

    public void HideNote()
    {
        noteImage.enabled = false;
    }

    public IEnumerator FadeOutImage()
    {
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alphaValue = Mathf.Lerp(startColor.a, 0f, elapsedTime / fadeDuration);
            imageToFade.color = new Color(startColor.r, startColor.g, startColor.b, alphaValue);
            yield return null;
        }
    }

    public void SetNoteSpeed(int _speed) //±âº» 500
    {
        _noteSpeed = _speed;
    }
}