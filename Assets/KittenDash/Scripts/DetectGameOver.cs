﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DetectGameOver : MonoBehaviour {

	public float gameOverHeight;
	
	bool gameOver = false;
    string gameOverLayer = "GameOver";

	void Update(){
		if( transform.position.y < gameOverHeight ){
			SendMessage("OnGameOver");
		}
   }
           
   void OnTriggerEnter2D(Collider2D collider){
		
		/*
		 * Advanced users should user enemyLayer && 1<<collider.gameObject.layer to see if a layer is in a list of layers
		 * but this is for beginners so it's easy just to understand comparing names
		 */
		
		string colliderLayerName = LayerMask.LayerToName( collider.gameObject.layer );
		if( colliderLayerName  == gameOverLayer ){
			SendMessage("OnGameOver");
		}  
	}
	
	void OnCollisionEnter2D(Collision2D collision){
		
		string colliderLayerName = LayerMask.LayerToName( collision.collider.gameObject.layer );
		if( colliderLayerName  == gameOverLayer ){
			SendMessage("OnGameOver");
		}
		
	}

    public void GameOver(){
        StartCoroutine(OnGameOver());
    }
	
	IEnumerator OnGameOver(){
		if( gameOver ) yield break;
	
		gameOver = true;
		GetComponent<Animator>().SetTrigger("GameOver");
		GetComponent<Rigidbody2D>().velocity = Vector2.up * 5f;
		Destroy( GetComponent<Collider2D>() );
		
		yield return new WaitForSeconds(2f);
		
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		Destroy( this );
		
	}
	
}
