using UnityEngine;
using System.Collections;

public class HookFixed : MonoBehaviour {

	public float lifetime = 1f;
	public float strength = 5f;

	private float distance;
	private Vector3 delta;
	private CharacterController player;
	private bool destroy = false;
	private Vector3 playerPos;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent <CharacterController> ();
	}

	void Update() {
		
		playerPos = player.transform.position;
		distance = (playerPos - transform.position).magnitude;

		if (destroy) {
			Debug.Log ("Claw destroyed");
			Destroy (gameObject);
		}
	}
	
	void FixedUpdate () {

		if (distance < 1.5) {
			delta = transform.position - playerPos;
			delta.y = 0;
			// player.jump();
			//player.Move (Vector3.up + delta.normalized);
			destroy = true;
		} else {
			player.Move (-(playerPos - transform.position).normalized * strength * 0.1f);		
		}
	}
}
