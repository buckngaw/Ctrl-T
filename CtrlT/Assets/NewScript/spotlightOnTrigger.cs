using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spotlightOnTrigger : MonoBehaviour {

    public GameObject spotlight;
    public GameObject Enemy_GameObject;
    private enemyScript Enemy_Script;

    public bool onTriggle { get; set; }

    private bool doUpdate;

    // Use this for initialization
    void Start () {
        Enemy_Script = Enemy_GameObject.GetComponent<enemyScript>();
    }
	
	// Update is called once per frame
	void Update () {
        print("Enemy_Script.onTriggle: " + Enemy_Script.onTriggle);
       // if (doUpdate)
        {
            if (Enemy_Script.onTriggle)
            {
                print("turn on");
                spotlight.SetActive(true);
            }
            else
            {
                spotlight.SetActive(false);
            }

        }
    }

   /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            doUpdate = true;
            print("cystal");
            if (Main_Script.ChooseFeature[3] == true)
            {
                spotlight.SetActive(true);
              
            }
        }

    }*/
}
