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

        fullHP = this.gameObject.transform.parent.GetComponent<Mouse>().HP;
        healthBarFilled.fillAmount = 1f;
    }
    
    void Update()
    {
        UpdateHPBar();
    }

    public void UpdateHPBar() //맞았을 때 실행
    {
        healthBarBackground.SetActive(true);
        healthBarFilled.fillAmount = (float)this.gameObject.transform.parent.GetComponent<Mouse>().HP / fullHP;
    }
}
