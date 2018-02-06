using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class clickLevel : MonoBehaviour {

    public string scenceName;
    public int stateNumber;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if ( stateNumber <= int.Parse(firebaseServiceForLogin.user.save))
            {
                //print(scenceName);
                SceneManager.LoadScene(scenceName);
            }
            else
            {
                //Do nothing
            }
           
        }

    }
}
