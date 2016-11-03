// MoveToClickPoint.cs
using UnityEngine;

public class MoveToClickPoint : MonoBehaviour {
	public Transform destination;
	public bool canMove = true;

	private NavMeshAgent agent;

	void Start () 
	{
		agent = gameObject.GetComponent<NavMeshAgent>();

		GameObject[] trucks = GameObject.FindGameObjectsWithTag ("Truck");
		foreach (GameObject truck in trucks) {
			MeshRenderer[] renderers = truck.GetComponentsInChildren<MeshRenderer>();
			foreach (MeshRenderer r in renderers) {
				foreach (Material m in r.materials) {
					m.color = Color.green;
				}
			}
		}
	}

	void Update()
	{
		if (canMove) {
			agent.updateRotation = false;
			if (Input.GetMouseButtonDown (0)) {
				int layerMask = 1 << 8;
				layerMask = ~layerMask;

				Ray screenRay = Camera.main.ScreenPointToRay (Input.mousePosition);

				RaycastHit hit;

				if (Physics.Raycast (screenRay, out hit, Mathf.Infinity, layerMask)) {
					agent.destination = hit.point;
				}
			}

			if (Input.GetKeyDown (KeyCode.D)) {
				DeployTruck ();
			}
		}
	}

	void DeployTruck()
	{
		canMove = false;
		GameObject[] trucks = GameObject.FindGameObjectsWithTag ("Truck");
		foreach (GameObject truck in trucks) {
			MeshRenderer[] renderers = truck.GetComponentsInChildren<MeshRenderer>();
			foreach (MeshRenderer r in renderers) {
				foreach (Material m in r.materials) {
					m.color = Color.red;
				}
			}
		}
	}
}