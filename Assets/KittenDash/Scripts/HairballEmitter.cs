using UnityEngine;
using System.Collections;

public class HairballEmitter : MonoBehaviour {

	public GameObject hairballPrefab;
	public Transform spawnPosition;
	public Vector2 force;

	void Start () {
		
	}
	
	void Update () {
		if( Input.GetButtonDown("Fire1") ){
			GameObject hairball = Instantiate<GameObject>(hairballPrefab);
			hairball.transform.position = spawnPosition.position;	
			Rigidbody2D body = hairball.GetComponent<Rigidbody2D>();
			body.AddForce( force, ForceMode2D.Impulse );
		}

	}
}
