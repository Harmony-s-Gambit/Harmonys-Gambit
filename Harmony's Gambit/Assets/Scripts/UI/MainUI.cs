using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    [SerializeField] public GameObject[] panels;
    [SerializeField] GameObject[] buttons;
    private GameManager _gameManager;
    private SightManager _sightManager;
    private CameraMoving _cameraMoving;
    private GridMaker _gridMaker;
    private ItemManager _itemManager;
    private StructureManager _structureManager;
    private PlayerManager _playerManager;
    private EnemyManager _enemyManager;
    private TimingManager _timingManager;
    private MissArea _missArea;

    [SerializeField] Image progressBar;
    [SerializeField] Text progressText;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }
        panels[0].SetActive(true);
    }

    public void Main_MapSelectButton()
    {
        panels[0].SetActive(false);
        panels[2].SetActive(true);
    }

    public void Main_OffsetSettingButton()
    {
        panels[0].SetActive(false);
        panels[1].SetActive(true);
    }

    public void OffsetSetting_MainButton()
    {
        panels[1].SetActive(false);
        panels[0].SetActive(true);
    }

    public void MapSelect_MainButton()
    {
        panels[2].SetActive(false);
        panels[0].SetActive(true);
    }

    public void Main_CreditButton()
    {
        //panels[0].SetActive(false);
        panels[4].SetActive(true);
    }

    public void Credit_MainButton()
    {
        panels[4].SetActive(false);
        //panels[0].SetActive(true);
    }

    public void Main_TestSettingButton()
    {
        //panels[0].SetActive(false);
        panels[6].SetActive(true);
    }

    public void TestSetting_MainButton()
    {
        panels[6].SetActive(false);
        //panels[0].SetActive(true);
    }

    public void GamePlay1Button() //���� �÷��� �� ����, ��Ʈ ���� ����, �� ���� ���� ��ư
    {
        AudioManager.instance.PlaySFX("Start");
        panels[0].SetActive(false);
        panels[3].SetActive(true);

        StageInfo.instance.SetStageName(GetNextStageName("Stage1_1"));
        SceneManager.LoadScene("Stage");
        StartCoroutine(MapDelay());
    }

    public void TestPlayButton() //���� �������� �ٷΰ��� ��ư
    {
        panels[6].SetActive(false);
        AudioManager.instance.PlaySFX("Start");
        panels[0].SetActive(false);
        panels[3].SetActive(true);

        StageInfo.instance.SetStageName("BossStage1");
        SceneManager.LoadScene("Stage");
        StartCoroutine(MapDelay());
    }

    public void NextStage(string _nextStage) //
    {
        AudioManager.instance.PlaySFX("Start");
        panels[0].SetActive(false);
        panels[3].SetActive(true);

        SceneManager.LoadScene("Stage");
        StartCoroutine(MapDelay());
    }

    IEnumerator MapDelay()
    {
        float timer = 0f;
        
        while (progressBar.fillAmount < 1f)
        {
            yield return null;
            timer += Time.unscaledDeltaTime;
            progressBar.fillAmount = Mathf.Lerp(0f, 1f, timer);
            
            if (timer < 0.99f)
            {
                progressText.text = (Mathf.Round(timer * 100)).ToString() + "%";
            }
            else
            {
                progressText.text = "99%";
            }
        }

        yield return null;
        Instantiate(Resources.Load("Map/" + StageInfo.instance.GetStageName()));

        _gameManager = FindObjectOfType<GameManager>();
        _sightManager = FindObjectOfType<SightManager>();
        _cameraMoving = FindObjectOfType<CameraMoving>();
        _gridMaker = FindObjectOfType<GridMaker>();
        _timingManager = FindObjectOfType<TimingManager>();
        _missArea = FindObjectOfType<MissArea>();

        yield return null;

        _gridMaker.MakeGrid();

        yield return null;

        _itemManager = FindObjectOfType<ItemManager>();
        _structureManager = FindObjectOfType<StructureManager>();
        _playerManager = FindObjectOfType<PlayerManager>();
        _enemyManager = FindObjectOfType<EnemyManager>();

        _itemManager.MakeItem();
        _structureManager.MakeStructure();
        _playerManager.MakePlayer();
        _enemyManager.MakeEnemy();
        
        _timingManager.SetStart();
        _missArea.SetStart();

        yield return null;

        progressBar.fillAmount = 1f;
        progressText.text = "100%";

        yield return new WaitForSeconds(0.1f);

        panels[3].SetActive(false); 

        try
        {
            GameObject.Find("LoadingImage").SetActive(false);
        }
        catch (System.Exception) { } //�ε� ȭ�� ����


        //�̺κ�

        //���� ����

        StartCoroutine(GameStartSetting());
        _gameManager.SetStart();
        //panels[2].SetActive(false);
        panels[5].SetActive(true); //���������� �ѱ�

        GridSlotInfo[] slots = FindObjectsOfType<GridSlotInfo>();
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].UpdateSightType();
        }

        StartCoroutine(TestDelay());
        _cameraMoving.FirstCam();
    }

    IEnumerator TestDelay()
    {
        yield return null;
        _sightManager.rhythm = true;
    }

    private string GetNextStageName(string _stage)
    {
        if (_stage == "Stage1_1")
        {
            return "Stage1_1_" + Random.Range(1, 4).ToString(); //�������� �߰� �� ���� �� ���� �ʿ�
        }
        else
        {
            return "";
        }
    }

    IEnumerator GameStartSetting()
    {
        _gameManager.isGameStart = true;
        _gameManager.isRedPlayerPlaying = true;
        _gameManager.isBluePlayerPlaying = true;
        _gameManager.whichDoorHasRedPlayer = -1;
        _gameManager.whichDoorHasBluePlayer = -1;
        _playerManager.GameClear = false;
        _playerManager.GameOver = false;

        yield return new WaitForSeconds(1f);
        NoteManager.instance.SetBGMValue("DiscoHeart");
        ScoreManager.instance.GameStartSetting();
    }

    public void ControllButton(int num)
    {
        switch (num)
        {
            case 0:
                buttons[0].SetActive(false); //������ ���� ��ư ����
                break;
            case 1:
                buttons[0].SetActive(true);
                break;
            case 2:
                buttons[1].SetActive(false); //������-���� ��ư ����
                break;
            case 3:
                buttons[1].SetActive(true);
                break;
        }
    }
}
