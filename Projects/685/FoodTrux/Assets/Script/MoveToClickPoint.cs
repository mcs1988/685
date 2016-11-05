// MoveToClickPoint.cs
using UnityEngine;

public class MoveToClickPoint : MonoBehaviour {
	public Transform destination;
	public bool canMove = true;

	private NavMeshAgent agent;

	private Transform myTransform; //this transform
	private Vector3 destinationPosition;   //destination position
	private float destinationDistance;     //distance to destination

	public float moveSpeed = 25.0f;    // controls character move speed
	public float rotateSpeed = 90f;   // speed at which character will rotate
	public float stopDistance = 10f; // stop moving when closer to stopDistance

	private Quaternion targetRotation;

	void Start () 
	{
		myTransform = transform;   // cache transform to improve performance
		destinationPosition = myTransform.position; // initialize destinationPosition
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

//	void Update()
//	{
//		if (canMove) {
//			if (Input.GetMouseButtonDown (0)) {
//				int layerMask = 1 << 8;
//				layerMask = ~layerMask;
//
//				Ray screenRay = Camera.main.ScreenPointToRay (Input.mousePosition);
//
//				RaycastHit hit;
//
//				if (Physics.Raycast (screenRay, out hit, Mathf.Infinity, layerMask)) {
//					agent.destination = hit.point;
//				}
//			}
//
//			if (Input.GetKeyDown (KeyCode.D)) {
//				DeployTruck ();
//			}
//		}
//	}

	void Update () {
		// will move player to clicked point
		if (Input.GetMouseButtonDown(0)&& GUIUtility.hotControl == 0) {
			// if some point clicked...
			Plane playerPlane = new Plane(Vector3.up, myTransform.position);
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			float hitdist = 0.0f;
			if (playerPlane.Raycast(ray, out hitdist)) {
				// set destination to the point clicked
				destinationPosition = ray.GetPoint(hitdist);
			}//end nested if
			Debug.Log ("CLICKED!");
		}//end if
		// calculate the current target direction
		Vector3 destDir = destinationPosition - myTransform.position;
		destDir.y = 0; // make it strictly horizontal to avoid object tilting
		destinationDistance = destDir.magnitude; // get the horizontal distance
		// object doesn't anything if below stopDistance:
		if (destinationDistance >= stopDistance){ // if farther than stopDistance...
			targetRotation = Quaternion.LookRotation(destDir); // update target rotation...
			// turn gradually to target direction each frame:
			myTransform.rotation = Quaternion.RotateTowards(myTransform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
			// move in its local forward direction (Translate default):
			myTransform.Translate(Vector3.forward * 10f * Time.deltaTime);  // move in forward direction 
		}//end if

		if (Input.GetKeyDown (KeyCode.D)) {
			DeployTruck ();
		}
	}//end update

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