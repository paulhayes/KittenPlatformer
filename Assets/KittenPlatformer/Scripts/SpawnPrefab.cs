using UnityEngine;
using System.Collections;

public class SpawnPrefab : MonoBehaviour {

    public GameObject prefab;
    public int numberToSpawn=1;

	public void Spawn(){
        if( numberToSpawn > 0 ){
            Instantiate(prefab,transform.position, transform.rotation);
            numberToSpawn--;
        }

    }
}
