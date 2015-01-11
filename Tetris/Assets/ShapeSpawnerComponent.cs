using UnityEngine;
using System.Collections;

public class ShapeSpawnerComponent : MonoBehaviour {

	public GameObject[] pieces = new GameObject[5];

	// Use this for initialization
	void Start () {
		spwanPiece ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void spwanPiece(){
		int select = Random.Range (0, 3);

		Debug.Log ("Spwaning piece : " + pieces [select].name);
		Instantiate (pieces [select], transform.position, Quaternion.identity);
	}
}
