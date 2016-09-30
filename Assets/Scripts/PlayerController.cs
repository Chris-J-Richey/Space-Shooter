using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	private AudioSource audioSource;

	public float speed;
	public float tilt;
	public Boundary boundary;

	public GameObject shot;
	public GameObject shotBig;
	public Transform shotSpawn;
	public Transform shotSpawnRight;
	public Transform shotSpawnLeft;
	public float fireRate;
	private float nextFire;

	private GameController gameController;

	void Start(){
		rb = GetComponent<Rigidbody> ();
		audioSource = GetComponent<AudioSource> ();

		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		} 
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' Script");
		}
	}

	void Update(){
		if (Input.GetButton("Fire1") && Time.time > nextFire) {
			int score = 0;
			score = gameController.getScore ();
			nextFire = Time.time + fireRate;
			if (score >= 000 && score < 250) {
				Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			}
			if(score >= 250 && score < 500){
				Instantiate (shot, shotSpawnRight.position, shotSpawnRight.rotation);
				Instantiate (shot, shotSpawnLeft.position, shotSpawnLeft.rotation);
			}
			if(score >= 500 && score < 750){
				Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
				Instantiate (shot, shotSpawnRight.position, shotSpawnRight.rotation);
				Instantiate (shot, shotSpawnLeft.position, shotSpawnLeft.rotation);
			}
			if (score >= 750 && score < 1000) {
				Instantiate (shotBig, shotSpawn.position, shotSpawn.rotation);
			}
			if(score >= 1000 && score < 1500){
				Instantiate (shotBig, shotSpawnRight.position, shotSpawnRight.rotation);
				Instantiate (shotBig, shotSpawnLeft.position, shotSpawnLeft.rotation);
			}
			if(score >= 1500){
				Instantiate (shotBig, shotSpawn.position, shotSpawn.rotation);
				Instantiate (shotBig, shotSpawnRight.position, shotSpawnRight.rotation);
				Instantiate (shotBig, shotSpawnLeft.position, shotSpawnLeft.rotation);
			}
			audioSource.Play ();
		}
	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;

		rb.position = new Vector3
		(
			Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
			0.0f, 
			Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
		);

		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
	}
}
