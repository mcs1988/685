using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EditorPathScript : MonoBehaviour {

	public Color rayColor = Color.white;
	public List<Transform> path_objs = new List<Transform> ();
	Transform[] pointArray;

	void OnDrawGizmos () {
		Gizmos.color = rayColor;
		pointArray = GetComponentsInChildren<Transform> ();
		path_objs.Clear ();

		foreach (Transform path_obj in pointArray) {
			if (path_obj != this.transform) {
				path_objs.Add (path_obj);
			}
		}

		for (int i = 0; i < path_objs.Count; i++) {
			Vector3 position = path_objs [i].position;
			if (i > 0) {
				Vector3 previous = path_objs [i - 1].position;
				Gizmos.DrawLine (previous, position);
				Gizmos.DrawWireSphere (position, 3.0f);
			}
		}
	}
}
