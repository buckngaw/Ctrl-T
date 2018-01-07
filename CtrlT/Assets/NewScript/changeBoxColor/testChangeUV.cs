using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testChangeUV : MonoBehaviour {

    Renderer rend;
    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.A))
        {
            rend.material.mainTexture = Resources.Load<Texture>("uvTile/orange");
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            rend.material.mainTexture = Resources.Load<Texture>("uvTile/blue");
        }
	}
}
