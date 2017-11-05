using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrefabButton : MonoBehaviour {

    public int turn { get; set; }
    public GameObject heroController_GameObject;
    private heroController heroController_Script;   //use heroController.cs

    // Use this for initialization
    void Start () {
        heroController_GameObject = GameObject.Find("Hero");
        heroController_Script = heroController_GameObject.GetComponent<heroController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void buttonOnClick()
    {
        heroController_Script.reverseTurn(turn, transform.GetComponent<Button>());
    }
}
