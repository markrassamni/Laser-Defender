using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {


	[SerializeField] private float moveSpeed = 15f;
	[SerializeField] private float padding = 0.7f;
	[SerializeField] private GameObject laserPrefab;
	[SerializeField] private float shipHeight = 0.6f;
	[SerializeField] private float fireRate = 0.2f;
	[SerializeField] private int health = 100; 

	private AudioSource audioSource;


	private float xMin;
	private float xMax;  
	private bool shooting;

	public int Health {
		get{
			return health;
		}
	}
	
	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
		float distanceZ = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceZ));
		Vector3 rightMost =  Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceZ));
		xMin = leftMost.x + padding;
		xMax = rightMost.x - padding;
		StartCoroutine(Fire());
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 moveDirection = new Vector2(Input.GetAxis("Horizontal"), 0);
		transform.Translate(moveSpeed * moveDirection * Time.deltaTime);
		float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);

		if (Input.GetKey("space")){
			shooting = true;
		} else {
			shooting = false;
		}

		if (health <= 0){
			LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
			man.LoadLevel("Win Screen");
			Destroy(gameObject);
		}
	}

	IEnumerator Fire(){
		if (shooting){
			Instantiate(laserPrefab, new Vector3 (transform.position.x, transform.position.y + shipHeight, transform.position.z), Quaternion.identity);
			audioSource.PlayOneShot(audioSource.clip);
			yield return new WaitForSeconds(fireRate);
			StartCoroutine(Fire());
		} else {
			yield return new WaitForSeconds(.00001f);
			StartCoroutine(Fire());
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "EnemyLaser"){
			EnemyLaser enemyLaser = other.gameObject.GetComponent<EnemyLaser>();
			health -= enemyLaser.LaserDamage;
		}
	}

}
