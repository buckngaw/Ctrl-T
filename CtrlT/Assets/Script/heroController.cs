using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class heroController : MonoBehaviour {

    //turnManager
    public GameObject turnManager_GameObject;
    private turnManager turnManager_Script;

    public Button typeHeroSkill;
    public GameObject tileCheckerR;
    public GameObject tileCheckerL;
    public GameObject tileCheckerF;
    public GameObject tileCheckerB;
    public Text _textAP;
    public RawImage endGameImageWin;
    public RawImage endGameImageLose;
    public Button restartGame;
    public bool isReversing { get; set; }
    public int _reverseTurn { get; set; }
    public string Warp;   //string that warp to other scence

    private float _movespeed = 4.0f;
    private int _heroTurn;
    private int _enviTurn;
    private List<int> _saveTurn;
    private float _winPointX;
    private float _winPointZ;
    private int _actionPoint;
    private bool _isEndGame;
    //private int _state;
    private Vector3 _targetPosition;
    private Vector3 _tileCheckerPosition;
    private bool _heroOnMove;
    private EndScript EndScript_script; // use EndScript.cs

    // Use this for initialization
    void Start () {
        //use variable in turnManagerScript
        turnManager_Script = turnManager_GameObject.GetComponent<turnManager>();
        //set active UI
        if(turnManager_Script.typeHeroSkill == 1) // normal mode
        {
            typeHeroSkill.gameObject.SetActive(false);
        }
        endGameImageWin.gameObject.SetActive(false);
        endGameImageLose.gameObject.SetActive(false);
        restartGame.gameObject.SetActive(false);

        _actionPoint = turnManager_Script.actionPoint ;
        _winPointX = turnManager_Script.winPoint.x;
        _winPointZ = turnManager_Script.winPoint.z;
        _isEndGame = turnManager_Script.isEndGame;

        _textAP.text = " " + _actionPoint;

        turnManager_Script.enviTurn = 0;
        _targetPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update () {

        _textAP.text = " " + _actionPoint;

        if (!_isEndGame)
        {
            GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile"); //tiles = object that has tag = "Tile"
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _movespeed * Time.deltaTime);

            if (_heroOnMove)
            {
                if (transform.position == _targetPosition)
                {
                    _heroOnMove = false;
                    turnManager_Script.savedTurn.Add(turnManager_Script.enviTurn);
                    turnManager_Script.enviTurn++;
                    turnManager_Script.heroTurn++;
                    _actionPoint--;
                    print("heroturn: " + turnManager_Script.heroTurn + " turn: " + turnManager_Script.enviTurn);
                }
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                //updateValue(tileCheckerR, tiles);
                if (checkTile(tileCheckerR, tiles))
                {
                    _tileCheckerPosition = new Vector3(1, 0, 0);
                    updateValue(_tileCheckerPosition);
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                //updateValue(tileCheckerL, tiles);
                if (checkTile(tileCheckerL, tiles))
                {
                    _tileCheckerPosition = new Vector3(-1, 0, 0);
                    updateValue(_tileCheckerPosition);     
                }
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                //updateValue(tileCheckerF, tiles);       
                if (checkTile(tileCheckerF, tiles))
                {
                    _tileCheckerPosition = new Vector3(0, 0, 1);
                    updateValue(_tileCheckerPosition);       
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                // updateValue(tileCheckerB, tiles);
                if (checkTile(tileCheckerB, tiles))
                {
                    _tileCheckerPosition = new Vector3(0, 0, -1);
                    updateValue(_tileCheckerPosition);              
                }
            }

            /*if (Input.GetKeyDown(KeyCode.Q))
            {
                turnManager_Script.enviTurn -= 3;
                //reverseTurn(3);
                turnManager_Script.heroTurn++;
                _actionPoint--;
               
                // print("turn: " + turnManager_Script.enviTurn + "heroturn: " + turnManager_Script.heroTurn);
            }*/

            if (transform.position.x == _winPointX && transform.position.z == _winPointZ)
            {
                print("Win");
                //_state = 1; // Win
                _isEndGame = true;
                print(turnManager_Script.isWarp);
                if (turnManager_Script.isWarp)
                {
                    SceneManager.LoadScene(Warp);   
                }
                else
                {
                    endGameImageWin.gameObject.SetActive(_isEndGame);
                    restartGame.gameObject.SetActive(_isEndGame);
                }
            }
            else
            {
                if (_actionPoint == 0)
                {
                    print("Lose");
                    _isEndGame = true;
                    endGameImageLose.gameObject.SetActive(_isEndGame);
                    restartGame.gameObject.SetActive(_isEndGame);
                }
            }
        }

    }

   public void updateValue(Vector3 input)
    {
        _targetPosition = _targetPosition + input;
        _heroOnMove = true;
    }

   public void reverseTurn(int reverseTurn)
    {
        isReversing = true;
        print("Turn: " + reverseTurn);
        _reverseTurn = reverseTurn;
        turnManager_Script.enviTurn = turnManager_Script.savedTurn[reverseTurn-1];
        turnManager_Script.savedTurn.Add(turnManager_Script.enviTurn);
        turnManager_Script.enviTurn++;
        turnManager_Script.heroTurn++;
        _actionPoint--;
        _textAP.text = " " + _actionPoint;
        print("heroturn: " + turnManager_Script.heroTurn + " turn: " + turnManager_Script.enviTurn  );
    }


    private bool checkTile(GameObject input,GameObject[] tiles)
    {
        bool output = false;
        foreach(GameObject tile in tiles)
        {
            if(( input.transform.position.x == tile.transform.position.x) && (input.transform.position.z == tile.transform.position.z))
            {
                output = true;
            }
        }
        return output;
    }

    //Check position of hero same tile with position of mon
    private void OnTriggerEnter(Collider other)
    {
        //print(other.name);
        if(other.gameObject.tag == "Enemy")
        {
            print("OUCH!");
            _isEndGame = true;
            endGameImageLose.gameObject.SetActive(_isEndGame);
            restartGame.gameObject.SetActive(_isEndGame);
        }
    }

}

    
