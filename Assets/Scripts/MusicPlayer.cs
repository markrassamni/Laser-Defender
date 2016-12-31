using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;

	[SerializeField] private AudioClip startClip;
	[SerializeField] private AudioClip gameClip;
	[SerializeField] private AudioClip endClip;

	private AudioSource music;
	
	void Start () {
		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Duplicate music player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
			music = GetComponent<AudioSource>();
			music.clip = startClip;
			music.loop = true;
			music.Play();
		}
		
	}

	void OnEnable(){
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnDisable(){
		 SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
  		music.Stop();
		if (scene.name == "Start Menu"){
			music.clip = startClip;
		} else if (scene.name == "Game"){
			music.clip = gameClip;
		} else if (scene.name == "Win Screen"){
			music.clip = endClip;
		}
		music.loop = true;
		music.Play();
 	}

}
