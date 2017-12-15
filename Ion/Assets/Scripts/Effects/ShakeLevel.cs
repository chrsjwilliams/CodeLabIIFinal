using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChrsUtils.ChrsCamera;

/*--------------------------------------------------------------------------------------*/
/*																						*/
/*	ShakeLevel: Shakes the level whne the player collides with the trigger area			*/
/*																						*/
/*		Functions:																		*/
/*			private:																	*/
/*				void OnTriggerEnter2D(Collider2D other)									*/
/*																						*/
/*--------------------------------------------------------------------------------------*/
public class ShakeLevel : MonoBehaviour 
{
	//	Public const Variables
	public const string PLAYER = "Player";				//	The player tag	

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	OnTriggerEnter2D: Function runs when collider enters trigger area					*/
	/*			param:																		*/
	/*				Collider2D other - the collider that entered the trigger area			*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag.Contains(PLAYER))
		{
			CameraShake.CameraShakeEffect.Shake(0.1f, 0.25f);
		}
	}
}
