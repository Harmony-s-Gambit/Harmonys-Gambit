using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
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

    private bool sweeperOn = false;
    private bool spearOn = false;

    private SpriteRenderer w1SR;
    private SpriteRenderer w2SR;
    private SpriteRenderer w3SR;

    private Color beforeGet = new Color(0.7f, 0.7f, 0.7f, 0.2f);
    private Color afterGet = new Color(1f, 1f, 1f, 0.4f);
    private Color usingItem = new Color(1f, 1f, 1f, 1f);
    /*
    [SerializeField] private Color beforeGet;
    [SerializeField] private Color afterGet;
    [SerializeField] private Color usingItem;
    */

    private GameObject sweeper;
    private GameObject spear;
    private SpriteRenderer sweeperSR;
    private SpriteRenderer spearSR;

    private Weapon playerWeapon;
    private GameObject redPlayer;

    private GameObject bluePlayer;
    private Weapon collar;

    private bool hasInfo = false;
    private string mapInfo;

    private void OnEnable()
    {
        mapInfo = FindObjectOfType<StageInfo>().GetStageName();
        if ((mapInfo == "Stage1_1_1") || (mapInfo == "Stage1_1_2") || (mapInfo == "Stage1_1_3") || (mapInfo == "Stage1_Hard"))
        {
            PlayerPrefs.DeleteAll();
        }
        else if (mapInfo == "BossStage1")
        {
            if (PlayerPrefs.HasKey("hasSweeper")) // 우선순위 스위퍼
            {
                equipSweeper();
            }
            else if (PlayerPrefs.HasKey("hasSpear"))
            {
                equipSpear();
            }
            equipCollar();
        }

        weaponUI1 = GameObject.Find("sweeper_icon");
        weaponUI2 = GameObject.Find("spear_icon");
        weaponUI3 = GameObject.Find("collar_icon");

        w1SR = weaponUI1.GetComponent<SpriteRenderer>();
        w2SR = weaponUI2.GetComponent<SpriteRenderer>();
        w3SR = weaponUI3.GetComponent<SpriteRenderer>();

        w1SR.color = beforeGet;
        w2SR.color = beforeGet;
        w3SR.color = beforeGet;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            switchRedPlayerWeapon();
        }
    }

    public void switchRedPlayerWeapon()
    {
        if (PlayerPrefs.HasKey("hasSweeper") && PlayerPrefs.HasKey("hasSpear"))
        {
            if (spearOn)
            {
                equipSweeper();
                Debug.Log("spear>>sweeper");
            }
            else //if (sweeperOn)
            {
                equipSpear();
                Debug.Log("sweeper>>>spear");
            }
        }
    }

    private void getInfo()
    {
        //redPlayer = GameObject.FindGameObjectWithTag("Player");
        redPlayer = GameObject.Find("redPlayer(Clone)");

        sweeper = GameObject.Find("bone_5/sweeper");
        spear = GameObject.Find("bone_5/spear");
        sweeperSR = sweeper.GetComponent<SpriteRenderer>();
        spearSR = spear.GetComponent<SpriteRenderer>();

        weaponUI1 = GameObject.Find("sweeper_icon");
        weaponUI2 = GameObject.Find("spear_icon");
        w1SR = weaponUI1.GetComponent<SpriteRenderer>();
        w2SR = weaponUI2.GetComponent<SpriteRenderer>();

        hasInfo = true;
    }

    public void equipSweeper()
    {
        //Debug.Log("equipSweeper");
        if (!hasInfo)
        {
            getInfo();
        }

        w1SR.color = usingItem;
        if (PlayerPrefs.HasKey("hasSpear"))
        {
            w2SR.color = afterGet;
        }

        //Destroy(redPlayer.GetComponent<Fist>());
        //Destroy(redPlayer.GetComponent<Spear>());
        Destroy(redPlayer.GetComponent<Weapon>());

        redPlayer.AddComponent<Sweeper>();

        spearSR.enabled = false;
        sweeperSR.enabled = true;
        playerWeapon = redPlayer.GetComponent<Sweeper>();
        playerWeapon.playerWeapon = true;
        //playerWeapon.equiper = redPlayer.gameObject;

        playerWeapon.isSpear = false;
        playerWeapon.isSweeper = true;

        sweeperOn = true;
        spearOn = false;
    }
    public void equipSpear()
    {
        //Debug.Log("equipSpear");
        if (!hasInfo)
        {
            getInfo();
        }

        w2SR.color = usingItem;
        if (PlayerPrefs.HasKey("hasSweeper"))
        {
            w1SR.color = afterGet;
        }

        //Destroy(redPlayer.GetComponent<Fist>());
        //Destroy(redPlayer.GetComponent<Sweeper>());
        Destroy(redPlayer.GetComponent<Weapon>());

        redPlayer.AddComponent<Spear>();

        spearSR.enabled = true;
        sweeperSR.enabled = false;
        playerWeapon = redPlayer.GetComponent<Spear>();
        playerWeapon.playerWeapon = true;
        //playerWeapon.equiper = redPlayer.gameObject;

        playerWeapon.isSpear = true;
        playerWeapon.isSweeper = false;

        sweeperOn = false;
        spearOn = true;
    }

    public void equipCollar()
    {
        bluePlayer = GameObject.Find("bluePlayer(Clone)");
        collar = bluePlayer.GetComponent<Weapon>();
        collar.isCollar= true;

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
