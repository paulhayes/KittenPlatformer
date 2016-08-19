using UnityEngine;
using System.Collections;

public class MoveBlock : MonoBehaviour {

    public Transform target1;
    public Transform target2;
    public bool headTowardsTarget1;
    public Rigidbody2D body;
    public float speed;


	void Start () {
	    body.isKinematic = true;
	}
	
	void FixedUpdate () {
        Transform target = headTowardsTarget1 ? target1 : target2;

	    body.velocity = ( target.position - transform.position ).normalized * speed;

        if( ( target.position - transform.position ).magnitude < 0.1f ){
            headTowardsTarget1 = !headTowardsTarget1;
        } 

	}
}
