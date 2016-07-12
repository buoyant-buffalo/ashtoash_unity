using UnityEngine;
using System.Collections;

public class PlayerExamine : MonoBehaviour {

    public float inspectDistance = 3f;

    private bool examineEnabled = false;
    private Transform cam;

    void Start ()
    {
        cam = Camera.main.transform;
    }

    void Update ()
    {
        RaycastHit hitInfo;
        Debug.DrawRay(cam.position, cam.forward * inspectDistance, Color.red);
        if (Physics.Raycast(cam.position, cam.forward, out hitInfo, inspectDistance)) {
            if (hitInfo.collider.gameObject.CompareTag("Ember")) {
//                examineEnabled = true;
                if (Input.GetKeyDown(KeyCode.E)) {
					Examine(hitInfo.collider.gameObject);
                }
            }
        } else {
//            examineEnabled = false;
        }
    }

	void Examine (GameObject item)
	{
        Debug.Log("Inspected item: " + item.name.ToString());
        Destroy(item);
	}
}
