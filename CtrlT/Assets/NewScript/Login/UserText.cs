using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserText : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if(firebaseServiceForLogin.user == null)
        {
            this.GetComponent<Text>().text = "Guest";
        }
        else
        {
            this.GetComponent<Text>().text = firebaseServiceForLogin.user.username;
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
