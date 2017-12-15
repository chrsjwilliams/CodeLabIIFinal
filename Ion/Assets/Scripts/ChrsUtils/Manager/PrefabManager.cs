﻿using UnityEngine;
using System.Collections;


namespace ChrsUtils
{
    namespace ChrsPrefabManager
    { 
		/*--------------------------------------------------------------------------------------*/
		/*																						*/
		/*	PrefabManager: Manages prefabs when they are destroyed								*/
		/*			public:																		*/
        /*																						*/
		/*			private:																	*/
		/*																						*/
		/*--------------------------------------------------------------------------------------*/
		public class PrefabManager : MonoBehaviour 
		{
			//	Public Variables
			public GameObject playerPrefab;						//	Reference to player prefab

			//Private Static Variabels
			private static PrefabManager m_Instance = null;		//	temporary variable to store instances

			/*--------------------------------------------------------------------------------------*/
			/*																						*/
			/*	Returns new instance of the prefab manager when done								*/
			/*																						*/
			/*--------------------------------------------------------------------------------------*/
			public static PrefabManager Instance
			{
				get
				{
					if (m_Instance == null)
					{
						m_Instance = (PrefabManager)FindObjectOfType (typeof(PrefabManager));
					}
					return m_Instance;
				}
			}
		}
	}
}