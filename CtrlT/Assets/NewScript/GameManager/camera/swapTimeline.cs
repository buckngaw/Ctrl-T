using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swapTimeline : MonoBehaviour {

    public GameObject panel_GameObject;
    private bool onClick;
    private bool temp;

    // Use this for initialization
    void Start () {
        onClick = false;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void swapPanel()
    {
        temp = !onClick;
        print(temp);
        if (temp)
        {
            panel_GameObject.SetActive(false);
            onClick = !onClick;
        }
        else
        {
            panel_GameObject.SetActive(true);
            onClick = !onClick;
        }

    }
}
