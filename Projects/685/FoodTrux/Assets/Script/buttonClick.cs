using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class buttonClick : MonoBehaviour {
	public Button yourButton;
	//public Vector3 spawnPoint = Vector3.zero;

	Vector3 position = new Vector3 (300.5f, 0f, 402.4f);



	void Start () {
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
		//position = transform.position.Set(514.5f,0f,402.4f);
	}

	void TaskOnClick(){
		Debug.Log ("You have clicked the button!");


		//GameObject truckToCopy= GameObject.Find("Truck"+idNum);
		GameObject truckToCopy= GameObject.Find("Truck1");

		GameObject newTruck = Instantiate(truckToCopy, position, transform.rotation) as GameObject;

		//Object.Instantiate (truckToCopy, position, Quaternion.identity);
	}
}
