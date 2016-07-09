using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	private static GameManager _instance = null;

	public static GameManager Instance 
	{ 
		get { return _instance; } 
	}

	void Awake () 
	{
		if (_instance != null && _instance != this)	
		{
			Destroy(gameObject);
		}
		else 
		{
			_instance = this;
		}
		DontDestroyOnLoad(gameObject);
	}
	
	void Update () 
	{
	
	}
}
