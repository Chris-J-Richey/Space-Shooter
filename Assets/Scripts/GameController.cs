using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;
	public GUIText waveText;

	private bool gameOver;
	private bool restart;
	private bool wave;
	private int score;
	private int waveNum;
	private float speed;


	void Start () {
		gameOver = false;
		restart = false;
		wave = false;
		gameOverText.text = "";
		restartText.text = "";
		waveText.text = "";
		score = 0;
		waveNum = 1;
		speed = 1;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
		for (int i = 0; i <= 0; i++) {
			speed *= 1.1f;
		}
	}

	void Update(){
		if (restart) {
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel (Application.loadedLevel);
			}
		}
		if (!wave) {
			waveText.text = "Wave " + waveNum;
		} else {
			waveText.text = "";
		}
	}

	IEnumerator SpawnWaves () {
		yield return new WaitForSeconds (startWait);
		while(true){
			for (int i = 0; i <= 0; i++) {
				speed *= 1.05f;
			}
			wave = true;
			waveNum++;
			for (int i = 0; i < hazardCount; i++){
				Vector3 spawnPosition = new Vector3 (Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion SpawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, SpawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			wave = false;

			for (int i = 0; i <= 0; i++) {
				speed *= 1.1f;
			}
			yield return new WaitForSeconds (waveWait);

			if (gameOver){
				restartText.text = "Press 'R' for restart";
					restart = true;
					break;
			}
		}
	}

	public void AddScore (int newScoreValue){
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore(){
		scoreText.text = "Score: " + score;
	}

	public int getScore(){
		return score;
	}

	public float getSpeed(){
		return speed;
	}

	public void GameOver(){
		gameOverText.text = "GameOver!";
		gameOver = true;
	}
}
