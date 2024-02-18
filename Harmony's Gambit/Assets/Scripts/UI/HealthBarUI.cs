using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    private GameObject healthBarBackground;
    private Image healthBarFilled;

    private int fullHP;

    void Start()
    {
        healthBarBackground = this.gameObject.transform.GetChild(0).gameObject;
        healthBarFilled = this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();

        fullHP = this.gameObject.transform.parent.GetComponent<Character>().HP;
        healthBarFilled.fillAmount = 1f;
    }
    
    void Update()
    {
        if (transform.parent.transform.localScale.x < 0)
        {
            Vector3 tempScale = healthBarFilled.GetComponent<RectTransform>().localScale;
            tempScale.x = -Mathf.Abs(tempScale.x);
            healthBarFilled.GetComponent<RectTransform>().localScale = tempScale;
        }
        else
        {
            Vector3 tempScale = healthBarFilled.GetComponent<RectTransform>().localScale;
            tempScale.x = Mathf.Abs(tempScale.x);
            healthBarFilled.GetComponent<RectTransform>().localScale = tempScale;
        }

        UpdateHPBar();
    }

    public void UpdateHPBar() //맞았을 때 실행
    {
        healthBarBackground.SetActive(true);
        healthBarFilled.fillAmount = (float)this.gameObject.transform.parent.GetComponent<Character>().HP / fullHP;
    }
}
