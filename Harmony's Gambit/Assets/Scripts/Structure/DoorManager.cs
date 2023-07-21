using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class DoorManager : MonoBehaviour
{
    GameObject button1, button2;
    GameObject slot;
    DoorOpenButton dob1, dob2;

    // Start is called before the first frame update
    void Start()
    {
        dob1 = button1.GetComponent<DoorOpenButton>();
        dob2 = button2.GetComponent<DoorOpenButton>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dob1.isPressed && dob2.isPressed )
        {
            Open();
        }
    }
    
    void Open()
    {
        // slot blockable  «ÿ¡¶
    }
}
