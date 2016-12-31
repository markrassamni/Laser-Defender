using UnityEngine;
using System.Collections;

public class EnemyShip : MonoBehaviour {


	[SerializeField] private GameObject enemyLaserPrefab;
	[SerializeField] private float shipHeight = 0.6f;
	[SerializeField] private float fireRate = 0.5f;

	[SerializeField] private int health = 100; 
	[SerializeField] private float firstShotDelay = 1f;


	private AudioSource audioSource;

	private int killPoints = 150;
	private bool readyToFire;

	public int Health {
		get{
			return health;
		}
	}

	void Start(){
		audioSource = GetComponent<AudioSource>();
		StartCoroutine(FirstShot());
	}

	void Update () {
		if (health <= 0){
			Destroy(gameObject);
			GameObject.Find("Score").GetComponent<ScoreKeeper>().Score(killPoints);
		}
		float fireProbability = Time.deltaTime * fireRate;
		if (fireProbability > Random.value && readyToFire){
			Fire();
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "PlayerLaser"){
			Laser laser = other.gameObject.GetComponent<Laser>();
			health -= laser.LaserDamage;
		}
	}

	void Fire(){
		Instantiate(enemyLaserPrefab, new Vector3 (transform.position.x, transform.position.y - shipHeight, transform.position.z), Quaternion.identity);
		audioSource.PlayOneShot(audioSource.clip);
	}

	IEnumerator FirstShot(){
		yield return new WaitForSeconds(firstShotDelay);
		readyToFire = true;
	}

}
