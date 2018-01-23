using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class reversePanel : MonoBehaviour {

    public GameObject PanelObject;
    public GameObject Swap;
    public RawImage cantReverse;
    public RawImage cantPause;
    //MAIN
    public GameObject Main_GameObject;
    private Main Main_Script;

    private statusTile statusTile_Script;

	// Use this for initialization
	void Start () {
        Main_Script = Main_GameObject.GetComponent<Main>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Main_Script._isFixPause)
        {
            cantPause.gameObject.SetActive(true);
            StartCoroutine(disablePauseText(1.5f));
        }
    }

    public void shPanel()
    {
        
        if (!Main_Script._isFixReverse)
        {
            PanelObject.gameObject.SetActive(true);
            Swap.gameObject.SetActive(true);
            Main_Script._isReverse = true;
        }
        else if(Main_Script._isFixReverse)
        {
            cantReverse.gameObject.SetActive(true);
            StartCoroutine(disableReverseText(1.5f));
            PanelObject.gameObject.SetActive(false);
            Swap.gameObject.SetActive(false);
        }
    }

    private IEnumerator disableReverseText(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Main_Script._isFixReverse = false;
        cantReverse.gameObject.SetActive(false);
    }

    private IEnumerator disablePauseText(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Main_Script._isFixPause = false;
        cantPause.gameObject.SetActive(false);
    }
}
