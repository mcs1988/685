using UnityEngine;
using System.Collections;

public class baseClicked : MonoBehaviour {


	public Canvas canvas;

	bool isSelected = false;
	protected bool showButton1 = true, showButton2 = true;

	private UnityEngine.NavMeshAgent agent;


	// Use this for initialization
	void Start () {

	
	}

	// Update is called once per frame
	void Update () {

		GameObject truckButton = GameObject.FindGameObjectWithTag ("baseMenu");

		if (isSelected) {
			canvas.enabled = true;
			string name = canvas.name;
			Debug.Log (name);

		} else {
			canvas.enabled = false;

//			truckButton.gameObject.SetActive (false);

		}
	}

	void OnMouseUpAsButton() {
		//canvas.enabled = !canvas.enabled;
		isSelected = !isSelected;
		baseIsSelected ();
		Debug.Log ("Base Has Been Clicked.");
	}

	void baseIsSelected()
	{
		GameObject selected = GameObject.Find("basePlane");

		//GameObject selected = GameObject.Find("Truck"+idNum);


		MeshRenderer rendered = selected.GetComponent<MeshRenderer>();

			foreach (Material m in rendered.materials)
			{
				Color color = m.GetColor ("_OutlineColor");
				color.a = isSelected ? 255.0f : 0.0f;
				m.SetColor ("_OutlineColor", color);

			}
		}
		

}

