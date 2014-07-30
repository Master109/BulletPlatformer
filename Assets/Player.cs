using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public float maxSpeed = 10f;
	public bool facingRight = true;
	float move = 0f;
	bool grounded = false;
	float groundCheckRadius = 0.375f;
	public LayerMask whatIsGround;
	public int jumpForce = 500;
	//public Transform wallCheck1;
	//public Transform wallCheck2;
	float wallCheckRadius = 0.4f;
	public float groundHoverAmount = .0001f;
	public GUISkin guiSkin1;
	public int pushOfWallForce = 500;
	public float extraXVel = 0;
	bool onWall = false;
	public bool paused;
	public Vector2 spawnLoc;
	public int hp;
	public GameObject bullet;
	GameObject go;
	public float shootRate;
	float shootTimer;
	public int bulletSpeed;

	void Awake ()
	{

	}

	// Use this for initialization
	void Start ()
	{
		//jumpForce *= transform.lossyScale.x;
		//maxSpeed *= transform.lossyScale.x;
		spawnLoc = transform.position;
		Input.ResetInputAxes();
		rigidbody2D.velocity = Vector2.zero;
	}

	public void Reset ()
	{
		transform.position = spawnLoc;
		Input.ResetInputAxes();
		rigidbody2D.velocity = Vector2.zero;
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		grounded = Physics.CheckSphere(transform.Find("GroundCheck").position, groundCheckRadius, whatIsGround);
		move = Input.GetAxis("Horizontal") * maxSpeed;
		if (!onWall && ((move > 0 && !facingRight) || (move < 0 && facingRight)))
			Flip ();
		/*
		if (move != 0 && Physics2D.OverlapCircle(wallCheck1.position, wallCheckRadius, whatIsGround) && !Physics2D.OverlapCircle(wallCheck2.position, wallCheckRadius, whatIsGround))
			transform.position = new Vector2(transform.position.x, transform.position.y + Vector2.Distance(wallCheck1.position, wallCheck2.position) + 0.1f);
			*/
		/*
		ArrayList array = new ArrayList ();
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("WallCheck"))
		{
			if (go.transform.IsChildOf(transform) && Physics.CheckSphere(go.transform.position, wallCheckRadius, whatIsGround))
			{
				array.Clear ();
				array.AddRange(Physics.OverlapSphere(go.transform.position, wallCheckRadius, whatIsGround));
				bool b = true;
				foreach (Collider c in array)
				{
					if (c.name.Contains("(Wall Jump)"))
						b = false;
				}
				if (b)
				{
					extraXVel = 0;
					move = 0;
					break;
				}
			}
		}
		onWall = false;
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("WallCheck"))
		{
			if (go.transform.IsChildOf(transform) && Physics.CheckSphere(go.transform.position, wallCheckRadius, whatIsGround))
			{
				array.Clear ();
				array.AddRange(Physics.OverlapSphere(go.transform.position, wallCheckRadius, whatIsGround));
				bool b = true;
				foreach (Collider c in array)
				{
					if (!c.name.Contains("(Wall Jump)"))
						b = false;
				}
				if (b)
				{
					onWall = true;
					extraXVel = 0;
					int x = 0;
					if (transform.position.x > go.transform.position.x)
						x = 1;
					else
						x = -1;
					if ((move > 0 && x < 0) || (move < 0 && x > 0))
						move = 0;
					if (Input.GetAxisRaw("Horizontal") == x && Input.GetAxisRaw("Jump") == 1)
					{
						rigidbody2D.velocity = Vector2.zero;
						rigidbody2D.AddForce(Vector2.up * jumpForce);
						extraXVel = x * pushOfWallForce;
						grounded = false;
						onWall = false;
						break;
					}
				}
			}
		}
		extraXVel *= rigidbody2D.drag;
		*/
		if (move != 0)
			rigidbody2D.velocity = new Vector2(move + extraXVel, rigidbody2D.velocity.y);
	}
	
	void Update ()
	{
		if (grounded && Input.GetKeyDown(KeyCode.Space))
		{
			rigidbody2D.AddForce(Vector2.up * jumpForce);
			grounded = false;
		}
		if (Time.timeSinceLevelLoad - shootTimer >= shootRate)
		{
			shootTimer = Time.timeSinceLevelLoad;
			go = (GameObject) GameObject.Instantiate(bullet, transform.position, Quaternion.identity);
			if (Input.GetKey(KeyCode.UpArrow))
				go.GetComponent<Bullet>().vel = transform.up;
			else if (Input.GetKey(KeyCode.DownArrow))
				go.GetComponent<Bullet>().vel = -transform.up;
			else
				go.GetComponent<Bullet>().vel = transform.right * transform.lossyScale.x;
			go.GetComponent<Bullet>().madeByPlayer = true;
			go.GetComponent<Bullet>().speed = bulletSpeed;
		}
	}

	void Flip () 
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnGUI ()
	{
		///*
		//GUI.skin = guiSkin2;
		if (paused)
		{
			//time = Time.fixedTime - time;
			if (GUI.Button(new Rect(0, Screen.height / 2, 100, 25), "Resume"))
			{
				Time.timeScale = 1;
				paused = false;
			}
			GUI.Label(new Rect(0, 0, 99999, 25), "Health: " + hp);
		}
		else
		{
			//Time.timeScale = 1;
			//GUI.skin = guiSkin1;
			if (GUI.Button(new Rect(0, Screen.height / 2, 100, 25), "Menu"))
			{
				paused = true;
				Time.timeScale = 0;
			}
			GUI.Label(new Rect(0, 0, 99999, 25), "Health: " + hp);
		}
		//*/
	}
}