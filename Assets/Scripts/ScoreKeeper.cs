using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {


	private AudioSource audioSource;
	private Text scoreLabel;
	private static int gameScore = 0;

	public static int GameScore {
		get {
			return gameScore;
		}
	}

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
		scoreLabel = GetComponent<Text>();
		Reset();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Score (int points){
		audioSource.PlayOneShot(audioSource.clip);
		gameScore += points;
		scoreLabel.text = gameScore.ToString();
	}

	public static void Reset(){
		gameScore = 0;
	}
}
