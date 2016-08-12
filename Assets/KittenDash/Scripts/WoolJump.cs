using UnityEngine;
using System.Collections;

public class WoolJump : MonoBehaviour {

    public Rigidbody2D body;
    public float force;
    public KeyCode key;
    public bool something;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if( Input.GetKeyDown(key) ){
            body.AddForce( Vector2.up * force );
        }
	}

    IEnumerator ThingToDo(){

        //do this

        yield return new WaitForSeconds(1);

        yield return new WaitWhile(()=>something);


    }
}
