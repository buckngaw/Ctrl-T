using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileMove : MonoBehaviour {


    //Main
    public GameObject Main_GameObject;
    private Main Main_Script;

    public int moveTurn;
    public GameObject freezeGameObject;
    public ParticleSystem pause;
    public Vector3[] positions;
    public Vector3 collectPosition;

    public bool isFreeze { get; set; }
    public int _enemyTurn { get; set; }

    // Use this for initialization
    void Start () {
        Main_Script = Main_GameObject.GetComponent<Main>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
