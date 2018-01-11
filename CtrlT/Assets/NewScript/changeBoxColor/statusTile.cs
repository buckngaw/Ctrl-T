using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statusTile : MonoBehaviour {
    //Main
    public GameObject hero_GameObject;
    private heroScript Hero_Script;

    public bool canWalk;

	// Use this for initialization
	void Start () {
        Hero_Script = hero_GameObject.GetComponent<heroScript>();
    }
	
	// Update is called once per frame
	void Update () {
        if (canWalk)
        {
            Debug.Log("isWalk");
        }
	}

    void OnMouseDown()
    {
        if (canWalk)
        {
            print("canWalk: " + canWalk);
            Hero_Script.checkDirection(this.gameObject);
            print("after");
            //return tile that canWalk = true
        }
        //Application.LoadLevel("SomeLevel");
       
    }
}
