using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour 
{
    public static GameManager instance = null;

    public int embers = 0;

	void Awake () 
	{
		if (instance != null && instance != this)	{
			Destroy(gameObject);
		} else {
			instance = this;
		}
		DontDestroyOnLoad(gameObject);
   	}
	
	void Update () 
	{
	}
}
