using UnityEngine;
using System.Collections;


public class MoneyHandler : MonoBehaviour {

    public int credits;


    // Use this for initialization
    void Start () {
   }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.Box(new Rect((Screen.width / 5) - 70, Screen.height - 30, 140, 25), "Credits: =" + credits);
    }
}
