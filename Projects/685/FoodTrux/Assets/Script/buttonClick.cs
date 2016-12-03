using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class buttonClick : MonoBehaviour {
	public Button yourButton;
	//public Vector3 spawnPoint = Vector3.zero;

	//Vector3 spawnPosition = new Vector3 (285.5f, 0f, 440.4f);
    //Sort of in Garage ^^
    Vector3 spawnPosition = new Vector3(278f, 0f, 430f);
    private UnityEngine.NavMeshAgent agent;
    public int truckNumID;
    private GameObject spawnDest;



    void Start () {
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);

        spawnDest = GameObject.FindWithTag("Player");
        //position = transform.position.Set(514.5f,0f,402.4f);
    }

	void TaskOnClick()
    {
		Debug.Log ("You have clicked the button!");



        truckNumID = GameObject.Find("homeBase").GetComponent<MoneyHandler>().truckCount;
        truckNumID += 1;

        

        GameObject truckToCopy= GameObject.Find("Truck1");


        GameObject spawnedTruck = Instantiate(truckToCopy, spawnPosition, transform.rotation) as GameObject;
        spawnedTruck.transform.Rotate(180,0,0);  //really sloppy solution


        spawnedTruck.name = "Truck" + truckNumID;

        
        spawnedTruck.GetComponent<MoveToClickPoint>().idNum = truckNumID; //gives truck new fresh ID #
        spawnedTruck.GetComponent<MoveToClickPoint>().isSelected = false;  //sets spawned truck to not selected
        spawnedTruck.GetComponent<MoveToClickPoint>().isDeployed = false; //sets spawned truck to not deployed
        spawnedTruck.GetComponent<MoveToClickPoint>().inGarage = true; //sets spawned truck to not deployed
        //should set to in garage once that is working right




        string truckName = spawnedTruck.name;

        Debug.Log("Just Spawned Truck " + truckNumID);
        Debug.Log("Truck's Name: " + truckName);

        // GameObject.Find("Truck3").GetComponent<MoveToClickPoint>().inGarage = true;



        //*
        //Attempt to Rip from Leave Garage

        //GameObject dest = GameObject.FindGameObjectWithTag("LeaveGarageDestination");
        //agent.destination = dest.transform.position;

        //*

        //Object.Instantiate(truckToCopy, position, Quaternion.identity);
    }

    //void LeaveGarage()
    //{
    //    isMoving = true;
    //    canMove = false;
    //    GameObject dest = GameObject.FindGameObjectWithTag("LeaveGarageDestination");
    //    agent.destination = dest.transform.position;
    //}
}
