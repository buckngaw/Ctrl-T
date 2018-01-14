using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class clickLevel : MonoBehaviour {

    public string scenceName;
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print(scenceName);
            SceneManager.LoadScene(scenceName);
        }

    }
}
