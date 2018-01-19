using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonTurn : MonoBehaviour
{

    public int turn { get; set; }
    public GameObject Main_GameObject;
    //control animation
    private GameObject camare_object;
    private Animator camera_animator;
    private Main main_Script;   //use heroController.cs

    // Use this for initialization
    void Start()
    {
        Main_GameObject = GameObject.Find("Main");
        main_Script = Main_GameObject.GetComponent<Main>();

        /*camare_object = GameObject.FindGameObjectWithTag("MainCamera");
        camera_animator = camare_object.GetComponent<Animator>();*/

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void buttonOnClick()
    {
        //animation
        //camera_animator.Play("reverse");
        main_Script.reverseTurn(turn, transform.GetComponent<Button>());
        //heroController_Script.reverseTurn(turn, transform.GetComponent<Button>());
    }
}
