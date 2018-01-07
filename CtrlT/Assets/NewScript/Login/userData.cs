using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class userData : MonoBehaviour {

    public Text _username;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        _username.text = "user: " + globalUser.thisUser.username;
    }
}
