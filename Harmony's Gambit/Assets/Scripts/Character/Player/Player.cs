using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class Player : Character
{
    private GameManager _gameManager;
    private PlayerManager _playerManager;

    public bool takenDamage = false;
    private int beforeHP;

    public override void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _playerManager = FindObjectOfType<PlayerManager>();

        //_gameManager.players.Add(gameObject);

        m_Animator = GetComponent<Animator>();
        m_Animator.SetTrigger("idle");

        beforeHP = HP;
        
        weapon = gameObject.AddComponent<Fist>();
        weapon.Start();
        weapon.playerWeapon = true;
        weapon.equiper = gameObject;
    }

    protected override void Update()
    {
        if(HP <= 0)
        {
            _playerManager.GameOver = true;
            ScoreManager.instance.StageFailScore();
            m_Animator.SetTrigger("die");
            Invoke("AfterDie", 1f);
            if (gameObject.name.Substring(0, 9) == "redPlayer")
            {
                _gameManager.isRedPlayerPlaying = false;
                
            }
            if (gameObject.name.Substring(0, 10) == "bluePlayer")
            {
                _gameManager.isBluePlayerPlaying = false;
            }
        }
        else
        {
            if (direction == DIRECTION.LEFT)
            {
                Vector3 tempScale = transform.localScale;
                tempScale.x = -1;
                transform.localScale = tempScale;
            }
            else if (direction == DIRECTION.RIGHT)
            {
                Vector3 tempScale = transform.localScale;
                tempScale.x = 1;
                transform.localScale = tempScale;
            }
            if (HP < beforeHP)
            {
                m_Animator.Play("damage", -1, 0);
            }
            beforeHP = HP;
        }
    }
    private void AfterDie()
    {
        SceneManager.LoadScene("GameOver");
        Destroy(GameObject.Find("Managers"));
        Destroy(GameObject.Find("MainCanvas"));
    }

    public override void SetXY(int px, int py)
    {
        x = px; y = py;
        currentBlock = GameObject.Find(x + "_" + y);
        currentBlock.GetComponent<GridSlotInfo>().occupyingCharacter = gameObject;
        gameObject.transform.position = currentBlock.transform.position;
    }

    public override GameObject GetNextDest()
    {
        GameObject destBlock = currentBlock;
        switch (direction)
        {
            case DIRECTION.UP:
                destBlock = GameObject.Find(x + "_" + (y + 1));
                break;
            case DIRECTION.LEFT:
                destBlock = GameObject.Find((x - 1) + "_" + y);
                break;
            case DIRECTION.DOWN:
                destBlock = GameObject.Find(x + "_" + (y - 1));
                break;
            case DIRECTION.RIGHT:
                destBlock = GameObject.Find((x + 1) + "_" + y);
                break;
            case DIRECTION.STAY:
                break;
            default:
                destBlock = currentBlock;
                break;

        }
        return destBlock;
    }

    public override void Move(GameObject nextDest)
    {
        if (nextDest.GetComponent<GridSlotInfo>().y == currentBlock.GetComponent<GridSlotInfo>().y)
        {
            m_Animator.Play("move", -1, 0);
        }
        else if (nextDest.GetComponent<GridSlotInfo>().y - currentBlock.GetComponent<GridSlotInfo>().y == 1)
        {
            m_Animator.Play("move2", -1, 0);
        }
        else
        {
            m_Animator.Play("move3", -1, 0);
        }
        isMovedThisTurn = true;
        if (!((gameObject.name.Substring(0, 9) == "redPlayer" && !_gameManager.isRedPlayerPlaying) || (gameObject.name.Substring(0, 10) == "bluePlayer" && !_gameManager.isBluePlayerPlaying)))
        {
            currentBlock.GetComponent<GridSlotInfo>().occupyingCharacter = null;
            nextDest.GetComponent<GridSlotInfo>().occupyingCharacter = gameObject;
            currentBlock = nextDest;
            gameObject.transform.position = currentBlock.transform.position;
            switch (direction)
            {
                case DIRECTION.UP:
                    y = y + 1;
                    break;
                case DIRECTION.LEFT:
                    x = x - 1;
                    break;
                case DIRECTION.DOWN:
                    y = y - 1;
                    break;
                case DIRECTION.RIGHT:
                    x = x + 1;
                    break;
                case DIRECTION.STAY:
                    break;
                default:
                    break;
            }
        }
    }

    public override bool MoveManage()
    {
        if (isMovedThisTurn)
        {
            return false;
        }
        GameObject nextDest = GetNextDest();
        isMovedThisTurn = true;
        GameObject whosOnDest = nextDest.GetComponent<GridSlotInfo>().occupyingCharacter;
        if (nextDest.GetComponent<GridSlotInfo>().blockType == BLOCKTYPE.WALL)
        {
            return false;
        }
        else if (whosOnDest == null)
        {
            Move(nextDest);
            return true;
        }
        else
        {
            if (whosOnDest.GetComponent<Character>().MoveManage())
            {
                Move(nextDest);
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public void Crashed(GameObject nextDest, Vector3 ori)
    {
        Coroutine crash = StartCoroutine(Crash(nextDest, ori));

    }
    IEnumerator Crash(GameObject nextDest, Vector3 ori)
    {
        int N=2;
        float elapsed = 0f;
        while(elapsed < 1)
        {
            elapsed += Time.deltaTime*N; 
            transform.position = Vector3.Lerp(nextDest.transform.position, ori, elapsed);
            yield return null;
        }
    }

    public void HealthRocover()
    {
        if (HP < 4)
        {
            HP += 1;
        }
    }

    public override void changeTarget(COLOR c)
    {
       
    }
}
