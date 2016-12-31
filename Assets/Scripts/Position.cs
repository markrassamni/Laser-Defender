using UnityEngine;
using System.Collections;

public class Position : MonoBehaviour {

	[SerializeField] private float radius = .5f;
	void OnDrawGizmos(){
		Gizmos.DrawWireSphere(transform.position, radius);
	}


}
