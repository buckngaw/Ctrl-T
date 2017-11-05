using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnPosition : MonoBehaviour {
    //turnManager
    public GameObject turnManager_GameObject;
    private turnManager turnManager_Script;
    //heroController
    public GameObject heroController_GameObject;
    private heroController heroController_Script;

    public Vector3[] positions;
    private float _movespeed = 4.0f;
    private int _enviTurn;
    
    // Use this for initialization
    void Start () {
        //get script turnManager
        turnManager_Script = turnManager_GameObject.GetComponent<turnManager>();
        //get script heroController
        heroController_Script = heroController_GameObject.GetComponent<heroController>();
    }
	
	// Update is called once per frame
	void Update () {
        _enviTurn = turnManager_Script.enviTurn;
        //reverse
        if (heroController_Script.isReversing)
        {
            // LIKE MOVEMENT(); BUT ENEMY WILL INSTANT WARP.
            int index = _enviTurn % positions.Length;
            transform.position = positions[index];
            heroController_Script.isReversing = false;

        }  //changeState
        else if (heroController_Script._isChangeState)
        {
            Behaviour Halo = (Behaviour)gameObject.GetComponent("Halo");
            Halo.enabled = true;
        }
        else
        {
            movement();
        }

        
       
	}

    private void OnMouseDown()
    {
        if (heroController_Script._isChangeState)
        {
            print("select");
            Behaviour Halo = (Behaviour)gameObject.GetComponent("Halo");
            Halo.enabled = false;
            heroController_Script._isChangeState = false;
        }
       
    }

    void movement()
    {
        int index = _enviTurn % positions.Length;
        transform.position = Vector3.MoveTowards(transform.position, positions[index],_movespeed*Time.deltaTime);

    }

    
}
