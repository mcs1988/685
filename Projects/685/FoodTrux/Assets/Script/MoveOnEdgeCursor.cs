using UnityEngine;
using System.Collections;

public class MoveOnEdgeCursor : MonoBehaviour {

	float mouseSens = 0.1f;
	Vector3 lastPosition;
	public int boundary = 50;
    public int neutralSpeed;
    public int rightClickSpeed;
	int speed = 50;

	int theScreenWidth;
	int theScreenHeight;

	// Use this for initialization
	void Start () {
		theScreenWidth = Screen.width;
		theScreenHeight = Screen.height;
	}

	// Update is called once per frame
	void Update () {

        // Speed Determined depending if rightMouseButton clicked
        if (Input.GetMouseButton(1))
        {
            speed = rightClickSpeed;
        }
        else
        {
            speed = neutralSpeed;
         }

        // If Middle Button is clicked Camera will move.
        if (Input.GetMouseButtonDown (2)) {
			lastPosition = Input.mousePosition;
		}

		if (Input.GetMouseButton(2)){
			Vector3 delta = Input.mousePosition - lastPosition;
			transform.Translate (-delta.x * mouseSens, -delta.y * mouseSens, 0);
			lastPosition = Input.mousePosition;
		}

//		if (!Input.GetMouseButton (2)) {
//			if (Input.mousePosition.x > theScreenWidth - boundary) {
//				transform.position = new Vector3 (transform.position.x + (speed * Time.deltaTime), transform.position.y, transform.position.z);
//			}
//
//			if (Input.mousePosition.x < boundary) {
//				transform.position = new Vector3 (transform.position.x - (speed * Time.deltaTime), transform.position.y, transform.position.z);
//			}
//
//			if (Input.mousePosition.y > theScreenHeight - boundary) {
//				transform.position = new Vector3 (transform.position.x, transform.position.y + (speed * Time.deltaTime), transform.position.z);
//			}
//
//			if (Input.mousePosition.y < boundary) {
//				transform.position = new Vector3 (transform.position.x, transform.position.y - (speed * Time.deltaTime), transform.position.z);
//			}
//		}
	}

	void OnGUI () {
		GUI.Box (new Rect ((Screen.width / 2) - 140, 5, 220, 25), "Mouse Position = " + Input.mousePosition);
        GUI.Box (new Rect ((Screen.width / 2) - 200, Screen.height - 30, 140, 25), "Mouse X = " + Input.mousePosition.x);
		GUI.Box (new Rect ((Screen.width / 2) - 50, Screen.height - 30, 140, 25), "Mouse Y = " + Input.mousePosition.y);
	}
}
