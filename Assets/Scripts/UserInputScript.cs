using UnityEngine;
using System.Collections;
 
public class UserInputScript : MonoBehaviour {
    // Use this for initialization
    void Start () {
    }
 
    // Update is called once per frame
    void Update () {
        MoveCamera();
        RotateCamera();
    }
 
    private void MoveCamera() {
        float xpos = Input.mousePosition.x;
        float ypos = Input.mousePosition.y;
        Vector3 movement = new Vector3(0,0,0);
 
        //horizontal camera movement
        if(xpos >= 0 && xpos < ResourceManager.ScrollWidth) {
            movement.x -= ResourceManager.ScrollSpeed;
        } else if(xpos <= Screen.width && xpos > Screen.width - ResourceManager.ScrollWidth) {
            movement.x += ResourceManager.ScrollSpeed;
        }
 
        //vertical camera movement
        if(ypos >= 0 && ypos < ResourceManager.ScrollWidth) {
            movement.z -= ResourceManager.ScrollSpeed;
        } else if(ypos <= Screen.height && ypos > Screen.height - ResourceManager.ScrollWidth) {
            movement.z += ResourceManager.ScrollSpeed;
        }
		
		//check cursor keys
	    movement.x += ResourceManager.ScrollSpeed * Input.GetAxis("Horizontal");
	    movement.z += ResourceManager.ScrollSpeed * Input.GetAxis("Vertical");
		
		// Drag right mouse button to move camera
		if (Input.GetMouseButton(1)) // RMB
        {
            // Hold button and drag camera around
            movement.x -= Input.GetAxis("Mouse X") * ResourceManager.MouseScrollSpeed * Time.deltaTime;
            movement.z -= Input.GetAxis("Mouse Y") * ResourceManager.MouseScrollSpeed * Time.deltaTime;
        }
 
        //make sure movement is in the direction the camera is pointing
        //but ignore the vertical tilt of the camera to get sensible scrolling
        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0;
 
        //away from ground movement
        movement.y -= ResourceManager.ScrollSpeed * Input.GetAxis("Mouse ScrollWheel");
		
		// Zoom in or out with PgUp/PgDn
		if (Input.GetKey(KeyCode.PageUp)) {
			movement.y += ResourceManager.ZoomSpeed;
		}
		if (Input.GetKey(KeyCode.PageDown)) {
			movement.y -= ResourceManager.ZoomSpeed;
		}
 
        //calculate desired camera position based on received input
        Vector3 origin = Camera.main.transform.position;
        Vector3 destination = origin;
        destination.x += movement.x;
        destination.y += movement.y;
        destination.z += movement.z;
 
        //limit away from ground movement to be between a minimum and maximum distance
        if(destination.y > ResourceManager.MaxCameraHeight) {
            destination.y = ResourceManager.MaxCameraHeight;
        } else if(destination.y < ResourceManager.MinCameraHeight) {
            destination.y = ResourceManager.MinCameraHeight;
        }
 
        //if a change in position is detected perform the necessary update
        if(destination != origin) {
            Camera.main.transform.position = Vector3.MoveTowards(origin, destination, Time.deltaTime * ResourceManager.ScrollSpeed);
        }
    }
 
    private void RotateCamera() {
        Vector3 origin = Camera.main.transform.eulerAngles;
        Vector3 destination = origin;
 
        //detect rotation amount if ALT is being held and the Right mouse button is down
        if((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && Input.GetMouseButton(1)) {
            destination.x -= Input.GetAxis("Mouse Y") * ResourceManager.RotateAmount;
            destination.y += Input.GetAxis("Mouse X") * ResourceManager.RotateAmount;
        }
 
        //if a change in position is detected perform the necessary update
        if(destination != origin) {
            Camera.main.transform.eulerAngles = Vector3.MoveTowards(origin, destination, Time.deltaTime * ResourceManager.RotateSpeed);
        }
    }
}