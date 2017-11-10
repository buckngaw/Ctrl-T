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
    public bool _isFreeze;
    private int _instanceID;
    
    // Use this for initialization
    void Start () {
        //get script turnManager
        turnManager_Script = turnManager_GameObject.GetComponent<turnManager>();
        //get script heroController
        heroController_Script = heroController_GameObject.GetComponent<heroController>();
        _isFreeze = false;
    }
	
	// Update is called once per frame
	void Update () {
        _enviTurn = turnManager_Script.enviTurn;
        //reverse
        if (heroController_Script.isReversing)
        {
            // LIKE MOVEMENT(); BUT ENEMY WILL INSTANT WARP.
            if (!_isFreeze)
            {
                int index = _enviTurn % positions.Length;
                transform.position = positions[index];
                heroController_Script.isReversing = false;
            }else if (_isFreeze)
            {
                print("freeze");
                heroController_Script.isReversing = false;
            }
            

        }  //changeState
        else if (heroController_Script._isChangeState)
        {
            //_isFreeze = false; // set all mon not freeze (reset)
            GameObject[] mons = GameObject.FindGameObjectsWithTag("Enemy");
            for(int i = 0; i < mons.Length; i++)
            {
                Behaviour Halo = (Behaviour)mons[i].GetComponent("Halo");
                Halo.enabled = true;
            }
            // if choose mon to freeze
            if(heroController_Script._isclick == true)
            {
                for (int i = 0; i < mons.Length; i++)
                {
                    Behaviour Halo = (Behaviour)mons[i].GetComponent("Halo");
                    Halo.enabled = false;
                }
                heroController_Script._isChangeState = false;
                heroController_Script._isclick = false;
            }

            
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
            //_instanceID = GetInstanceID();
            //heroController_Script._freezeMon = GetInstanceID();
            _isFreeze = true;
            turnManager_Script.actionPoint--;
            heroController_Script._isclick = true;
            print(heroController_Script._isclick);
            print(gameObject.name + ": _isFreeze = " + _isFreeze);
        }
       
    }

    void movement()
    {
        int index = _enviTurn % positions.Length;
        transform.position = Vector3.MoveTowards(transform.position, positions[index],_movespeed*Time.deltaTime);

    }

    
}
