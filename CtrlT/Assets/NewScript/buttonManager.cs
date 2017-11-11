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

    public List<float> savedPostionButtonTurn;

    public int _line { get; set; }

    private bool isTurn;
    private int _turn;
    private int _heroTurn;

    // Use this for initialization
    void Start () {
        Main_Script = Main_GameObject.GetComponent<Main>();
        hero_Script = hero_object.GetComponent<heroScript>();
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
            if (hero_Script._heroTurn == 0)
            {
                // DO NOTHING.
            }
            else
            {
                pos.x = savedPostionButtonTurn[hero_Script._heroTurn - 1] + 50.0f;
                pos.y = defaultPosition.y + (50f * _line);
            }

            _heroTurn++;

        }

        RectTransform rectTransform = goButton.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = pos;
        savedPostionButtonTurn.Add(pos.x);

    }


}
