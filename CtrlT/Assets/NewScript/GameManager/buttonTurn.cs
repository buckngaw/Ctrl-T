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
    private GameObject fadeManager;
    GameObject Panel;
    GameObject Swap;
    // Use this for initialization
    void Start()
    {
        Main_GameObject = GameObject.Find("Main");
        main_Script = Main_GameObject.GetComponent<Main>();
        fadeManager = GameObject.Find("fadeManager");
        /*camare_object = GameObject.FindGameObjectWithTag("MainCamera");
        camera_animator = camare_object.GetComponent<Animator>();*/

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void buttonOnClick()
    {
        //Create reverse
        main_Script.reverseTurn(turn, transform.GetComponent<Button>());
        //Fade in
        fadeManager.GetComponent<FadeManager>().Fade(true, 1.5f);
        //Wait for close fade
        StartCoroutine(WaitForCloseFade(0.3f));
        //Wait for change enemy pos.y
        StartCoroutine(WaitEnemyChangePos(1.0f));
        main_Script._isReverse = false;
    }

    private IEnumerator WaitForCloseFade(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //Close timeline
        Panel = GameObject.Find("Panel");
        Panel.SetActive(false);
        //Close Swap
        Swap = GameObject.Find("Swap");
        Swap.SetActive(false);
        //Fade out
        fadeManager.GetComponent<FadeManager>().Fade(false, 1.5f);
    }

    //Change Y position(enemy)
    private IEnumerator WaitEnemyChangePos(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        main_Script._isReverseFinish = true;
    }
}
