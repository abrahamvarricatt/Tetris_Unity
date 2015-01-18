using UnityEngine;
using System.Collections;

public class ShapeComponent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		InvokeRepeating ("fallOneStep", 1.0f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		GameObject[] blocks = GameObject.FindGameObjectsWithTag("Falling");
		if (blocks.Length > 0 && blocks [0].transform.parent.gameObject == gameObject) {
			if (Input.GetKeyUp (KeyCode.LeftArrow)) {
				sideStep (true);
			} else if (Input.GetKeyUp (KeyCode.RightArrow)) {
				sideStep (false);
			} else if (Input.GetKeyUp (KeyCode.UpArrow)) {
				rotate();
			} else if (Input.GetKeyUp (KeyCode.DownArrow)) {
				Invoke("fallOneStep", 0.0f);
			}
		}
	}

	void sideStep(bool isLeft){
		GameObject[] blocks = GameObject.FindGameObjectsWithTag("Falling");
		bool canSideStep = true;

		foreach (GameObject block in blocks) {
			Ray ray = new Ray(block.GetComponent<Transform>().position, isLeft ? Vector3.left : Vector3.right);
			RaycastHit hit;	
			
			if (Physics.Raycast(ray,out hit, 1.0f) ) {				
				if( hit.collider.gameObject.tag.Equals("Floor") || hit.collider.gameObject.tag.Equals("Piece")){
					canSideStep = false;
					break;
				}
			}
		}
		
		if (canSideStep) {
			GetComponent<Transform> ().position += isLeft ? Vector3.left : Vector3.right;
		} 
	}

	void rotate(){
		Transform trans = GetComponent<Transform> ();
		trans.RotateAround (trans.position, Vector3.forward, 90.0f);

		bool canbeRotated = true;

		GameObject[] blocks = GameObject.FindGameObjectsWithTag("Falling");
		foreach (GameObject block in blocks) {
			Ray ray = new Ray(block.GetComponent<Transform>().position, Vector3.down);
			RaycastHit hit;	
			
			if (Physics.Raycast(ray,out hit, 0.9f) ) {				
				if( hit.collider.gameObject.tag.Equals("Floor") || hit.collider.gameObject.tag.Equals("Piece")){
					canbeRotated = false;
					break;
				}
			}
		}

		if (!canbeRotated) {
			trans.RotateAround (trans.position, Vector3.forward, -90.0f);
		}
	}

	void fallOneStep(){
		GameObject[] blocks = GameObject.FindGameObjectsWithTag("Falling");
		bool shouldStopFalling = false;

		foreach (GameObject block in blocks) {
			Ray ray = new Ray(block.GetComponent<Transform>().position, Vector3.down);
			RaycastHit hit;	

			if (Physics.Raycast(ray,out hit, 1.0f) ) {				
				if( hit.collider.gameObject.tag.Equals("Floor") || hit.collider.gameObject.tag.Equals("Piece")){
					CancelInvoke("fallOneStep");
					shouldStopFalling = true;
					break;
				}
			}
		}

		if (!shouldStopFalling) {
			GetComponent<Transform> ().position += Vector3.down;
		} else {
			foreach (GameObject block in blocks) {
				block.tag = "Piece";
				block.GetComponent<Collider>().isTrigger = true;
			}
			GameObject.Find("Shape Spawner").SendMessage("pieceStoppedFalling");
		}
	}

}
