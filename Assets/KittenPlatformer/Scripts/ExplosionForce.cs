using UnityEngine;
using System.Collections;

public class ExplosionForce : MonoBehaviour {

    public float radius=10;
    public float force=100;

    public bool playOnAwake;

    private Collider2D[] blastedColliders = new Collider2D[50];

	void Start () {
        if( playOnAwake ) {
            ApplyForce();
        }
	}

    public void ApplyForce(){
        if( radius <= 0 ){
            return;
        }
        Debug.Log("Exploding");
        int numObjects = Physics2D.OverlapCircleNonAlloc(transform.position, radius, blastedColliders) ;
        for( int i=0; i<numObjects; i++ ){
            Collider2D col = blastedColliders[i];
            if( col.attachedRigidbody != null ){
                Vector2 relPos = (col.transform.position - transform.position);
                Debug.Log(col.name);
                col.attachedRigidbody.AddForce( force*(relPos.magnitude/radius)*relPos.normalized, ForceMode2D.Impulse );
            }
        }
    }
}
