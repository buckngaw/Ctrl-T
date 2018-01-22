using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonManager : MonoBehaviour {

    public GameObject prefabButtonTurn;
    public RectTransform ParentPanel;

    //Main
    public GameObject Main_GameObject;
    private Main Main_Script;

    //heroScript
    public GameObject hero_object;
    private heroScript hero_Script;

    //buttonController
    public GameObject buttonController_object;
    private reverseBackward buttonController_script;

    public List<float> savedPostionButtonTurn;
    private List<Button> allButton;

    public int _line { get; set; }
    public bool isBackward { get; set; }

    //private bool isTurn;
    //private int _turn;
    private int _heroTurn;
    //private int _axisX;
    //private int _axisY;

    // Use this for initialization
    void Start () {
        Main_Script = Main_GameObject.GetComponent<Main>();
        hero_Script = hero_object.GetComponent<heroScript>();
        //buttonController_script = buttonController_object.GetComponent<reverseBackward>();
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void createButton(int inputLine, Button clickedButton) // 0 = same line, 1 = add line
    {
        Button goButton = Instantiate(prefabButtonTurn).GetComponent<Button>();
        goButton.GetComponent<buttonTurn>().turn = hero_Script._heroTurn;     //set parameter in PrefabButton.cs
        goButton.transform.SetParent(ParentPanel, false);
        goButton.GetComponentInChildren<Text>().text = (hero_Script._heroTurn + 1).ToString();
        //goButton.interactable = false;
        goButton.transform.localScale = new Vector3(1, 1, 1);
        //allButton.Add(goButton);
        Vector3 defaultPosition = new Vector3(-209, -77, 0);
        Vector3 pos = defaultPosition;

        //inputLine = 1 when click reverse
        //buttonController_script.isBackward
        if (inputLine == 1)
        {
            _line++; // set new line when reverseTurn
            pos.x = clickedButton.GetComponent<RectTransform>().anchoredPosition.x; //same pos.x of turn that reverse
            pos.y = defaultPosition.y + (55f * _line);
        }
        else
        {
            if (hero_Script._heroTurn == 0)
            {
                // DO NOTHING. (Begin playing)
            }
            else
            {
                pos.x = savedPostionButtonTurn[hero_Script._heroTurn - 1] + 55.0f;
                pos.y = defaultPosition.y + (55f * _line);
            }

            _heroTurn++;

        }

        RectTransform rectTransform = goButton.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = pos;
        savedPostionButtonTurn.Add(pos.x);


    }

    private void calculateReverse()
    {

    }


}
