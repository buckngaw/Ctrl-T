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
    }

    void Update()
    {
        
    }

    public void createButton(int inputLine, Button clickedButton) // 0 = same line, 1 = add line
    {
        Button goButton = Instantiate(prefabButton).GetComponent<Button>();
        goButton.GetComponent<PrefabButton>().turn = turnManager_Script.heroTurn;     //set parameter in PrefabButton.cs
        goButton.transform.SetParent(ParentPanel, false);
        goButton.GetComponentInChildren<Text>().text = (turnManager_Script.heroTurn + 1).ToString();
        goButton.transform.localScale = new Vector3(1, 1, 1);
        Vector3 defaultPosition = new Vector3(-220, -115, 0);
        Vector3 pos = defaultPosition;
        
        if (inputLine == 1)
        {
            _line++; // set new line when reverseTurn
            pos.x = clickedButton.GetComponent<RectTransform>().anchoredPosition.x; //same pos.x of turn that reverse
            pos.y = defaultPosition.y + (50f * _line);
        }
        else
        {
            if (turnManager_Script.heroTurn == 0)
            {
                // DO NOTHING.
            }
            else
            {
                pos.x = savedPostionButtonTurn[turnManager_Script.heroTurn - 1] + 50.0f;
                pos.y = defaultPosition.y + (50f * _line);
            }
            
            _heroTurn++;

        }

        RectTransform rectTransform = goButton.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = pos;
        savedPostionButtonTurn.Add(pos.x);
        
    }

}