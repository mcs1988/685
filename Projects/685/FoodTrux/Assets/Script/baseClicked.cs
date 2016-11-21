using UnityEngine;
using System.Collections;

public class baseClicked : MonoBehaviour {


	public Canvas canvas;
	bool isSelected = false;

	private UnityEngine.NavMeshAgent agent;


	// Use this for initialization
	void Start () {


	
	}
	
	// Update is called once per frame
	void Update () {

		if (isSelected)
			canvas.enabled = true;
		else
			canvas.enabled = false;
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

