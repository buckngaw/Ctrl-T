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

    public List<float> savedPostionButtonTurnX;
    public List<List<float>> matrixPositionBtn = new List<List<float>>();
    private bool isReversing;

    public bool isBackward { get; set; }
    public bool isForkward { get; set; }
    public float _lineY { get; set; }
    public float _lineX { get; set; }

    //private bool isTurn;
    //private int _turn;
    private int _heroTurn;
    private bool _isReverse;
    private bool _setLineZero;
    private float _initPositionButton;
    private List<bool> _isFill;
    private bool[,] _matrixPosition;
    private int actionPoint;
    private float _lineYMax;
    //private int _axisX;
    //private int _axisY;

    // Use this for initialization
    void Start () {
        Main_Script = Main_GameObject.GetComponent<Main>();
        hero_Script = hero_object.GetComponent<heroScript>();
        actionPoint = Main_Script.actionPoint - 1;
        _matrixPosition = new bool[actionPoint, actionPoint];
        _lineYMax = 1;
    }

    // Update is called once per frame
    void Update() {
        //REVERSING
        if (isReversing)
        {
            if (isBackward)
            {
                print("isbacwarding");
                setTimelineBW(isBackward);
                isBackward = false;
            }
            if (isForkward)
            {
                print("isforwarding");
                setTimelineFW(isForkward);
                isForkward = false;
            }
            isReversing = false;
        }
        //DON'T REVERSING
        else if (!isReversing)
        {
            //LINE 0
            if(_lineY == 0)
            {  
                if (isBackward)
                {
                    print("line0 BW");
                    _setLineZero = true;
                    setLineZero(_setLineZero);
                    isBackward = false;
                }
                if (isForkward)
                {
                    print("line0 FW");
                    _setLineZero = false;
                    setLineZero(_setLineZero);
                    isForkward = false;
                }
            }
            //LINE > 0
            else if(_lineY > 0)
            {
                if (isBackward)
                {
                    print("line > 0 BW");
                    setRealtimeBtnBW();
                    isBackward = false;
                }
                if (isForkward)
                {
                    print("line > 0 FW");
                    setRealtimeBtnFW();
                    isForkward = false;
                }
            } 
        }
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
        _initPositionButton = -154;

        //inputLine = 1 when click reverse
        //buttonController_script.isBackward
        if (inputLine == 1)
        {
            // set new line when reverseTurn
            pos.x = clickedButton.GetComponent<RectTransform>().anchoredPosition.x; //same pos.x of turn that reverse
            //Check _lineX = 0?
            if(pos.x == defaultPosition.x)
            {
                _lineX = 0;
            }else if(pos.x != defaultPosition.x)
            {
                _lineX = ((Mathf.Abs(_initPositionButton - pos.x)) / 55) + 1;
            }


            //Check choose enter new line?
            _lineY++;
            for (int i = 0; i <= _lineYMax; i++)
            {
                if (!_matrixPosition[(int)(_lineX), i])
                {
                    pos.y = defaultPosition.y + (55f * i);
                    _lineY = i;
                    break;
                }
            }
            /*if (!_matrixPosition[(int)(_lineX), (int)(_lineY - 1)])
            {
                print("_lineX: " + _lineX);
                print("_lineY: " + _lineY);
                print("case1");
                for (int i = 0; i <= _lineY - 1; i++)
                {
                    if (!_matrixPosition[(int)(_lineX), i])
                    {
                        pos.y = defaultPosition.y + (55f * i);
                        _lineY = i;
                        break;
                    }
                }
            }
            else
            {
                print("case2");
                pos.y = defaultPosition.y + (55f * _lineY);
            }*/
            _lineYMax += 1;
            isReversing = true;
        }
        else
        {
            if (hero_Script._heroTurn == 0)
            {
                _lineX = 0;
                // DO NOTHING. (Begin playing)
            }
            else
            {
                pos.x = savedPostionButtonTurnX[hero_Script._heroTurn - 1] + 55.0f;
                if(_lineY == 0)
                {
                    pos.y = defaultPosition.y + (55f * _lineY);

                }
                else if(_lineY > 0)
                {
                    //if bottom empty

                    if (_matrixPosition[(int)(_lineX), (int)(_lineY)])
                    {
                        bool isEmpty = true;
                        int i = (int)(_lineY)+1;
                        while(isEmpty)
                        {
                            if(!_matrixPosition[(int)(_lineX), i])
                            {
                                pos.y = defaultPosition.y + (55f * i);
                                _lineY = i;
                                isEmpty = false;
                            }
                            i++;
                        }
                    }
                    //Y axis is empty?
                    else if (!_matrixPosition[(int)(_lineX), (int)(_lineY-1)])
                    {
                        for(int i = 0; i <= _lineY-1; i++)
                        {
                            if (!_matrixPosition[(int)(_lineX), i]){
                                pos.y = defaultPosition.y + (55f * i);
                                _lineY = i;
                                break;
                            }
                        }

                    }
                    else
                    {
                        pos.y = defaultPosition.y + (55f * _lineY);
                    }
                }
            }
            _heroTurn++;
        }
        RectTransform rectTransform = goButton.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = pos;
        savedPostionButtonTurnX.Add(pos.x);
        _matrixPosition[(int)_lineX, (int)_lineY] = true;
        _lineX++;
    }


    //Set timeline for backward and reward (reversing)
    private void setTimelineFW(bool isbackward)
    {
        GameObject[] reverseButtons = GameObject.FindGameObjectsWithTag("reverseButton");
        foreach (GameObject reverseButton in reverseButtons)
        {
            reverseButton.GetComponent<Button>().interactable = true;
            if (reverseButton.transform.localPosition.x > savedPostionButtonTurnX[hero_Script._heroTurn - 1])
            {
                reverseButton.GetComponent<Button>().interactable = false;
            }
        }
    }

    private void setTimelineBW(bool isforward)
    {
        GameObject[] reverseButtons = GameObject.FindGameObjectsWithTag("reverseButton");
        foreach (GameObject reverseButton in reverseButtons)
        {
            reverseButton.GetComponent<Button>().interactable = true;
            if (reverseButton.transform.localPosition.x < savedPostionButtonTurnX[hero_Script._heroTurn - 1])
            {
                reverseButton.GetComponent<Button>().interactable = false;
            }
        }
    }

    //Set timeline when don't reverse
    private void setRealtimeBtnBW()
    {
        GameObject[] reverseButtons = GameObject.FindGameObjectsWithTag("reverseButton");
        foreach (GameObject reverseButton in reverseButtons)
        {
            reverseButton.GetComponent<Button>().interactable = true;
            if (reverseButton.transform.localPosition.x > savedPostionButtonTurnX[hero_Script._heroTurn - 1])
            {
                reverseButton.GetComponent<Button>().interactable = false;
            }
        }
    }

    private void setRealtimeBtnFW()
    {
        GameObject[] reverseButtons = GameObject.FindGameObjectsWithTag("reverseButton");
        foreach (GameObject reverseButton in reverseButtons)
        {
            reverseButton.GetComponent<Button>().interactable = true;
            if (reverseButton.transform.localPosition.x <= savedPostionButtonTurnX[hero_Script._heroTurn - 1])
            {
                reverseButton.GetComponent<Button>().interactable = false;
            }
        }
    }


    //Set if haven't reversed
    private void setLineZero(bool isback)
    {
        GameObject[] reverseButtons = GameObject.FindGameObjectsWithTag("reverseButton");
        foreach (GameObject reverseButton in reverseButtons)
        {
            reverseButton.GetComponent<Button>().interactable = isback;
        }
    }
}
