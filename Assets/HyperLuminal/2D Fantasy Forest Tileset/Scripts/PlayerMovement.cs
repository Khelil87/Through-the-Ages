using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour 
{
	#region Member Variables
	/// <summary>
	/// Player movement speed
	/// </summary>
	private float movementSpeed = 100.0f;

	/// <summary>
	/// Animation state machine local reference
	/// </summary>
	private Animator animator;

	/// <summary>
	/// The last position of the player in previous frame
	/// </summary>
	private Vector3 lastPosition;

	/// <summary>
	/// The last checkpoint position that we have saved
	/// </summary>
	private Vector3 CheckPointPosition;

	/// <summary>
	/// Is the player dead?
	/// </summary>
	private bool isDead = false;

	/// <summary>
	/// Random event generator
	/// </summary>
	private System.Random rand;

	/// <summary>
	/// Number of trasures collected
	/// </summary>
	private int collectibles;

	/// <summary>
	/// For debugging purposes, stops combat
	/// </summary>
	public bool combat = true;

	public Vector3 playerTransform;
	#endregion

	// Use this for initialization
	void Start ()
	{
		// get the local reference
		animator = GetComponent<Animator>();
		rand = new System.Random ();
		// set initial position
		lastPosition = transform.position;
		CheckPointPosition = transform.position;

		// load from global
		playerTransform = GlobalControl.Instance.playerTransform;
		GameObject.FindWithTag("Player").transform.position = playerTransform;
		collectibles = GlobalControl.Instance.collectibles;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// check for player exiting the game
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}

		// get the input this frame
		float vertical = Input.GetAxis("Vertical");
		float horizontal = Input.GetAxis("Horizontal");

		// if there is no input then stop the animation
		if((vertical == 0.0f)&&(horizontal == 0.0f))
		{
			animator.speed = 0.0f;
		}

		// reset the velocity each frame
		GetComponent<Rigidbody2D>().velocity =	new Vector2(0, 0);

		// horizontal movement, left or right, set animation type and speed 
		if (horizontal > 0)
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(movementSpeed * Time.deltaTime, 0);
			animator.SetInteger("Direction", 1);
			animator.speed = 0.5f;
		}
		else if (horizontal < 0)
		{
			GetComponent<Rigidbody2D>().velocity =	new Vector2(-movementSpeed * Time.deltaTime, 0);
			animator.SetInteger("Direction", 3);
			animator.speed = 0.5f;
		}

		// vertical movement, up or down, set animation type and speed 
		if (vertical > 0)
		{
			//transform.Translate(0, movementSpeed * 0.9f * Time.deltaTime, 0);
			GetComponent<Rigidbody2D>().velocity =	new Vector2(GetComponent<Rigidbody2D>().velocity.x, movementSpeed * Time.deltaTime);
			animator.SetInteger("Direction", 0);
			animator.speed = 0.35f;
		}
		else if (vertical < 0)
		{
			//transform.Translate(0, -movementSpeed *  0.9f * Time.deltaTime, 0);
			GetComponent<Rigidbody2D>().velocity =	new Vector2(GetComponent<Rigidbody2D>().velocity.x, -movementSpeed * Time.deltaTime);
			animator.SetInteger("Direction", 2);
			animator.speed = 0.35f;
		}

		if (vertical != 0 && horizontal != 0) 
		{
			if (rand.Next (100) == 1 && combat) 
			{
				SaveState ();
				int levelID = Application.loadedLevel + 1;
				Application.LoadLevel(levelID);
			}
		}

		//compare this position to the last known one, are we moving?
		if(this.transform.position == lastPosition)
		{
			// we aren't moving so make sure we dont animate
			animator.speed = 0.0f;
		}

		// get the last known position
		lastPosition = transform.position;

		// if we are dead do not move anymore
		if(isDead == true)
		{
			GetComponent<Rigidbody2D>().velocity =	new Vector2(0.0f, 0.0f);
			animator.speed = 0.0f;
		}

	}
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "DangerousTile") 
		{
			GameObject.Find ("FadePanel").GetComponent<FadeScript> ().RespawnFade ();
			isDead = true;
		}

		else if (collider.gameObject.tag == "LevelChanger") 
		{
			GameObject.Find ("FadePanel").GetComponent<FadeScript> ().FadeOut ();
			isDead = true;
		}

		else if (collider.gameObject.tag == "Collectible") 
		{
			if (++collectibles == 4) 
			{
				//int levelID = Application.loadedLevel - 1;
				//Application.LoadLevel(levelID);
				SceneManager.LoadScene("Game Win");
			}
		}
	}

	/// <summary>
	/// Respawns the player at checkpoint.
	/// </summary>
	public void RespawnPlayerAtCheckpoint()
	{
		// if we hit a dangerous tile then we are dead so go to the checkpoint position that was last saved
		transform.position = CheckPointPosition;
		isDead = false;
	}

	public void SaveState()
	{
		GlobalControl.Instance.collectibles = collectibles;
		//GlobalControl.Instance.collectible_r = collectible_r;
		//GlobalControl.Instance.collectible_b = collectible_b;
		//GlobalControl.Instance.collectible_g = collectible_g;
		//GlobalControl.Instance.collectible_y = collectible_y;
		GlobalControl.Instance.playerTransform = transform.position;
	}

}