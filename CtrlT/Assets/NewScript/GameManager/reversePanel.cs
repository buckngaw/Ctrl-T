using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class reversePanel : MonoBehaviour {

    public GameObject PanelObject;
    public GameObject Swap;
    public RawImage ReverseFixText;
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
		
	}

    public void shPanel()
    {
        
        if (!Main_Script._isFixReverse)
        {
            PanelObject.gameObject.SetActive(true);
            Swap.gameObject.SetActive(true);
        }
        else if(Main_Script._isFixReverse)
        {
            ReverseFixText.gameObject.SetActive(true);
            PanelObject.gameObject.SetActive(false);
            Swap.gameObject.SetActive(false);
        }
       
    }
}
