using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class DoorManager : MonoBehaviour
{
    GameObject button1, button2;

    // Start is called before the first frame update
    void Start()
    {
        button1 = (GameObject)Instantiate(Resources.Load("Prefabs/Structure/DoorOpenButton"));
        button2 = (GameObject)Instantiate(Resources.Load("Prefabs/Structure/DoorOpenButton"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
