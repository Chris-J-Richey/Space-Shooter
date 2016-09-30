using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	private Rigidbody rb;

	public float speed;
	public float x;
	public float z;

	private bool change;

	private GameController gameController;

	void Start () {
		change = true;
		rb = GetComponent<Rigidbody> ();

		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		} 
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' Script");
		}
		if (speed < 0){
			speed *= gameController.getSpeed ();
		}

		if (x == 9.9f) {
			x = Random.Range (-0.5f, 0.5f);
		}
		Vector3 movement = new Vector3 (x, 0.0f, z);
		rb.velocity = movement * speed;
	}
}
