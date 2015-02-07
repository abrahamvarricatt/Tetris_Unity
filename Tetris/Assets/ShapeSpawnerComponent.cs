using UnityEngine;
using System.Collections;

public class ShapeSpawnerComponent : MonoBehaviour {

	public GameObject[] pieces = new GameObject[5];


	// Use this for initialization
	void Start () {
	  	ShapeComponent.maxDistance = (int)GameObject.Find ("Main Camera").GetComponent<Camera>().orthographicSize - 3;
		StartCoroutine ("spwanPiece");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void spwanPiece(){
		new WaitForSeconds (2.0f);

		int select = Random.Range (0, pieces.Length-1);

		Debug.Log ("Spwaning piece : " + pieces [select].name);
		Instantiate (pieces [select], transform.position, Quaternion.identity);
	}

	void pieceStoppedFalling(){
		StartCoroutine ("spwanPiece");
	}
}
