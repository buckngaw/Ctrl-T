using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reverseBackward : MonoBehaviour {

    public bool isBackward { get; set; }
    public bool isForward { get; set; }

    public void backward() {
       isBackward = true;
    }
    public void forward()
    {
        isForward = true;
    }
}
