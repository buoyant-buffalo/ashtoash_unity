using UnityEngine;
using System.Collections;
using WindowsInput;

public class HookShot : MonoBehaviour {

	public float lifetime = 1f;
	public float strength = 100f;
	public float smoothing = 5f;

	private bool anchored = false;
	private bool firstPullFrame = true;
	private Vector3 delta;
	private Rigidbody hookRb;
	private Rigidbody playerRb;
	private UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController playerController;

	void Start () 
	{
		hookRb = GetComponent <Rigidbody> ();
		playerRb = GameObject.FindGameObjectWithTag ("Player").GetComponent <Rigidbody> ();
		playerController = playerRb.GetComponent <UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController> ();

		hookRb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

		Invoke("Expire", lifetime);
	}
		
	void OnCollisionEnter (Collision col) 
	{
		hookRb.velocity = Vector3.zero;
		InputSimulator.SimulateKeyPress (VirtualKeyCode.SPACE);
		anchored = true;
		playerRb.collisionDetectionMode = CollisionDetectionMode.Continuous;
	}

	void FixedUpdate ()
	{
		if (anchored && !playerController.Grounded) {
			PullPlayer ();
		}
	}

	void PullPlayer () 
	{
		Vector3 deltaPos = transform.position - playerRb.position;
		float distance = deltaPos.magnitude;

		if (firstPullFrame) {
			playerRb.AddForce (deltaPos.normalized * strength, ForceMode.Impulse);
			firstPullFrame = false;
		}

		if (distance < 1) {
			playerRb.AddForce (Vector3.zero, ForceMode.VelocityChange);
			anchored = false;
			Expire ();
		} else if (distance <= 5) {
//			delta = playerRb.position - transform.position;
//			delta.y = 0;
//			playerRb.AddForce ((playerRb.position - transform.position).normalized * smoothing, ForceMode.VelocityChange);
//			playerRb.velocity = Vector3.up + delta.normalized;
			playerRb.velocity *= 0.9f;
		} else {
			playerRb.AddForce (deltaPos.normalized, ForceMode.VelocityChange);
		}
	}

	void Expire()
	{
		if (!anchored) {
			Destroy (gameObject);
		}
	}
}
