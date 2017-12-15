using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using IonGameEvents;

/*--------------------------------------------------------------------------------------*/
/*																						*/
/*	PlayerControls: Handles player state in N-gon										*/
/*			Functions:																	*/
/*					public:																*/
/*																						*/
/*					private:															*/
/*						void Start ()													*/
/*						void Move (float dx, float dy)									*/
/*						void ReversePolarity ()											*/
/*						IEnumerator NormalizePolarity()									*/
/*						void OnCollisionEnter2D(Collision2D other)						*/
/*						void Update () 													*/
/*																						*/
/*--------------------------------------------------------------------------------------*/
public class PlayerControls : MonoBehaviour 
{
	//	Public Const Variables
	public const string PLAYER1 = "Player1";		//	Tag for player 1
	public const string PLAYER2 = "Player2";        //	Tag for player 2

    //	Public Variabels
    public bool currentlyReversed;
	public float moveSpeed = 10.0f;					//	Default movement speed of character
    public float leftLimit = -12.4f;
    public float rightLimit = 16.2f;
    public float lowerLimit = -2.455001f;
	public float upperLimit = 13.39f;
    public KeyCode reversePolarity;	                //	Key for shifting polarity
	public KeyCode upKey = KeyCode.W;				//	Key for moving up
	public KeyCode downKey = KeyCode.S;				//	Key for moving down
	public KeyCode leftKey = KeyCode.A;				//	Key for moving left
	public KeyCode rightKey = KeyCode.D;			//	Key for moving right

	//	Private Variables
	private float x = 0;							//	Float to store from Input.GetAxis
	private float y = 0;							//	Float to store from Input.GetAxis
	private SpriteRenderer _MyRenderer;				//	Reference to this SpriteRenderer
	private Rigidbody2D _Rigidbody2D;				//	Reference to player's rigidbody
	private TrailRenderer _MyTrail;					//	Reference to TrailRenderer
	private Particle _ThisParticle;					//	Reference to player's Particle component
    private ParticleSystem _ParticleSystem;

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	Start: Runs once at the begining of the game. Initalizes variables.					*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	private void Start () 
	{
        currentlyReversed = false;

		_Rigidbody2D = GetComponent<Rigidbody2D> ();
		_ThisParticle = GetComponent<Particle>();
		_MyRenderer = GetComponent<SpriteRenderer>();
		_MyTrail = GetComponent<TrailRenderer>();
        _ParticleSystem = GetComponent<ParticleSystem>();

        _ParticleSystem.Stop();
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	Move: moves the player in a direction x and/or y based on axis input				*/
	/*		param:																			*/
	/*			float dx - horizontal axis input											*/
	/*			float dy - vertical axis input												*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	private void Move (KeyCode key,float dx, float dy)
	{
		if(Input.GetKey(key))
		{
			_Rigidbody2D.velocity = new Vector2(dx * moveSpeed, dy * moveSpeed);
        }

        if (transform.position.x < leftLimit)
        {
            transform.position = new Vector3(leftLimit, transform.position.y, transform.position.z);
        }

        if (transform.position.x > rightLimit)
        {
            transform.position = new Vector3(rightLimit, transform.position.y, transform.position.z);
        }

        if (transform.position.y > upperLimit)
        {
             transform.position = new Vector3(transform.position.x, upperLimit, transform.position.z);
        }
       
        if (transform.position.y < lowerLimit)
        {
             transform.position = new Vector3(transform.position.x, lowerLimit, transform.position.z);
        }
    }

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	ReversePolarity: Pushes the moving particles away									*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	private void ReversePolarity ()
	{
        Services.Events.Fire(new PlayerShiftEvent(this, currentlyReversed));
		StartCoroutine(NormalizePolarity());
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	NormalizePolarity: Returns Polarity to normal 										*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	IEnumerator NormalizePolarity()
	{
        currentlyReversed = true;
		_ThisParticle.charge = _ThisParticle.charge * -4.0f;
		_MyRenderer.color = _ThisParticle.nodeDischarged;
		_MyTrail.startColor = _ThisParticle.nodeDischarged;
		_MyTrail.endColor = _ThisParticle.nodeDischarged;
		yield return new WaitForSeconds(1.0f);
        currentlyReversed = false;
		_MyRenderer.color = _ThisParticle.nodeCharged;
		_MyTrail.startColor = _ThisParticle.nodeCharged;
		_MyTrail.endColor = _ThisParticle.nodeCharged;
		_ThisParticle.charge = (_ThisParticle.charge/ 4.0f)* -1.0f;
	}


    private IEnumerator Explosion()
    {
        _ParticleSystem.Stop();
        _ParticleSystem.Play();
        yield return new WaitForSeconds(0.25f);
        _ParticleSystem.Stop();
    }

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	OnCollisionEnter2D: Function runs when collider enter collides with anything		*/
	/*			param:																		*/
	/*				Collision2D other - the thing we collided with							*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	void OnCollisionEnter2D(Collision2D other)
	{
        
		//	TODO: Can add effetcs based on collision
		if (other.gameObject.tag == "MovingParticle")
		{
			
		}
        else
        {
            Services.Events.Fire(new PlayerCollidedEvent(gameObject.name));
            StartCoroutine(Explosion());
        }
	}
	
	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	Update: Called once per frame														*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	private void Update () 
	{
		switch(gameObject.name)
		{
			case PLAYER1:
					x = Input.GetAxis ("Horizontal");
					y = Input.GetAxis ("Vertical");
				break;
			case PLAYER2:
					x = Input.GetAxis ("Player2_Horizontal");
					y = Input.GetAxis ("Player2_Vertical");
				break;
		}	

		Move (upKey, x, y);
		Move (downKey, x, y);
		Move (leftKey, x, y);
		Move (rightKey, x ,y);	

		if (Input.GetKeyDown(reversePolarity))
		{
			ReversePolarity ();
		}
	}
}