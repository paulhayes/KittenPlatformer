using UnityEngine;
using System.Collections;

public class DestroyOnCollision : MonoBehaviour {

	string destroyLayer;

	void OnCollisionEnter2D(Collision2D collision){
		
		string colliderLayerName = LayerMask.LayerToName( collision.collider.gameObject.layer );
		if( colliderLayerName  == destroyLayer ){
			Destroy (gameObject);
		}
		
	}
	
	void OnTriggerEnter2D(Collider2D collider){
		string colliderLayerName = LayerMask.LayerToName( collider.gameObject.layer );
		if( colliderLayerName  == destroyLayer ){
			Destroy (gameObject);
		}
	}
}
