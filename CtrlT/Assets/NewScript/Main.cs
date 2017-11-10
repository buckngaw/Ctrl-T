using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour {

    public int actionPoint;
    public int typeHeroSkill; // 1 = normal , 2 = freeze , 3 = star
    public int countStarWin;
    public Text _textAP;
    public Vector3 winPoint;
    public Text _textStar;
    public Text _textStarBack;
    public RawImage endGameImageWin;
    public RawImage endGameImageLose;
    public Image starImage;
    public Button freezeButton;
    public Button restartGame;

    private heroScript hero_Script;
    private enemyScript enemy_Script;

    public GameObject buttonManager_GameObject;
    private buttonManager button_Script;

    private int _reverseTurn;

    private Vector3 _tileCheckerPosition;
    public bool _isEndGame { get; set; }
    public bool _isfreeze { get; set; }

    // Use this for initialization
    void Start () {
        GameObject hero = GameObject.FindGameObjectWithTag("hero");
        hero_Script = hero.GetComponent<heroScript>();
        GameObject enemy = GameObject.FindGameObjectWithTag("enemy");
        _textAP.text = " " + actionPoint;

        button_Script = buttonManager_GameObject.GetComponent<buttonManager>();

        if (typeHeroSkill == 1) // normal mode
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

        endGameImageWin.gameObject.SetActive(false);
        endGameImageLose.gameObject.SetActive(false);
        restartGame.gameObject.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
        _textAP.text = " " + actionPoint;
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
                        hero_Script.HeroOnMove(_tileCheckerPosition);
                        actionPoint--;
                        hero_Script.direction[0] = false;
                        button_Script.createButton(0, null);
                    }

                    if (hero_Script.direction[1])
                    {
                        _tileCheckerPosition = new Vector3(-1, 0, 0);
                        hero_Script.HeroOnMove(_tileCheckerPosition);
                        actionPoint--;
                        hero_Script.direction[1] = false;
                        button_Script.createButton(0, null);
                    }

                    if (hero_Script.direction[2])
                    {
                        _tileCheckerPosition = new Vector3(0, 0, 1);
                        hero_Script.HeroOnMove(_tileCheckerPosition);
                        actionPoint--;
                        hero_Script.direction[2] = false;
                        button_Script.createButton(0, null);
                    }

                    if (hero_Script.direction[3])
                    {
                        _tileCheckerPosition = new Vector3(0, 0, -1);
                        hero_Script.HeroOnMove(_tileCheckerPosition);
                        actionPoint--;
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
                        }
                        hero_Script.heroFinishedMove = false;
                    }
                }

            }
            else
            {
                // freezing
            }

        }
    }

    public void reverseTurn(int reverseTurn, Button clickedButton)
    {
        if (!_isEndGame)
        {
            button_Script.createButton(1, clickedButton); // 1 = new line , get btn that clicked
            _reverseTurn = reverseTurn;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
            foreach (GameObject enemy in enemies)
            {
                enemy_Script = enemy.GetComponent<enemyScript>();
                if (enemy_Script.isFreeze)
                {
                    //enemy_Script._isReversingAndFreezing = true;
                    enemy_Script._enemyTurn = enemy_Script.savedEnemyTurn[enemy_Script._enemyTurn];
                    //print("enemyTurn in freeze: " + enemy_Script._enemyTurn);
                    enemy_Script.savedEnemyTurn.Add(enemy_Script._enemyTurn); // use old position
                }
                else
                {
                    enemy_Script._isReversing = true;
                    enemy_Script._enemyTurn = enemy_Script.savedEnemyTurn[_reverseTurn];
                    enemy_Script.savedEnemyTurn.Add(enemy_Script._enemyTurn);
                    enemy_Script._enemyTurn++;
                }
            }
            hero_Script._heroTurn++;
            actionPoint--;
            _textAP.text = " " + actionPoint;
           print("heroturn: " + hero_Script._heroTurn + " Enemy turn: " + enemy_Script._enemyTurn);

        }
    }

    public void freezeEnvi()
    {
        //reset enemies're isfreeze
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy_Script = enemy.GetComponent<enemyScript>();
            enemy_Script.isFreeze = false;
        }
        _isfreeze = true;
        _textAP.text = " " + actionPoint;
    }
}
