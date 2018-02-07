using SimpleFirebaseUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class heroScript : MonoBehaviour {

    //Main
    public GameObject Main_GameObject;
    private Main Main_Script;

    //Firebase
    Firebase firebase;

    public bool isMove {get; set;} // can move?
    public bool heroOnMove { get; set; } // moving
    public bool[] direction { get; set; } // 0 = right, 1 = left , 2 = front , 3 = back
    public bool heroFinishedMove { get; set; }
    public int _heroTurn { get; set; }
    //public ParticleSystem reverse;

    public GameObject tileCheckerR;
    public GameObject tileCheckerL;
    public GameObject tileCheckerF;
    public GameObject tileCheckerB;
    // Collect tileChecker -> right left font back
    public List<GameObject> tileCheckers = new List<GameObject>();
    public string Warp;


    public Vector3 _targetPosition { get; set; }
    private Vector3 _tileCheckerPosition;

    private float _movespeed = 4.0f; 
    private float _winPointX;
    private float _winPointZ;

    private int _countStar;

    public bool _tileIsWalk { get; set; }
    //public bool _isEffect { get; set; }

    GameObject[] tiles;

    // Use this for initialization
    void Start () {
        Main_Script = Main_GameObject.GetComponent<Main>();
        direction = new bool[4]; // 4 point move ->  0 = right, 1 = left , 2 = front , 3 = back
        isMove = false;
        _tileIsWalk = false;

        //Firebase
        firebase = Firebase.CreateNew("ctrlplust-33926.firebaseio.com");

        //reverse.Stop();
        _targetPosition = transform.position;
        transform.position = new Vector3(0, 7f, 0);
        _countStar = Main_Script.countStarWin;

        _winPointX = Main_Script.winPoint.x;
        _winPointZ = Main_Script.winPoint.z;

        tiles = GameObject.FindGameObjectsWithTag("Tile"); //tiles = object that has tag = "Tile"

        tileCheckers.Add(tileCheckerR);
        tileCheckers.Add(tileCheckerL);
        tileCheckers.Add(tileCheckerF);
        tileCheckers.Add(tileCheckerB);
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _movespeed * Time.deltaTime);
        changeColorPathHero();
        if (heroOnMove)
        {
            if (transform.position == _targetPosition)
            {
                heroFinishedMove = true; // transform.position == _targetPosition
                Main_Script.actionPoint--;
                heroOnMove = false;
                _heroTurn++;
                //print("heroTurn" + _heroTurn);
                //turnManager_Script.savedTurn.Add(turnManager_Script.enviTurn);
            }
        }

        //Check Win?
        if ((transform.position.x == _winPointX) && (transform.position.z == _winPointZ))
        {
            if (!Main_Script.ChooseFeature[3])
            {
                if (_countStar == 0)
                {
                    Win();
                }
            }else if (Main_Script.ChooseFeature[3])
            {
                if (Main_Script.onTrigger)
                {
                    Win();
                }
                else if (Main_Script.actionPoint <= 0 && (transform.position.x != _winPointX && transform.position.z != _winPointZ))
                {
                    if(Main_Script.ChooseFeature[3] || !Main_Script.ChooseFeature[3])
                    {
                        Lose();
                        //Main_Script.restartGame.gameObject.SetActive(Main_Script._isEndGame);
                    }
                }
            }
            
        }
        else
        {
            if (Main_Script.actionPoint <= 0 )
            {
                Lose();
                //Main_Script.restartGame.gameObject.SetActive(Main_Script._isEndGame);
            }
        }
    }

    //If win
    public void Win()
    {
        print("Win");
        this.gameObject.transform.GetChild(4).GetComponent<Animator>().Play("chickyJump");
        Main_Script._isEndGame = true;
        Main_Script.endGameImageWin.gameObject.SetActive(Main_Script._isEndGame);
        if(firebaseServiceForLogin.user.username != "Admin")
        {
            int nextLevel = Main_Script.stateNumber + 1;
            string json = "{ \"username\": \"" + firebaseServiceForLogin.user.username + "\", \"password\": \""
                        + firebaseServiceForLogin.user.password
                        + "\",\"save\":\"" + nextLevel + "\"}";
            firebaseServiceForLogin.user.save = nextLevel.ToString();
            firebase.Child("users/" + firebaseServiceForLogin.user.key).UpdateValue(json, true, FirebaseParam.Empty);
        }
    }

    public void Lose()
    {
        print("Lose");
        Main_Script._isEndGame = true;
        Main_Script.endGameImageLose.gameObject.SetActive(Main_Script._isEndGame);
    }

    private bool checkTile(GameObject input, GameObject[] tiles)
    {
        bool output = false;
        foreach (GameObject tile in tiles)
        {
            if ((input.transform.position.x == tile.transform.position.x) && (input.transform.position.z == tile.transform.position.z))
            {
                output = true;
            }
        }
        return output;
    }
    
    //check tile that hero can walk
    public void changeColorPathHero()
    {
        List<GameObject> tilesCanWalk = new List<GameObject>();
        foreach (GameObject tilechecker in tileCheckers)
        {
            GameObject tile = getWalkTile(tilechecker, tiles);
            if (tile)
            {
                //ADD Tile that hero can walk
                tilesCanWalk.Add(tile);        
            }
           
            //rend.material.mainTexture = Resources.Load<Texture>("uvTile/orange");
        }

        foreach (GameObject tile in tilesCanWalk)
        {
            tile.GetComponent<statusTile>().canWalk = true;
            Renderer rend = tile.gameObject.GetComponent<Renderer>();
            rend.material.mainTexture = Resources.Load<Texture>("uvTile/orange");
        }
    }

    //Get tile(GameObject) that hero can walk
    private GameObject getWalkTile(GameObject input, GameObject[] tiles)
    {
        //statusTile statusTile_Script;
        foreach (GameObject tile in tiles)
        {
            if ((input.transform.position.x == tile.transform.position.x) 
                && (input.transform.position.z == tile.transform.position.z))
            {
                return tile;
            }
            else
            {
                Renderer rend = tile.gameObject.GetComponent<Renderer>();
                tile.GetComponent<statusTile>().canWalk = false;
                if (!tile.GetComponent<statusTile>().isTileMove)
                {
                    rend.material.mainTexture = Resources.Load<Texture>("uvTile/white");
                }else if (tile.GetComponent<statusTile>().isTileMove)
                {
                    rend.material.mainTexture = Resources.Load<Texture>("uvTile/rock");
                }
                
            }
        }
        return null;
    }

    public void checkDirection(GameObject tile)
    {
        if(this.gameObject.transform.position.x == tile.transform.position.x)
        {
            //font or back
            if (this.gameObject.transform.position.z > tile.transform.position.z) 
            {
                //back
                isMove = true;
                direction[3] = true;
            }
            else if (this.gameObject.transform.position.z < tile.transform.position.z) 
            {
                //font
                isMove = true;
                direction[2] = true;
            }
        }
        //right or left
        else if (this.gameObject.transform.position.z == tile.transform.position.z) 
        {
            //left
            if (this.gameObject.transform.position.x > tile.transform.position.x)
            {
                isMove = true;
                direction[1] = true;
            }
            //right
            else if (this.gameObject.transform.position.x < tile.transform.position.x) 
            {
                //print("right");
                isMove = true;
                direction[0] = true;
            }
        }
    }

    public void HeroOnMove(Vector3 input)
    {
        _targetPosition = _targetPosition + input;
        heroOnMove = true;
    }

    //Check position of hero same tile with position of mon
    private void OnTriggerEnter(Collider other)
    {
        //print(other.name);
        if (other.gameObject.tag == "enemy" )
        {
            print("OUCH! you're lose");
            this.gameObject.transform.GetChild(4).GetComponent<Animator>().Play("crash");
            //this.gameObject.SetActive(false);
            Main_Script._isEndGame = true;
            Main_Script.endGameImageLose.gameObject.SetActive(Main_Script._isEndGame);
            //Main_Script.restartGame.gameObject.SetActive(Main_Script._isEndGame);
        }

        //star
        if (other.gameObject.tag == "star")
        {
            print("OUCH!");
            _countStar--;
            other.GetComponent<Animator>().Play("diamonAnim");
           // other.gameObject.SetActive(false);
            Main_Script._textStar.text = "" + _countStar;
        }
        //Needle
        if(other.gameObject.tag == "Needle")
        {
            print("OUCH!");
            this.gameObject.transform.GetChild(4).GetComponent<Animator>().Play("crash");
            this.gameObject.SetActive(false);
            Main_Script._isEndGame = true;
            Main_Script.endGameImageLose.gameObject.SetActive(Main_Script._isEndGame);
        }
    }


}
