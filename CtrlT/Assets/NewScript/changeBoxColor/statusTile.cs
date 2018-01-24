using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statusTile : MonoBehaviour {
    //HERO
    public GameObject hero_GameObject;
    private heroScript Hero_Script;

    public GameObject Main_GameObject;
    private Main main_Script;

    public bool canWalk { get; set; }

    // Use this for initialization
    void Start () {
        Hero_Script = hero_GameObject.GetComponent<heroScript>();
        Main_GameObject = GameObject.Find("Main");
        main_Script = Main_GameObject.GetComponent<Main>();
    }
	
	// Update is called once per frame
	void Update () {
       /* if (canWalk)
        {
            Debug.Log("isWalk");
        }*/
	}

    void OnMouseDown()
    {
        if (!main_Script._isReverse)
        {
            if (canWalk)
            {
                //print("canWalk: " + canWalk);
                Hero_Script.checkDirection(this.gameObject);
                //print("after");
                //return tile that canWalk = true
            }
        }
        else
        {
            print("can't click");
            
        }
    }  
}
