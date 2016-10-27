// MoveToClickPoint.cs
using UnityEngine;

public class MoveToClickPoint : MonoBehaviour {
	public Transform destination;

	private NavMeshAgent agent;

	void Start () 
	{
		agent = gameObject.GetComponent<NavMeshAgent>();
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			int layerMask = 1 << 8;
			layerMask = ~layerMask;

			Ray screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);

			RaycastHit hit;

			if (Physics.Raycast(screenRay, out hit, Mathf.Infinity, layerMask)) {
				agent.destination = hit.point;
			}
		}
	}
}