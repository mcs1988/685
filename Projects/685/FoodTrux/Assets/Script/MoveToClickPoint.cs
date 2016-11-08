// MoveToClickPoint.cs
using UnityEngine;

public class MoveToClickPoint : MonoBehaviour
{
    public Transform destination;
    public bool canMove = true;
	public bool isMoving = false;
	public bool isSelected = false;

    private UnityEngine.AI.NavMeshAgent agent;
    private int layerMask;

    private Transform myTransform; //this transform
    private Vector3 destinationPosition;   //destination position
    private float destinationDistance;     //distance to destination

    public float moveSpeed = 25.0f;    // controls character move speed
    public float rotateSpeed = 250f;   // speed at which character will rotate
    public float stopDistance = 3f; // stop moving when closer to stopDistance

    int creditTimer = 0; //Temp Variable to Simulate Income


    private Quaternion targetRotation;

    void Start()
    {
        myTransform = transform;   // cache transform to improve performance
        destinationPosition = myTransform.position; // initialize destinationPosition
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        changeTruckColor(Color.green);

        layerMask = ~(1 << 8);
    }

    void Update()
    {
		if (canMove && isSelected)
        {
            if (Input.GetMouseButtonDown(0))
            {

                Ray screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;
                if (Physics.Raycast(screenRay, out hit, Mathf.Infinity))
                {
                    if (!hit.collider.tag.Equals("Truck"))
                    {
                        isMoving = true;
                        changeTruckColor(Color.yellow);
                        agent.destination = hit.point;
                    }
                }
            }

            if (agent.nextPosition == agent.destination && agent.velocity == new Vector3(0, 0, 0))
            {
                isMoving = false;
                changeTruckColor(Color.green);
            }
        }
        else
        {
            //robs kustom editz
            GameObject[] customers = GameObject.FindGameObjectsWithTag("Customer");
            GameObject[] deployedTruck = GameObject.FindGameObjectsWithTag("Truck");
            foreach (GameObject truck in deployedTruck)
            {
                foreach (GameObject target in customers)
                {
                    float distance = Vector3.Distance(truck.transform.position, target.transform.position);
                    if (distance < 40)
                    {
                        creditTimer += 1;
                    }
                    if (creditTimer == 1000)
                    {
                        GameObject.Find("homeBase").GetComponent<MoneyHandler>().credits += 1;
                        creditTimer = 0;
                    }
                }
            }
            //canMove = false; Reason for this?
        }
        //end rob kustom kode

		if (Input.GetKeyDown(KeyCode.D) && !isMoving && isSelected)
        {
            canMove = !canMove;
            changeTruckColor(Color.red);
        }
		else if (Input.GetKeyDown(KeyCode.D) && !canMove && isSelected)
        {
            canMove = !canMove;
            changeTruckColor(Color.green);
        }
    }

	void OnMouseUpAsButton() {
		isSelected = !isSelected;
		truckIsSelected ();
	}

    //	void Update () {
    //		if (canMove) {
    //			// will move player to clicked point
    //			if (Input.GetMouseButtonDown (0) && GUIUtility.hotControl == 0) {
    //				// if some point clicked...
    //				Plane playerPlane = new Plane (Vector3.up, myTransform.position);
    //				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
    //				RaycastHit hitdist;
    //				int layerMask = 1 << 8;
    //				layerMask = ~layerMask;
    //				Ray screenRay = Camera.main.ScreenPointToRay (Input.mousePosition);
    //				RaycastHit hit;
    //				if (Physics.Raycast (screenRay, out hitdist, Mathf.Infinity, layerMask)) {
    //					// set destination to the point clicked
    //					destinationPosition = hitdist.point;
    //					//agent.destination = hitdist.point;
    //				}//end nested if
    //				Debug.Log ("CLICKED!");
    //			}//end if
    //			// calculate the current target direction
    //			Vector3 destDir = destinationPosition - myTransform.position;
    //			Debug.Log ("dest dir = " + destDir + "    dest pos = " + destinationPosition + "     trans pos = " + myTransform.position);
    //			destDir.y = 0; // make it strictly horizontal to avoid object tilting
    //			destinationDistance = destDir.magnitude; // get the horizontal distance
    //			// object doesn't anything if below stopDistance:
    //			if (destinationDistance >= stopDistance) { // if farther than stopDistance...
    //				isMoving = true;
    //				changeTruckColor(Color.yellow);
    //				targetRotation = Quaternion.LookRotation (destDir); // update target rotation...
    //				// turn gradually to target direction each frame:
    //				myTransform.rotation = Quaternion.RotateTowards (myTransform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    //				// move in its local forward direction (Translate default):
    //				myTransform.Translate (Vector3.forward * 25f * Time.deltaTime);  // move in forward direction 
    //			}//end if
    //			else{
    //				isMoving = false;
    //				changeTruckColor(Color.green);
    //			}
    //		}
    //		
    //		if (Input.GetKeyDown (KeyCode.D) && !isMoving) {
    //			canMove = !canMove;
    //			changeTruckColor (Color.red);
    //		} else if (Input.GetKeyDown (KeyCode.D) && !canMove) {
    //			canMove = !canMove;
    //			changeTruckColor (Color.green);
    //		}
    //	}//end update

	void changeTruckColor(Color color)
	{
		GameObject[] trucks = GameObject.FindGameObjectsWithTag("Truck");
		foreach (GameObject truck in trucks)
		{
			MeshRenderer[] renderers = truck.GetComponentsInChildren<MeshRenderer>();
			foreach (MeshRenderer r in renderers)
			{
				foreach (Material m in r.materials)
				{
					m.color = color;
				}
			}
		}
	}

	void truckIsSelected()
	{
		GameObject[] trucks = GameObject.FindGameObjectsWithTag("Truck");
		foreach (GameObject truck in trucks)
		{
			MeshRenderer[] renderers = truck.GetComponentsInChildren<MeshRenderer>();
			foreach (MeshRenderer r in renderers)
			{
				foreach (Material m in r.materials)
				{
					Color color = m.GetColor ("_OutlineColor");
					color.a = isSelected ? 255.0f : 0.0f;
					m.SetColor ("_OutlineColor", color);

				}
			}
		}
	}
}