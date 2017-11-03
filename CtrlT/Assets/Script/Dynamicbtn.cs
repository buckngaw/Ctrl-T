using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Dynamicbtn : MonoBehaviour
{
    //prefabButton.cs
    public GameObject prefabButton;
    public RectTransform ParentPanel;

    //turnManager.cs
    public GameObject turnManager_GameObject;
    private turnManager turnManager_Script;
    //heroController.cs
    public GameObject heroController_GameObject;
    private heroController heroController_Script;

    public List<float> savedPostionButtonTurn;

    private bool isTurn;    
    private int _temp = 0;
    private int _turn;
    private Vector3 i;
    private int _line;
    private int _heroTurn;
    

    // Use this for initialization
    void Start()
    {
        
            turnManager_Script = turnManager_GameObject.GetComponent<turnManager>();
            heroController_Script = heroController_GameObject.GetComponent<heroController>();
            _temp += 1;
            _line = 1;
            _heroTurn = turnManager_Script.heroTurn + 1;
    }

    void Update()
    {
   
        _turn = turnManager_Script.heroTurn; 
        if (_turn == _temp)
        {
            //print("turn++");
            isTurn = true;
        }

       if (isTurn)
        {
            print("create");
            createButton();
            isTurn = false;
            _temp++;
        }
        

    }

    public void createButton()
    {
        GameObject goButton = (GameObject)Instantiate(prefabButton);
        goButton.GetComponent<PrefabButton>().turn = turnManager_Script.heroTurn;     //set parameter in PrefabButton.cs
        goButton.transform.SetParent(ParentPanel, false);
        goButton.GetComponentInChildren<Text>().text = turnManager_Script.heroTurn.ToString();
        goButton.transform.localScale = new Vector3(1, 1, 1);
        Vector3 pos = goButton.transform.position;

        //if reverseTurn
        if (heroController_Script.isReversing == true)
        {
            _line++; // set new line when reverseTurn
            pos.x = savedPostionButtonTurn[(heroController_Script._reverseTurn - 1)]; //same pos.x of turn that reverse
            pos.y = (50f * _line); 
            goButton.transform.position = pos;
            savedPostionButtonTurn.Add(pos.x);
        }
        else{
            if(turnManager_Script.heroTurn == 1)
            {
                pos.x = 50f * _heroTurn;
               
            }
            else
            {
                print(turnManager_Script.heroTurn);
                pos.x = 50f + savedPostionButtonTurn[turnManager_Script.heroTurn - 2];
            }

            pos.y = 50f * _line;
            _heroTurn++;
            goButton.transform.position = pos;
            savedPostionButtonTurn.Add(pos.x);

        }
        
    }

}