using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*--------------------------------------------------------------------------------------*/
/*																						*/
/*	MovingParticle: Sub class of Particle that moves based on magnetic force			*/
/*			Extends: Particle															*/
/*																						*/
/*		Functions:																		*/
/*			private:																	*/
/*				void Start () 															*/
/*																						*/
/*--------------------------------------------------------------------------------------*/
public class MovingParticle : Particle 
{
	//	Public Variables
	public float mass = 1.0f;			//	Given mass of particle
	public Rigidbody2D myRigidbody;		//	Rigidbody2D of particle

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	Start: Runs once at the begining of the game. Initalizes variables.					*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	void Start () 
	{
		UpdateColor();
		myRigidbody = gameObject.AddComponent<Rigidbody2D>();
		myRigidbody.mass = mass;
		myRigidbody.gravityScale = 0;
	}
}
