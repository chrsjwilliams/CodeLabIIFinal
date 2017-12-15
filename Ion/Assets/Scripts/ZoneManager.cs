using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ChrsUtils.ChrsEventSystem.EventsManager;
using IonGameEvents;

/*--------------------------------------------------------------------------------------*/
/*																						*/
/*	ZoneManager: Handles logic for the zones											*/
/*																						*/
/*		Functions:																		*/
/*			private:																	*/
/*				void Start () 															*/
/*				void OnTriggerEnter2D(Collider2D other)									*/
/*				void OnTriggerExit2D(Collider2D other)									*/
/*																						*/
/*--------------------------------------------------------------------------------------*/
public class ZoneManager : MonoBehaviour 
{
	private const string PARTICLE = "MovingParticle";		//	Tag for the moving particles
	public int score;										//	This zone's current score

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	Start: Runs once at the begining of the game. Initalizes variables.					*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	void Start () 
	{
		score  = 0;
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	OnTriggerEnter2D: Function runs when collider enters trigger area					*/
	/*			param:																		*/
	/*				Collider2D other - the collider that entered the trigger area			*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag.Contains(PARTICLE))
		{
            Services.Events.Fire(new ParticleEnteredZoneEvent(this));
		}
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	OnTriggerExit2D: Function runs when collider exits trigger area						*/
	/*			param:																		*/
	/*				Collider2D other - the collider that entered the trigger area			*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag.Contains(PARTICLE))
		{
            Services.Events.Fire(new ParticleExitedZoneEvent(this));
		}
	}
}
