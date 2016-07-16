using UnityEngine;
using System.Collections;
using System;

public class PlayerFireHook : MonoBehaviour {

    [Serializable]
    public class GrappleSettings
    {
        public GameObject hookShot;
        public Transform firePoint;
        public float projectileSpeed = 20f;
        public float reloadTime = 0.2f;
    }

    public GrappleSettings grappleSettings = new GrappleSettings();
    private float lastFireTime;
	
	
	void Update () 
    {

        if (Input.GetButtonDown ("Fire1") && Time.time > lastFireTime + grappleSettings.reloadTime) 
        {
            fireHook ();
            lastFireTime = Time.time;
        }
	}

    private void fireHook () 
    {
        GameObject hook = (GameObject)Instantiate (grappleSettings.hookShot, grappleSettings.firePoint.position, grappleSettings.firePoint.rotation);
        Physics.IgnoreCollision (GetComponent <Collider>(), hook.GetComponent <Collider>());
        hook.GetComponent <Rigidbody>().velocity = Camera.main.transform.forward * grappleSettings.projectileSpeed;
    }
}
