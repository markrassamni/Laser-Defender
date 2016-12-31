using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

	[SerializeField] private float laserSpeed = 500f;
	[SerializeField] private int laserDamage = 25;

	public int LaserDamage{
		get{
			return laserDamage;
		}
	}

	private Rigidbody2D laserRigidBody;

	void Start(){
		laserRigidBody = GetComponent<Rigidbody2D>();
	}	

	void Update(){
		laserRigidBody.velocity = Vector2.up * laserSpeed * Time.deltaTime;
	}


	void OnTriggerEnter2D(Collider2D other){
		
		if (other.tag == "DestroyLasers" || other.tag == "EnemyShip"){
			Destroy(gameObject);
		}
	}
}
