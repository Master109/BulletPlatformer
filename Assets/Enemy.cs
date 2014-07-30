using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public float maxSpeed = 10f;
	public bool facingRight = true;
	Vector2 move;
	bool grounded = false;
	float groundCheckRadius = 0.375f;
	public LayerMask whatIsGround;
	public int jumpForce = 500;
	public int hp;
	public GameObject bullet;
	GameObject go;
	public float shootRate;
	float shootTimer;
	public int bulletSpeed;
	public bool awake;
	public bool flying;
	public bool followPlayer;
	public int[] shootAngles;

	void Awake ()
	{
		
	}
	
	// Use this for initialization
	void Start ()
	{

	}
	
	public void Reset ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!awake)
			return;
		if (Time.timeSinceLevelLoad - shootTimer >= shootRate)
		{
			shootTimer = Time.timeSinceLevelLoad;
			Vector2 toPlayer = GameObject.Find("Player").transform.position - transform.position;
			foreach (int angModifier in shootAngles)
			{
				float ang = Mathf.Atan2(toPlayer.y, toPlayer.x);
				ang += angModifier;
				go = (GameObject) GameObject.Instantiate(bullet, transform.position, Quaternion.identity);
				go.GetComponent<Bullet>().vel = new Vector2(Mathf.Cos(ang), Mathf.Sin(ang));
				go.GetComponent<Bullet>().madeByPlayer = false;
				go.GetComponent<Bullet>().speed = bulletSpeed;
			}
		}
		if (followPlayer)
		{
			move = GameObject.Find("Player").transform.position - transform.position;
			if (flying)
				rigidbody2D.velocity = Vector2.ClampMagnitude(move * 9999999999, maxSpeed);
		}
	}
}
