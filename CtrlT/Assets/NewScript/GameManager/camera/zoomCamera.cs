using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoomCamera : MonoBehaviour {

    public float panSpeed = 2f;
    public float panBorderThickness = 10f;
    public Vector2 panLimit;

   /* public float scrollSpeed = 20f;
    public float minY ;
    public float maxY ;*/

    private float sensitivity = 10f;

    // Use this for initialization
   void Update()
    {
        Vector3 pos = transform.position;
        //if(Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        if (Input.GetKey("w"))
        {
            pos.z += panSpeed * Time.deltaTime;
        }
        //if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        if (Input.GetKey("s") )
        {
            pos.z -= panSpeed * Time.deltaTime;
        }
        //if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        if (Input.GetKey("d") )
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        //if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        if (Input.GetKey("a"))
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        /*float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollSpeed * 100f * Time.deltaTime;*/
        /*float fov = Camera.main.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minY, maxY);
        Camera.main.fieldOfView = fov;*/

        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
       // pos.y = Mathf.Clamp(pos.y, minY, maxY);   
        pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);

        transform.position = pos;
    }
	
	// Update is called once per frame
	/*void Update () {

        
        float fov = Camera.main.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;

    }*/
}
