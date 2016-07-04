using UnityEngine;
using System.Collections;

public class HookShot : MonoBehaviour {

	public float lifetime = 1f;
	public float strength = 100f;

	private bool anchored = false;
	private Rigidbody hookRb;
	private Rigidbody playerRb;
	private PlayerController playerController;

	void Start () 
	{
		hookRb = GetComponent <Rigidbody> ();
		playerRb = GameObject.FindGameObjectWithTag ("Player").GetComponent <Rigidbody> ();
		playerController = playerRb.GetComponent <PlayerController> ();

		hookRb.collisionDetectionMode = CollisionDetectionMode.Continuous;

		Invoke("Expire", lifetime);
	}
		
	void OnCollisionEnter (Collision col) 
	{
		hookRb.velocity = Vector3.zero;
		anchored = true;
		if (playerController.Grounded) 
		{
			playerController.Jump ();
		}
	}

	void FixedUpdate ()
	{
		if (anchored) 
		{
			PullPlayer ();
		}
	}

	void PullPlayer () 
	{
		Vector3 deltaPos = transform.position - playerRb.position;
		float distance = deltaPos.magnitude;

		if (distance < 1) {
			playerRb.velocity = Vector3.zero;
			anchored = false;
			Expire ();
		} else {
			playerRb.velocity = deltaPos.normalized * strength;
		}
	}

	void Expire()
	{
		if (!anchored) {
			Destroy (gameObject);
		}
	}
}
