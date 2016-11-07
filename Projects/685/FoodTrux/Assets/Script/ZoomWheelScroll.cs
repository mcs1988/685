using UnityEngine;
using System.Collections;

public class ZoomWheelScroll : MonoBehaviour
{

    public int cameraCurrentZoom;
    public int cameraZoomMax;
    public int cameraZoomMin;
    public int scrollRate;
    void Start()
    {
        Camera.main.orthographicSize = cameraCurrentZoom;
    }
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0) // back
        {
            if (cameraCurrentZoom < cameraZoomMax)
            {
                cameraCurrentZoom += 1;
                Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize + scrollRate);
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
        {
            if (cameraCurrentZoom > cameraZoomMin)
            {
                cameraCurrentZoom -= 1;
                Camera.main.orthographicSize = Mathf.Min(Camera.main.orthographicSize - scrollRate);
            }
        }
    }

void OnGUI()
    {
        GUI.Box(new Rect((Screen.width / 3) - 70, Screen.height - 30, 140, 25), "Zoom Level: =" + cameraCurrentZoom);
    }
}

