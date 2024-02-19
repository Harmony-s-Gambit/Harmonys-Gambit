using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUIController : MonoBehaviour
{
    /*
    [SerializeField] private GameObject weaponUI1; // sweeper
    [SerializeField] private GameObject weaponUI2; // spear
    [SerializeField] private GameObject weaponUI3; // collar
    */
    private GameObject weaponUI1; // sweeper
    private GameObject weaponUI2; // spear
    private GameObject weaponUI3; // collar

    public bool sweeperOn = false;
    public bool spearOn = false;

    private SpriteRenderer w1SR;
    private SpriteRenderer w2SR;
    private SpriteRenderer w3SR;

    private Color beforeGet = new Color(0.7f, 0.7f, 0.7f, 0.2f);
    private Color afterGet = new Color(1f, 1f, 1f, 0.7f);
    private Color usingItem = new Color(1f, 1f, 1f, 1f);
    /*
    [SerializeField] private Color beforeGet;
    [SerializeField] private Color afterGet;
    [SerializeField] private Color usingItem;
    */
    private void Start()
    {
        weaponUI1 = GameObject.Find("sweeper_icon");
        weaponUI2 = GameObject.Find("spear_icon");
        weaponUI3 = GameObject.Find("collar_icon");

        w1SR = weaponUI1.GetComponent<SpriteRenderer>();
        w2SR = weaponUI2.GetComponent<SpriteRenderer>();
        w3SR = weaponUI3.GetComponent<SpriteRenderer>();

        w1SR.color = beforeGet;
        w2SR.color = beforeGet;
        w3SR.color = beforeGet;

        // stageInfo = FindObjectOfType<StageInfo>().GetStageName();
        // BossStage1

    }

    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (PlayerPrefs.HasKey("hasSweeper"))
            {
            equipSweeper();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (PlayerPrefs.HasKey("hasSpear"))
            {
                equipSpear();
            }
        }
        */
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            switchRedPlayerWeapon();
        }
    }

    public void switchRedPlayerWeapon()
    {
        if (true)
        {

        }
    }



    public void equipSweeper()
    {
        // skip null test
        weaponUI1 = GameObject.Find("sweeper_icon");
        weaponUI2 = GameObject.Find("spear_icon");
        w1SR = weaponUI1.GetComponent<SpriteRenderer>();
        w2SR = weaponUI2.GetComponent<SpriteRenderer>();

        w1SR.color = usingItem;
        if (PlayerPrefs.HasKey("hasSpear"))
        {
            w2SR.color = afterGet;
        }
    }
    public void equipSpear()
    {
        // skip null test
        weaponUI1 = GameObject.Find("sweeper_icon");
        weaponUI2 = GameObject.Find("spear_icon");
        w1SR = weaponUI1.GetComponent<SpriteRenderer>();
        w2SR = weaponUI2.GetComponent<SpriteRenderer>();

        w2SR.color = usingItem;
        if (PlayerPrefs.HasKey("hasSweeper"))
        {
            w1SR.color = afterGet;
        }
    }

    public void equipCollar()
    {
        if (weaponUI3 == null)
        {
            weaponUI3 = GameObject.Find("collar_icon");
            w3SR = weaponUI3.GetComponent<SpriteRenderer>();
            w3SR.color = usingItem;
        }
        else
        {
        w3SR.color = usingItem;
        }
    }

    public void EnterBoss()
    {
        equipSweeper();
        if (PlayerPrefs.HasKey("hasCollar"))
        {
            w3SR.color = afterGet;
        }
    }

    /*
    private void OnEnable()
    {
        Item.PlayerPrefsValueChanged += HandlePlayerPrefsValueChanged;
    }

    private void OnDisable()
    {
        Item.PlayerPrefsValueChanged -= HandlePlayerPrefsValueChanged;
    }

    private void HandlePlayerPrefsValueChanged(string key)
    {
        // PlayerPrefs 값이 변경될 때 호출될 메소드
        if (key == "hasSweeper")
        {
            w1SR.color = afterGet;
        }
        else if (key == "hasSpear")
        {
            w2SR.color = afterGet;
        }
        else if (key == "hasCollar")
        {
            w3SR.color = afterGet;
        }
        else
        {

        }
        
    }
    */
}
