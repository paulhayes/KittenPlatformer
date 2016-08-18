using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    public bool destroyOnExplode;
    public GameObject explodePrefab;

    void Awake(){
    }

    public void Explode(){
        Instantiate( explodePrefab, transform.position, transform.rotation );


        if( destroyOnExplode ){
            Destroy( gameObject );
        }
    }


}
