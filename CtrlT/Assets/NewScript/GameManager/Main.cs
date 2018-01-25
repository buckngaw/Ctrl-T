using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour {

    public int actionPoint;
    //public int typeHeroSkill; 
    public int countStarWin;
    public int fixActionPointTurn;
    public int fixNumFreeze;
    public Text _textAP;
    public Vector3 winPoint;
    public Text _textStar;
    public Text _textStarBack;
    public Text _fixReversetext;
    public Text _fixFreezetext;
    public RawImage fixCircleReverse;
    public RawImage fixCirclePause;
    public RawImage endGameImageWin;
    public RawImage endGameImageLose;
    public Image starImage;
    public Button reverseButton;
    public Button freezeButton;
    public Button forwardButton;
    public GameObject buttonManager_GameObject;
    public GameObject PanelObject;
    public bool[] ChooseFeature; // 0 = freeze , 1 = star , 2 = FixReverse , 3 = monCollectItem , 4 = FixFreeze(select 0)
    public bool isWarp;

    public bool _isEndGame { get; set; }
    public bool _isfreeze { get; set; }
    public bool onTrigger { get; set; }
    public bool _isFixReverse { get; set; }
    public bool _isFixPause { get; set; }
    public bool _isReverse { get; set; } // Use to can't click tile
    public bool _isReverseFinish { get; set; } // Use when fading and change enemy pos.y
    public bool _cantPause { get; set; }

    private heroScript hero_Script;
    private enemyScript enemy_Script;
    private buttonManager button_Script;
    private int _reverseTurn;
    private int _numReverseTurn;
    private int _numFreeze;
    private Vector3 _tileCheckerPosition;

    // Use this for initialization
    void Start () {
        GameObject hero = GameObject.FindGameObjectWithTag("hero");
        hero_Script = hero.GetComponent<heroScript>();
        GameObject enemy = GameObject.FindGameObjectWithTag("enemy");
        _textAP.text = "" + actionPoint;

        button_Script = buttonManager_GameObject.GetComponent<buttonManager>();

        _numReverseTurn = 0;
        _numFreeze = 0;
        onTrigger = false;
        _isFixReverse = false;
        _isReverse = false;

        starImage.gameObject.SetActive(false);

        fixCircleReverse.gameObject.SetActive(false);

        fixCirclePause.gameObject.SetActive(false);
        freezeButton.interactable = false;
        //change normal color of button
        /*ColorBlock cb = freezeButton.colors;
        cb.normalColor = Color.gray; 
        freezeButton.colors = cb;*/

        if (ChooseFeature[0])
        {
            //freezeButton.gameObject.SetActive(true);
            freezeButton.interactable = true;
        }
        if (ChooseFeature[1])
        {
            starImage.gameObject.SetActive(true);
            _textStarBack.text = "  /" + countStarWin;
        }
        if (ChooseFeature[2])
        {
            print("fixActionPointTurn" + fixActionPointTurn);
            _fixReversetext.text = "" + fixActionPointTurn;
            fixCircleReverse.gameObject.SetActive(true);
        }
        if (ChooseFeature[4])
        {
            _fixFreezetext.text = "" + fixNumFreeze;
            fixCirclePause.gameObject.SetActive(true);
        }
        /*if (typeHeroSkill == 1) // normal mode
        {
            freezeButton.gameObject.SetActive(false);
            starImage.gameObject.SetActive(false);
            //print("typeHeroSkill 1");
        }
        if (typeHeroSkill == 3) // star mode
        {
            freezeButton.gameObject.SetActive(false);
            _textStarBack.text = "  /" + countStarWin;
        }
        if (typeHeroSkill == 2) // freeze mode
        {
            starImage.gameObject.SetActive(false);
        }
        if(typeHeroSkill == 4) //fix AP
        {
            starImage.gameObject.SetActive(false);
        }*/

        endGameImageWin.gameObject.SetActive(false);
        endGameImageLose.gameObject.SetActive(false);
        //restartGame.gameObject.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
        _textAP.text = "" + actionPoint;
        if (!_isEndGame)
        {
            if (!_isfreeze)
            {
                if (hero_Script.isMove)
                {
                    //hero move
                    if (hero_Script.direction[0])
                    {
                        _tileCheckerPosition = new Vector3(1, 0, 0);
                        heroJump(_tileCheckerPosition);
                        hero_Script.HeroOnMove(_tileCheckerPosition);
                        hero_Script.direction[0] = false;
                        button_Script.createButton(0, null);
                    }

                    if (hero_Script.direction[1])
                    {
                        _tileCheckerPosition = new Vector3(-1, 0, 0);
                        heroJump(_tileCheckerPosition);
                        hero_Script.HeroOnMove(_tileCheckerPosition);
                        //actionPoint--;
                        hero_Script.direction[1] = false;
                        button_Script.createButton(0, null);
                    }

                    if (hero_Script.direction[2])
                    {
                        _tileCheckerPosition = new Vector3(0, 0, 1);
                        heroJump(_tileCheckerPosition);
                        hero_Script.HeroOnMove(_tileCheckerPosition);
                        //actionPoint--;
                        hero_Script.direction[2] = false;
                        button_Script.createButton(0, null);
                    }

                    if (hero_Script.direction[3])
                    {
                        _tileCheckerPosition = new Vector3(0, 0, -1);
                        heroJump(_tileCheckerPosition);
                        hero_Script.HeroOnMove(_tileCheckerPosition);
                        //actionPoint--;
                        hero_Script.direction[3] = false;
                        button_Script.createButton(0, null);
                    }

                    if (hero_Script.heroFinishedMove)
                    {
                        //print("hero transform: " + hero_Script.transform.position);
                        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
                        foreach (GameObject enemy in enemies)
                        {
                            enemy_Script = enemy.GetComponent<enemyScript>();
                            //print("enemy transform: " + enemy_Script.transform.position);
                            enemy_Script.EnemyOnMove();
                            //enemy_Script.onTriggle = false;
                        }
                        hero_Script.heroFinishedMove = false;
                    }
                }

            }
            else
            {
                // freezing
            }
            if (ChooseFeature[3])
            {
                bool win = false;
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
                foreach (GameObject enemy in enemies)
                {
                    enemy_Script = enemy.GetComponent<enemyScript>();
                    if (enemy_Script.onTriggle)
                    {
                        win = true;
                    }
                    else
                    {
                        win = false;
                        break;
                    }
                }
                onTrigger = win;
            }
            if (ChooseFeature[2] == true && fixActionPointTurn == _numReverseTurn)
            {
                _isFixReverse = true;
            }
        }
    }

    public void reverseTurn(int reverseTurn, Button clickedButton)
    {
        if (!_isEndGame)
        {
            if(ChooseFeature[2] == true && fixActionPointTurn == _numReverseTurn)
            {
                print("Can't Reverse Again!!");
            }
            else
            {
                //_isReverse = true;
                button_Script.createButton(1, clickedButton); // 1 = new line , get btn that clicked
                _reverseTurn = reverseTurn;
                //print("reverseTurn: " + _reverseTurn);
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
                foreach (GameObject enemy in enemies)
                {
                    enemy_Script = enemy.GetComponent<enemyScript>();
                    if (enemy_Script.isFreeze)
                    {
                        //enemy_Script._isReversingAndFreezing = true;
                        //enemy_Script._enemyTurn = enemy_Script.savedEnemyTurn[enemy_Script._enemyTurn];
                        //print("enemyTurn in freeze: " + enemy_Script._enemyTurn);
                        enemy_Script.savedEnemyTurn.Add(enemy_Script._enemyTurn); // use old position
                    }
                    else
                    {
                        enemy_Script._isReversing = true;
                        enemy_Script._enemyTurn = enemy_Script.savedEnemyTurn[_reverseTurn]; // Location = turn that reverse
                        if (_isReverseFinish)
                        {
                            //enemy change pos.y 
                            enemy_Script._isChangePosY = true;
                        }
                        //print("savedEnemyTurn: " + enemy_Script._enemyTurn);
                        enemy_Script.savedEnemyTurn.Add(enemy_Script._enemyTurn);
                        //_isReverse = false;
                        //enemy_Script._enemyTurn++;
                    }
                    /*if (ChooseFeature[3] && !enemy_Script.onTriggle)
                    {
                        enemy_Script.onTriggle = false;
                    }*/
                }
                hero_Script._heroTurn++;
                _numReverseTurn++;
                if (ChooseFeature[2])
                {
                    if(fixActionPointTurn - _numReverseTurn > 0)
                    {
                        _fixReversetext.text = "" + (fixActionPointTurn - _numReverseTurn);
                    }
                    else
                    {
                        _fixReversetext.text = "";
                        fixCircleReverse.gameObject.SetActive(false);
                        reverseButton.interactable = false;
                    }

                }
                actionPoint--;
                _textAP.text = "" + actionPoint;
                //print("heroturn: " + hero_Script._heroTurn + " Enemy turn: " + enemy_Script._enemyTurn);
            }
        
        }
    }

    public void freezeEnvi()
    {
        if (!_isEndGame)
        {
            print("numFreeze" + _numFreeze);
            if (ChooseFeature[4] == true && fixNumFreeze == _numFreeze)
            {
                print("Can't Freeze Again!!");
                _isFixPause = true;
            }
            else
            {
                //reset enemies're isfreeze
                _isfreeze = true;
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
                foreach (GameObject enemy in enemies)
                {

                    Behaviour Halo = (Behaviour)enemy.GetComponent("Halo");
                    Halo.enabled = false;
                    enemy_Script = enemy.GetComponent<enemyScript>();
                    enemy_Script.isFreeze = false;

                    //enemy_Script.freezeGameObject.gameObject.SetActive(false);          
                }
                _numFreeze++;
                if (ChooseFeature[4])
                {
                    if (fixNumFreeze - _numFreeze > 0)
                    {
                        _fixFreezetext.text = "" + (fixNumFreeze - _numFreeze);
                    }
                    else
                    {
                        _fixFreezetext.text = "";
                        fixCirclePause.gameObject.SetActive(false);
                        freezeButton.interactable = false;
                    }
                }
                // reset halo when choose to freeze enemy
                /*if (_isClickedFreeze)
                {
                    print("eiei");
                    foreach (GameObject enemy in enemies)
                    {
                        Behaviour Halo = (Behaviour)enemy.GetComponent("Halo");
                        Halo.enabled = false;
                    }
                    _isClickedFreeze = false;
                }*/

            }
        }
       
        _textAP.text = "" + actionPoint;
    }

    private void heroJump(Vector3 positionMove)
    {
        GameObject hero = GameObject.Find("Hero");
        hero.transform.GetChild(4).GetComponent<Animator>().Play("chickyJump");
        if(hero_Script._targetPosition.z == hero_Script._targetPosition.z - positionMove.z)
        {
            if(hero_Script._targetPosition.x > hero_Script._targetPosition.x - positionMove.x)
            {
                hero.transform.GetChild(4).eulerAngles = new Vector3(0, 90, 0);
            }
            else
            {
                hero.transform.GetChild(4).eulerAngles = new Vector3(0, -90, 0);
            }    
        }else if (hero_Script._targetPosition.x == hero_Script._targetPosition.x - positionMove.x)
        {
            if (hero_Script._targetPosition.z > hero_Script._targetPosition.z - positionMove.z)
            {
                hero.transform.GetChild(4).eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                hero.transform.GetChild(4).eulerAngles = new Vector3(0, 180, 0);
            }
        }
    }

    /*
     * if (positions[index].z == positions[index - 1].z && isJump == true)
                {
                    this.transform.eulerAngles = new Vector3(0, -90, 0);
                    this.transform.GetChild(1).GetComponent<Animator>().Play("jump");
                    isJump = false;
                    print("jump");
                    isJump = false;
                }else if(positions[index].x == positions[index - 1].x && isJump == true)
                {
                    this.transform.eulerAngles = new Vector3(0, 0, 0);
                    this.transform.GetChild(1).GetComponent<Animator>().Play("jump");
                    isJump = false;
                    print("jump");
                }*/
}
