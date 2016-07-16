using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerExamine : MonoBehaviour {

    public float inspectDistance = 3f;
    public Text examineText;

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
                examineText.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E)) {
					Examine(hitInfo.collider.gameObject);
                }
            }
        } else {
            examineEnabled = false;
            examineText.gameObject.SetActive(false);
        }
    }

	void Examine (GameObject item)
	{
        GameManager.instance.embers++;

        Debug.Log("Inspected item: " + item.name.ToString());
        Debug.Log("Embers: " + GameManager.instance.embers);

        Destroy(item);
	}
}
