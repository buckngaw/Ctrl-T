using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class heroController : MonoBehaviour {

    //turnManager
    public GameObject turnManager_GameObject;
    private turnManager turnManager_Script;

    public GameObject buttonManager_obj;
    private Dynamicbtn buttonManager_script;

    public Button freezeButton;
    public GameObject tileCheckerR;
    public GameObject tileCheckerL;
    public GameObject tileCheckerF;
    public GameObject tileCheckerB;

    public Text _textAP;
    public Text _textStar;
    public Text _textStarBack;

    public RawImage endGameImageWin;
    public RawImage endGameImageLose;
    public Image starImage;
    public Button restartGame;
    public bool isReversing { get; set; }
    public int _reverseTurn { get; set; }
    public string Warp;   //string that warp to other scence
    public bool _isChangeState { get; set; }

    private float _movespeed = 4.0f;
    private float _winPointX;
    private float _winPointZ;
    private int _heroTurn;
    private int _enviTurn;
    private int _actionPoint;
    private int _countStar;
    private List<int> _saveTurn;
    private bool _isEndGame;
    private bool _heroOnMove;
    private Vector3 _targetPosition;
    private Vector3 _tileCheckerPosition;


    // Use this for initialization
    void Start () {
        //use variable in turnManagerScript
        turnManager_Script = turnManager_GameObject.GetComponent<turnManager>();
        buttonManager_script = buttonManager_obj.GetComponent<Dynamicbtn>();
        //set type of each level
        if(turnManager_Script.typeHeroSkill == 1) // normal mode
        {
            freezeButton.gameObject.SetActive(false);
            starImage.gameObject.SetActive(false);
            //print("typeHeroSkill 1");
        }
        if (turnManager_Script.typeHeroSkill == 3) // star mode
        {
            freezeButton.gameObject.SetActive(false);
            _textStarBack.text = "  /" + turnManager_Script.countStarWin;
        }
        if (turnManager_Script.typeHeroSkill == 2) // freeze mode
        {
            starImage.gameObject.SetActive(false);
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
        _countStar = 0;
        _isChangeState = false;
        _targetPosition = transform.position;

    }
	
	// Update is called once per frame
	void Update () {
        _textAP.text = " " + _actionPoint;

        if (!_isEndGame)
        {
            GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile"); //tiles = object that has tag = "Tile"
            if (!_isChangeState)
            {
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
                        buttonManager_script.createButton(0, null);
                    }
                }

                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    //updateValue(tileCheckerL, tiles);
                    if (checkTile(tileCheckerL, tiles))
                    {
                        _tileCheckerPosition = new Vector3(-1, 0, 0);
                        updateValue(_tileCheckerPosition);
                        buttonManager_script.createButton(0, null);
                    }
                }
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    //updateValue(tileCheckerF, tiles);       
                    if (checkTile(tileCheckerF, tiles))
                    {
                        _tileCheckerPosition = new Vector3(0, 0, 1);
                        updateValue(_tileCheckerPosition);
                        buttonManager_script.createButton(0, null);
                    }
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    // updateValue(tileCheckerB, tiles);
                    if (checkTile(tileCheckerB, tiles))
                    {
                        _tileCheckerPosition = new Vector3(0, 0, -1);
                        updateValue(_tileCheckerPosition);
                        buttonManager_script.createButton(0,null);
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

                //Check IsWin
                if (transform.position.x == _winPointX && transform.position.z == _winPointZ)
                {
                    if (turnManager_Script.typeHeroSkill == 3)
                    {
                        if (_countStar == turnManager_Script.countStarWin)
                        {
                            print("Win");
                            _isEndGame = true;
                            //print(turnManager_Script.isWarp);
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
                    }
                    else
                    {
                        print("Win");
                        _isEndGame = true;
                        //print(turnManager_Script.isWarp);
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
            else
            {
                print("eiei2");
            }
        }

    }

    private void OnMouseDown()
    {
        _isChangeState = true;
       
    }

    public void freeze()
    {
        _isChangeState = true;
        print("eiei");
        print(_isChangeState);
    }

    public void updateValue(Vector3 input)
    {
        _targetPosition = _targetPosition + input;
        _heroOnMove = true;
    }

   public void reverseTurn(int reverseTurn, Button clickedButton)
    {
        if (!_isEndGame)
        {
            buttonManager_script.createButton(1, clickedButton);

            isReversing = true;
            _reverseTurn = reverseTurn;
            turnManager_Script.enviTurn = turnManager_Script.savedTurn[reverseTurn];
            turnManager_Script.savedTurn.Add(turnManager_Script.enviTurn);
            turnManager_Script.enviTurn++;
            turnManager_Script.heroTurn++;
            _actionPoint--;
            _textAP.text = " " + _actionPoint;
            print("heroturn: " + turnManager_Script.heroTurn + " turn: " + turnManager_Script.enviTurn);
            
        }
    }

    //GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
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

        //star
        if (other.gameObject.tag == "star")
        {
            print("OUCH!");
            _countStar++;
            other.gameObject.SetActive(false);
            _textStar.text = " " + _countStar;
        }
    }

  

}

    
