using UnityEngine;
using System.Collections;

public class ShapeComponent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		InvokeRepeating ("fallOneStep", 1.0f, 1.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void fallOneStep(){
		GetComponent<Transform> ().position += Vector3.down;
	}
}
