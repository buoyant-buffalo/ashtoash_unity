using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerInteract : MonoBehaviour {

    public float interactDistance = 3f;
    public Text interactText;

    private bool examineEnabled = false;
    private Transform cam;

    void Start ()
    {
        cam = Camera.main.transform;
    }

    void Update ()
    {
        RaycastHit hitInfo;
        Debug.DrawRay(cam.position, cam.forward * interactDistance, Color.red);

        if (Physics.Raycast(cam.position, cam.forward, out hitInfo, interactDistance)) {
            interactText.gameObject.SetActive(true);

            switch (hitInfo.collider.gameObject.tag) {
                case "Ember":
                    interactText.text = "Collect Ember";
                    if (Input.GetKeyDown(KeyCode.E)) {
                        CollectEmber(hitInfo.collider.gameObject);
                    }
                    break;

                case "Artifact":
                    interactText.text = "Examine Artifact";
                    if (Input.GetKeyDown(KeyCode.E)) {
                        Examine(hitInfo.collider.gameObject);
                    }
                    break;

                case "Burn":
                    interactText.text = "Burn it with fire";
                    if (Input.GetKeyDown(KeyCode.E)) {
                        hitInfo.collider.gameObject.GetComponent<Burn>().enabled = true;
                    }
                    break;
            }
        } 
        else {
            examineEnabled = false;
            interactText.gameObject.SetActive(false);
        }
    }

	void CollectEmber (GameObject item)
	{
        EventManager.TriggerEvent("ember");
//        GameManager.instance.embers++;
//        Debug.Log("Inspected item: " + item.name.ToString());
//        Debug.Log("Embers: " + GameManager.instance.embers);
        Destroy(item);
	}

    void Examine (GameObject item)
    {
//        TODO
    }
}
