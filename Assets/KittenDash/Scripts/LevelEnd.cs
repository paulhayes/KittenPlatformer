using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour {

	public string layerName;
	public float waitTime;
	public string levelName;
	public Text message;
	private bool loadingLevel = false;
	
	void OnTriggerEnter2D(Collider2D collider){
		string colliderLayerName = LayerMask.LayerToName( collider.gameObject.layer );
		if( colliderLayerName == layerName ){
			if( loadingLevel ) return;
			loadingLevel = true;
			SendMessage("Stop");
			StartCoroutine( LoadLevel() );
			
		}  
	}
	
	IEnumerator LoadLevel(){
		message.enabled = true;
		yield return new WaitForSeconds(waitTime);
		if( levelName.Length > 0 ) SceneManager.LoadScene(levelName);
	}
}
