using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class main_selectEP : MonoBehaviour {

    //EP2
    public GameObject lightEp2;
    public GameObject crystalEP2;
    public GameObject lightCrystalEp2;

    //EP3
    public GameObject lightEp3;
    public GameObject crystalEP3;
    public GameObject lightCrystalEp3;

    public bool isEP2 = false;
    public bool isEP3 = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isEP2)
        {
            lightEp2.SetActive(true);
            crystalEP2.SetActive(true);
            lightCrystalEp2.SetActive(true);
        }
        if (isEP3)
        {
            lightEp3.SetActive(true);
            crystalEP3.SetActive(true);
            lightCrystalEp3.SetActive(true);
        }
	}
}
