using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour {

    public int actionPoint;
    public int typeHeroSkill; // 1 = normal , 2 = freeze , 3 = star
    public Text _textAP;
    public Vector3 winPoint;
    public RawImage endGameImageWin;
    public RawImage endGameImageLose;
    public Button restartGame;

    public bool isReversing { get; set; }

    private heroScript hero_Script;
    private enemyScript enemy_Script;

    public GameObject buttonManager_GameObject;
    private buttonManager button_Script;

    private int _reverseTurn;

    private Vector3 _tileCheckerPosition;
    public bool _isEndGame;

    // Use this for initialization
    void Start () {
        GameObject hero = GameObject.FindGameObjectWithTag("hero");
        hero_Script = hero.GetComponent<heroScript>();

        GameObject enemy = GameObject.FindGameObjectWithTag("enemy");
        _textAP.text = " " + actionPoint;

        button_Script = buttonManager_GameObject.GetComponent<buttonManager>();

        endGameImageWin.gameObject.SetActive(false);
        endGameImageLose.gameObject.SetActive(false);
        restartGame.gameObject.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
        _textAP.text = " " + actionPoint;
        if (!_isEndGame)
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

                        if (!enemy_Script.isFreeze)
                        {
                            enemy_Script.EnemyOnMove();
                        }
                    }
                    hero_Script.heroFinishedMove = false;
                }
            }

        }
    }

    public void reverseTurn(int reverseTurn, Button clickedButton)
    {
        if (!_isEndGame)
        {
            //isReversing = true;
            button_Script.createButton(1, clickedButton);
            //isReversing = true;
            _reverseTurn = reverseTurn;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
            foreach (GameObject enemy in enemies)
            {
                enemy_Script._isReversing = true;
                enemy_Script = enemy.GetComponent<enemyScript>();
                enemy_Script._enemyTurn = enemy_Script.savedEnemyTurn[_reverseTurn];
                enemy_Script.savedEnemyTurn.Add(enemy_Script._enemyTurn);
                enemy_Script._enemyTurn++;
            }
            hero_Script._heroTurn++;

            actionPoint--;
            _textAP.text = " " + actionPoint;
           print("heroturn: " + hero_Script._heroTurn + " Enemy turn: " + enemy_Script._enemyTurn);

        }
    }
}
