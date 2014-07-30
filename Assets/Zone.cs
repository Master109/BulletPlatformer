using UnityEngine;
using System.Collections;

public class Zone : MonoBehaviour
{
	bool awake;
	ArrayList enemies = new ArrayList();

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.name.Contains("Enemy"))
		{
			enemies.Add(other.GetComponent<Enemy>()); 
		}
		if (other.name == "Player")
		{
			foreach (Enemy e in enemies)
				e.awake = true;
		}
	}
}
