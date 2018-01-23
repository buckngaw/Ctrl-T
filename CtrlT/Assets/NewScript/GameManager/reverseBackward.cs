using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reverseBackward : MonoBehaviour {

    //Main
    public GameObject Main_GameObject;
    private Main Main_Script;

    //buttonManager
    public GameObject buttonManager_GameObject;
    private buttonManager buttonManager_Script;

    public bool isBackward { get; set; }
    public bool isForward { get; set; }

    public void Start()
    {
        Main_Script = Main_GameObject.GetComponent<Main>(); //MAIN
        buttonManager_Script = buttonManager_GameObject.GetComponent<buttonManager>();
    }

    public void Update()
    {
        if (isBackward)
        {
            buttonManager_Script.isBackward = true;
            isBackward = false;
        }
        else if(isForward)
        {
            buttonManager_Script.isForkward = true;
            isForward = false;
        }
    }

    public void Backward() {
        
        isBackward = true;
    }
    public void Forward()
    {
        isForward = true;
    }
}
