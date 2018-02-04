using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour {

    //Main
    public GameObject Main_GameObject;
    private Main Main_Script;

    public List<int> savedEnemyTurn { get; set; }

    public GameObject freezeGameObject;
    public ParticleSystem pause;
    public bool isObstacle;
    public bool isTileMove;
    public Vector3[] positions;
    public Vector3 collectPosition;


    public bool isFreeze { get; set; }
    public int _enemyTurn { get; set; }

    private float _movespeed = 4.0f;
    public bool onTriggle { get; set; }
    public bool _isReversing { get; set; }
    public bool _isReversingAndFreezing { get; set; }
    public bool _isChangePosY { get; set; }
    private Vector3 tempPosition;
    private bool isJump;

    // Use this for initialization
    void Start () {
        savedEnemyTurn = new List<int>();
        Main_Script = Main_GameObject.GetComponent<Main>();
        isJump = false;
        pause.Stop();
        //onTriggle = false;
        //savedEnemyTurn.Add(0);
    }
	
	// Update is called once per frame
	void Update () {
        //_isReversing = Main_Script.isReversing;
        if (_isReversing)
        {
            int index = _enemyTurn % positions.Length;
            //Set enemy position y = 1.2
            tempPosition.x = positions[index].x;
            tempPosition.z = positions[index].z;
            if (!isObstacle)
            {
                tempPosition.y = 8.0f;
            }
            else
            {
                tempPosition.y = 1.5f;
            }
            transform.position = tempPosition;
            if (_isChangePosY)
            {
                transform.position = positions[index];
            }
           // print("transform enemy: " + transform.position);
            _isReversing = false;
        }
        else if (Main_Script._isfreeze)
         {
            //Animation Freeze
            freezeGameObject.gameObject.SetActive(false);
            print("choose to freeze");
         }
        else
        {
            int index = _enemyTurn % positions.Length;
            if (!isObstacle && !isTileMove)
            {
                if (index != 0)
                {
                    if (positions[index].z == positions[index - 1].z && isJump == true)
                    {
                        if (positions[index].x < positions[index - 1].x)
                        {
                            this.transform.eulerAngles = new Vector3(0, 0, 0);
                        }
                        else
                        {
                            this.transform.eulerAngles = new Vector3(0, -180, 0);
                        }
                        this.transform.GetChild(1).GetComponent<Animator>().Play("jump");
                        isJump = false;
                        //print("jump");
                    }
                    else if (positions[index].x == positions[index - 1].x && isJump == true)
                    {
                        if (positions[index].z > positions[index - 1].z)
                        {
                            this.transform.eulerAngles = new Vector3(0, 0, 0);
                        }
                        else if (positions[index].z < positions[index - 1].z)
                        {
                            this.transform.eulerAngles = new Vector3(0, 0, 0);
                        }
                        this.transform.GetChild(1).GetComponent<Animator>().Play("jump");
                        isJump = false;
                        //print("jump");
                    }
                }
            }          
            transform.position = Vector3.MoveTowards(transform.position, positions[index], _movespeed * Time.deltaTime);
            if (isTileMove && this.transform.position.y != -0.5)
            {
                this.transform.GetChild(0).GetComponent<Renderer>().enabled = true;
                this.transform.GetChild(0).GetComponent<Collider>().enabled = true;
            }
            if (isTileMove && this.transform.position.y == -0.5)
            {
                this.transform.GetChild(0).GetComponent<Renderer>().enabled = false;
                this.transform.GetChild(0).GetComponent<Collider>().enabled = false;
            }
        }

        if (Main_Script.ChooseFeature[3])
        {
            if(transform.position == collectPosition)
            {
                onTriggle = true;
                print("true");
            }
            else
            {
                onTriggle = false;
                print("false");
            }
            /*if (onTriggle)
            {
                Behaviour Halo = (Behaviour)gameObject.GetComponent("Halo");
                Halo.enabled = true;
            }
            else
            {
                Behaviour Halo = (Behaviour)gameObject.GetComponent("Halo");
                Halo.enabled = false;
            }*/
        }

    }

    public void EnemyOnMove()
    {
        _enemyTurn++; 
        isJump = true;
        //print("enemy: " + _enemyTurn);
        savedEnemyTurn.Add(_enemyTurn);
        //turnManager_Script.savedTurn.Add(turnManager_Script.enviTurn);
    }

    private void OnMouseDown()
    {
        if (Main_Script._isfreeze)
        {
            print("freezed");
            //ANIMATION
            pause.Play();
            isFreeze = true;
            Behaviour Halo = (Behaviour)gameObject.GetComponent("Halo");
            freezeGameObject.gameObject.SetActive(true);
            Halo.enabled = true;
            Main_Script.actionPoint--;
            Main_Script._isfreeze = false;
        } 
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "collectCystal")
        {
           // print("cystal");
            if (Main_Script.ChooseFeature[3] == true)
            {
                onTriggle = true;
                Behaviour Halo = (Behaviour)gameObject.GetComponent("Halo");
                Halo.enabled = true;
            }
        }
        
    }*/
}
