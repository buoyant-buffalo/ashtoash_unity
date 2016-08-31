using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour 
{
    public static GameManager instance = null;

    public int embers = 0;

    private UnityAction emberListener; 

	void Awake () 
	{
		if (instance != null && instance != this)	{
			Destroy(gameObject);
		} else {
			instance = this;
		}
		DontDestroyOnLoad(gameObject);

        emberListener = new UnityAction(IncreaseEmbers);
   	}

    void OnEnable ()
    {
        EventManager.StartListening("ember", emberListener);
    }

    void IncreaseEmbers ()
    {
        embers++;
        Debug.Log("Embers increased");
    }

    void OnDisable ()
    {
        EventManager.StopListening("ember", emberListener);
    }
}
