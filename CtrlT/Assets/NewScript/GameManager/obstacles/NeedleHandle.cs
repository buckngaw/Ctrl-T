using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleHandle : MonoBehaviour {

    //Main
    public GameObject Main_GameObject;
    private Main Main_Script;

    public List<int> savedNeeddleTurn { get; set; }
    public int _needleTurn { get; set; }
    public bool _isReversing { get; set; }

    public int triggleTurn;

    private bool isMove;

    // Use this for initialization
    void Start () {
        savedNeeddleTurn = new List<int>();
        Main_Script = Main_GameObject.GetComponent<Main>();
        isMove = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (isMove)
        {
           
        }
	}

    public void NeedleOnMove()
    {
        _needleTurn++;
        //isJump = true;   play animation
        //print("enemy: " + _enemyTurn);
        savedNeeddleTurn.Add(_needleTurn);
        isMove = true;
        //turnManager_Script.savedTurn.Add(turnManager_Script.enviTurn);
    }
}
