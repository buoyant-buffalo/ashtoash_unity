using UnityEngine;
using System.Collections;

public class PlayerAction : MonoBehaviour {

	public GameObject hookShot;
	public Transform firePoint;
	public float projectileSpeed = 20f;

	public static bool hookBlocked = false;

	void Start () {}

	void Update () 
	{
		if (Input.GetButtonDown ("Fire1")) {
			fireHook ();
		}
	}

	void fireHook () 
	{
		GameObject hook = (GameObject)Instantiate (hookShot, firePoint.position, firePoint.rotation);
		Physics.IgnoreCollision (GetComponent <Collider>(), hook.GetComponent <Collider>());
		hook.GetComponent <Rigidbody>().velocity = Camera.main.transform.forward * projectileSpeed;
	}
}
