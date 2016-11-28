using UnityEngine;
using System.Collections;


public class MoneyHandler : MonoBehaviour {

    public int credits;
    public int truckCount;
   


    // Use this for initialization
    void Start () {
   }
	
	// Update is called once per frame
	void Update () {
        truckCount = GameObject.FindGameObjectsWithTag("Truck").Length;
        truckCount = (truckCount / 12);

    }


    //void changeTruckColor(Color color)
    //{
    //    GameObject activeTruck = GameObject.Find("Truck" + idNum);

    //    MeshRenderer[] renderers = activeTruck.GetComponentsInChildren<MeshRenderer>();
    //    foreach (MeshRenderer r in renderers)
    //    {
    //        foreach (Material m in r.materials)
    //        {
    //            m.color = color;
    //        }
    //    }
    //}



    void OnGUI()
    {
        GUI.Box(new Rect((Screen.width / 5) - 70, Screen.height - 30, 140, 25), "Credits: =" + credits);
        GUI.Box(new Rect((Screen.width / 2) + 90, Screen.height - 30, 140, 25), "TruckCount: =" + truckCount);
    }
}
