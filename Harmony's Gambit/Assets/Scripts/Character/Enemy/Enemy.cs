using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public bool death = false;
    protected DIRECTION[] pattern;
    protected int _directionIdx;
    protected int beforeHP;
    protected int killScore; //처치 시 얻는 점수
    protected bool killScoreOnce = false;

    // Start is called before the first frame update
    public override void Start()
    {
        _directionIdx = 0;

        m_Animator = GetComponent<Animator>();

        m_Animator.SetTrigger("idle");

        beforeHP = HP;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (direction == DIRECTION.LEFT)
        {
            Vector3 tempScale = transform.localScale;
            tempScale.x = -Mathf.Abs(tempScale.x);
            transform.localScale = tempScale;
        }
        else if (direction == DIRECTION.RIGHT)
        {
            Vector3 tempScale = transform.localScale;
            tempScale.x = Mathf.Abs(tempScale.x);
            transform.localScale = tempScale;
        }
        if (HP < 1)
        {
            m_Animator.SetTrigger("die");

            Die();
        }
        if (HP < beforeHP)
        {
            m_Animator.SetTrigger("damage");
        }
        beforeHP = HP;
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
        m_Animator.SetTrigger("move");
        isMovedThisTurn = true;
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
        _directionIdx = (_directionIdx + 1) % pattern.Length;
        direction = pattern[_directionIdx];
        specialDirection();
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
                if(direction == DIRECTION.STAY)
                {
                    _directionIdx = (_directionIdx + 1) % pattern.Length;
                    direction = pattern[_directionIdx];
                    specialDirection();
                }
                return false;
            }
        }
    }
    // 요 밑의 3가지는 각각의 enemy 타입에서 overriding 하는 것을 추천
    public void Attack()
    {
        weapon.attackEnemies(1);
        _directionIdx = (_directionIdx + 1) % pattern.Length;
        direction = pattern[_directionIdx];
    }

    public bool isPlayerAtDest()
    {
        return false;
    }

    public void specialDirection()
    {

    }

    public void Die()
    {
        if (!killScoreOnce)
        {
            killScoreOnce = true;
            ScoreManager.instance.KillScore(killScore);
        }
        //Debug.Log("Die");
        GameObject.Find((x) + "_" + y).GetComponent<GridSlotInfo>().occupyingCharacter = null;
        GameObject.Find("GameManager").GetComponent<GameManager>().enemies.Remove(gameObject);
        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Die") && m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.98f)
        {
            Destroy(gameObject);
        }
    }


    public override void changeTarget(COLOR c)
    {
        
    }

    public void specialAttack() { }
}
