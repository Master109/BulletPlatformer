using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
	public Vector2 vel;
	public int speed;
	public bool madeByPlayer;

	// Use this for initialization
	void Start ()
	{
		vel = Vector2.ClampMagnitude(vel * 9999999999, speed);
		if (madeByPlayer)
			renderer.material.color = Color.green;
		else
			renderer.material.color = Color.red;
	}
	
	// Update is called once per frame
	void Update ()
	{
		rigidbody2D.velocity = vel;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.name == "Player" && !madeByPlayer)
		{
			other.GetComponent<Player>().hp --;
			if (other.GetComponent<Player>().hp <= 0)
				Application.LoadLevel(Application.loadedLevel);
			Destroy(gameObject);
		}
		else if (other.name.Contains("Enemy") && madeByPlayer)
		{
			other.GetComponent<Enemy>().hp --;
			if (other.GetComponent<Enemy>().hp <= 0)
				Destroy(other.gameObject);
			Destroy(gameObject);
		}
		else if (other.name.Contains("Bound"))
			Destroy(gameObject);
	}
}
