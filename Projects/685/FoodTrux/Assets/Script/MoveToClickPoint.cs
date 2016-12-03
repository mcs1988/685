// MoveToClickPoint.cs
using UnityEngine;

public class MoveToClickPoint : MonoBehaviour
{
    //public Transform destination;
	public Canvas canvas;
    public bool canMove = true;
	public bool isMoving = false;
	public bool isSelected = false;
	public bool inGarage = true;

    private UnityEngine.NavMeshAgent agent;

    private Transform myTransform; //this transform
    //private Vector3 destinationPosition;   //destination position
    private float destinationDistance;     //distance to destination

    public float moveSpeed = 25.0f;    // controls character move speed
    public float rotateSpeed = 250f;   // speed at which character will rotate
    public float stopDistance = 3f; // stop moving when closer to stopDistance

    int creditTimer; //Temp Variable to Simulate Income
	public int incomeRate; //Rate at which credits increase (smaller is faster)
	public int attractionRadius; //What range customers will be attracted to your Truck

	public int idNum; //truck ID #

	public bool isDeployed = false; // is it deployed?


    //private Quaternion targetRotation;

        void Awake()
    {
        Debug.Log("Awake Happened.");
    }

    void Start()
    {

        Debug.Log("Start Happened.");

        //myTransform = transform;   // cache transform to improve performance
        //destinationPosition = myTransform.position; // initialize destinationPosition
        agent = gameObject.GetComponent<UnityEngine.NavMeshAgent>();
        changeTruckColor(Color.yellow);
		canvas.enabled = false;

       
        isSelected = false;
        //isDeployed = false; 
        

        if (inGarage)
		LeaveGarage ();

		creditTimer = 0;

        //
        //
        //

        GameObject selected = GameObject.Find("Truck" + idNum);
        MeshRenderer[] renderers = selected.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer r in renderers)
        {
            foreach (Material m in r.materials)
            {
                Color color = m.GetColor("_OutlineColor");
                color.a = isSelected ? 255.0f : 0.0f;
                m.SetColor("_OutlineColor", color);
            }
        }

        //
        //
        //
    }

    void Update()
	{
		
		if (agent.nextPosition == agent.destination && agent.velocity == new Vector3(0, 0, 0))
		{
			isMoving = false;
			if (canMove)
				changeTruckColor (Color.green);
			else if (inGarage) {
				canMove = true;
				changeTruckColor (Color.green);
				inGarage = false;
			}
		}

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
						if(hit.collider.tag.Equals("Respawn")){
							
						}
						else{
                        	agent.destination = hit.point;
						}
                    }
                }
            }
        }

		if (Input.GetKeyDown(KeyCode.D) && isSelected && !isDeployed && !isMoving)
		{
			canMove = false;
			isDeployed = true;

			//Debug.Log (isDeployed + "Truck" + idNum + "has been deployed");
		}
		else if (Input.GetKeyDown(KeyCode.D) && !isMoving && !canMove && isSelected)
		{
			
			canMove = true;
			isDeployed = false;
			changeTruckColor(Color.green);

			//Debug.Log (isDeployed + "Truck" + idNum + "has been undeployed");
		}

		//robs kustom editz
		//if truck is deployed, then income increases for each customer in vicinity @ 1/incomeRate.
		//incomeRate is how many frames equal a tick on an arbitrary income timer

		if(isDeployed)
		{
			changeTruckColor(Color.red);
            GameObject[] customers = GameObject.FindGameObjectsWithTag("Customer");
            //GameObject[] deployedTruck = GameObject.FindGameObjectsWithTag("Truck");
			GameObject deployedTruck = GameObject.Find("Truck"+idNum);

			canMove = false; //Reason for this?

			//Debug.Log(deployedTruck + "Deployed: " + isDeployed);

			foreach (GameObject target in customers)
                {
				
				float distance = Vector3.Distance(deployedTruck.transform.position, target.transform.position);
                    if (distance < attractionRadius)
                    {
                        creditTimer += 1;
                    }
                    if (creditTimer == incomeRate)
                    {
                        GameObject.Find("homeBase").GetComponent<MoneyHandler>().credits += 1;
					Debug.Log ("Credit Added from Truck " + idNum);
                        creditTimer = 0;
                    }
                }
            }
//        //end rob kustom kode
    }

	void OnMouseUpAsButton() {
		isSelected = !isSelected;
		truckIsSelected ();
		//gameObject.tag = "Unselected";
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
		GameObject activeTruck = GameObject.Find("Truck"+idNum);

			MeshRenderer[] renderers = activeTruck.GetComponentsInChildren<MeshRenderer>();
			foreach (MeshRenderer r in renderers)
			{
				foreach (Material m in r.materials)
				{
					m.color = color;
				}
		}
	}

	void truckIsSelected()
	{
		canvas.enabled = !canvas.enabled;

  
            GameObject selected = GameObject.Find("Truck" + idNum);

        Debug.Log(selected.name + "has been selected.");

            MeshRenderer[] renderers = selected.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer r in renderers)
            {
                foreach (Material m in r.materials)
                {
                    Color color = m.GetColor("_OutlineColor");
                    color.a = isSelected ? 255.0f : 0.0f;
                    m.SetColor("_OutlineColor", color);
                }
            }
        
	}

	void LeaveGarage()
	{
		isMoving = true;
		canMove = false;
		GameObject dest = GameObject.FindGameObjectWithTag ("LeaveGarageDestination");
		agent.destination = dest.transform.position;
	}
}