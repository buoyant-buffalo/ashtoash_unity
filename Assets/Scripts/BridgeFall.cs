using UnityEngine;
using System.Collections;

public class BridgeFall : MonoBehaviour {

    public GameObject[] bridgeArray;
	
	void OnTriggerEnter () 
    {
        for (int i=0; i < bridgeArray.Length; i++) {
            bridgeArray[i].GetComponent<Rigidbody> ().useGravity = true;
            bridgeArray[i].GetComponent<MeshCollider> ().enabled = true;
        }

        Destroy(gameObject);
	}
}
