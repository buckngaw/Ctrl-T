using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{

    public GameObject episode;
    private int count;
    private int angle;

    // Use this for initialization
    void Start()
    {
        count = 1;
        angle = 1200;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onRightClick()
    {
        print("right");    
        episode.transform.Rotate(Vector3.up * 120, Space.World);
        /*Quaternion newRotation = Quaternion.AngleAxis(angle, Vector3.up);
        episode.transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, .05f);*/ 
    }

    public void onLeftClick()
    {
        print("left");
        episode.transform.Rotate(Vector3.up * -120, Space.World);
    }
}
