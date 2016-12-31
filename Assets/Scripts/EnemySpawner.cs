using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {


	[SerializeField] private GameObject enemyPrefab;
	[SerializeField] private float width = 15.98f;
	[SerializeField] private float height = 2.81f;
	[SerializeField] private float moveSpeed = 10f;
	[SerializeField] private float padding = 0.7f;
	[SerializeField] private float spawnDelay = .5f;

	private float xMin;
	private float xMax; 
	private bool movingRight;

	void Start () {
		float distanceToScreen = Camera.main.transform.position.z - transform.position.z;
		float leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distanceToScreen)).x;
		float rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distanceToScreen)).x;
		xMax = rightMost - padding;
		xMin = leftMost + padding;
		StartCoroutine(SpawnUntilFull());
	}
	void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height,0));
	}
	

	void Update () {
		if (movingRight){
			transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
		} else {
			transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
		}
		if (transform.position.x - width/2 < xMin){
			movingRight = true;
		} else if (transform.position.x + width/2 >xMax){
			movingRight = false;
		}

		if (AllMembersAreDead()){
			StartCoroutine(SpawnUntilFull());
		}
	}

	IEnumerator SpawnUntilFull(){
		Transform nextSpawnPosition = NextFreePosition();
		if (nextSpawnPosition != null){
			GameObject enemy = Instantiate(enemyPrefab, nextSpawnPosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = nextSpawnPosition;
			yield return new WaitForSeconds(spawnDelay);
			StartCoroutine(SpawnUntilFull());
		}
	}

	Transform NextFreePosition(){
		foreach (Transform childPositionGameObject in transform){
			if (childPositionGameObject.childCount == 0){
				return childPositionGameObject;
			} 
		}
		return null;
	}

	bool AllMembersAreDead(){
		foreach (Transform childPositionGameObject in transform){
			if (childPositionGameObject.childCount > 0){
				return false;
			} 
		}
		return true;
	}
	 
}
