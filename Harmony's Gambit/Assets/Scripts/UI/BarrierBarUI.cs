using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrierBarUI : MonoBehaviour
{
    private GameObject barrierOwner;
    private Enemy barrierEnemy;
    public int barrierCount = 3;
    public Sprite[] barrierColors = new Sprite[4];

    // Start is called before the first frame update
    void Start()
    {
        Stack<COLOR> tempStack = new Stack<COLOR>();
        barrierOwner = this.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject;
        barrierEnemy = barrierOwner.GetComponent<Enemy>();
        for (int i = barrierCount; i > 0; i--)
        {
            COLOR tempColor = barrierEnemy.barrier.Pop();
            tempStack.Push(tempColor);
            GameObject barrierUI = gameObject.transform.GetChild(i - 1).gameObject;
            if (tempColor == COLOR.RED)
            {
                barrierUI.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = barrierColors[2];
                barrierUI.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.red;
            }
            else
            {
                barrierUI.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = barrierColors[3];
                barrierUI.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.blue;
            }
        }
        for(int i = barrierCount; i > 0; i--)
        {
            barrierEnemy.barrier.Push(tempStack.Pop());
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(gameObject.transform.parent.transform.gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<Enemy>().direction == DIRECTION.RIGHT)
        {
            gameObject.transform.localScale = new Vector3(30, 30, 1);
        }else if(gameObject.transform.parent.transform.gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<Enemy>().direction == DIRECTION.LEFT)
        {
            gameObject.transform.localScale = new Vector3(-30, 30, 1);
        }

        if(barrierEnemy.barrier.Count != 0)
        {
            COLOR barrierColor = barrierEnemy.barrier.Peek();
            if(barrierColor == COLOR.RED)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = barrierColors[0]; 
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = barrierColors[1];
            }
            for(int i = barrierCount; i > barrierEnemy.barrier.Count; i--)
            {
                gameObject.transform.GetChild(i-1).gameObject.SetActive(false);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
