﻿using UnityEngine;
using System.Collections;

public class SaveSpot : MonoBehaviour {

	public static Vector3 position = Vector3.zero;
    
	void Start(){
		if( position != Vector3.zero ){
			transform.position = position;			
		}
	}

	void OnTriggerEnter2D(Collider2D collider){ 
        if(collider.gameObject.CompareTag( "Respawn" ) ){
			Debug.Log("Respawn point "+collider.transform.position);
			position = collider.transform.position;
		}
	}
}
