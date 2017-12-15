using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChrsUtils.ChrsEventSystem.GameEvents;
using IonGameEvents;

public class SFXManager : MonoBehaviour
{
    public AudioClip player1Clip;
    public AudioClip player2Clip;
    public AudioSource[] soundCauser;
    public Dictionary<string, AudioSource> audioSourceSheet;

    private PlayerCollidedEvent.Handler onPlayerCollision;
    private PlayerShiftEvent.Handler onPlayerShifting;

	// Use this for initialization
	void Start ()
    {
        soundCauser = GetComponentsInChildren<AudioSource>();
        PopulateDictionary();

        onPlayerCollision = new PlayerCollidedEvent.Handler(OnPlayerCollision);
        onPlayerShifting = new PlayerShiftEvent.Handler(OnPlayerShifting);

        Services.Events.Register<PlayerCollidedEvent>(onPlayerCollision);
        Services.Events.Register<PlayerShiftEvent>(onPlayerShifting);

    }

    private void PopulateDictionary()
    {
        audioSourceSheet = new Dictionary<string, AudioSource>();
        for (int i = 0; i < soundCauser.Length; i++)
        {
            audioSourceSheet.Add(soundCauser[i].name, soundCauser[i]);
        }
    }

    private void OnPlayerCollision(GameEvent ige)
    {
        string player = ((PlayerCollidedEvent)ige).player;

        if (player == "Player1")
        {
            player1Clip = Resources.Load<AudioClip>("Audio/Collide");
            audioSourceSheet["Player1"].Stop();
            audioSourceSheet["Player1"].PlayOneShot(player1Clip);
        }
        else if (player == "Player2")
        {
            player2Clip = Resources.Load<AudioClip>("Audio/Collide");
            audioSourceSheet["Player2"].Stop();
            audioSourceSheet["Player2"].PlayOneShot(player2Clip);
        }
    }

    private void OnPlayerShifting(GameEvent ige)
    {
        PlayerControls player = ((PlayerShiftEvent)ige).player;
        bool currentlyShifting = ((PlayerShiftEvent)ige).currentlyReversed;

        if(player.name == "Player1")
        {
            if (currentlyShifting)
            {
                player1Clip = Resources.Load<AudioClip>("Audio/Null");
                audioSourceSheet["Player1"].Stop();
                audioSourceSheet["Player1"].PlayOneShot(player1Clip);
            }
            else
            {
                player1Clip = Resources.Load<AudioClip>("Audio/Shift");
                audioSourceSheet["Player1"].Stop();
                audioSourceSheet["Player1"].PlayOneShot(player1Clip);
            }
        }
        else if (player.name == "Player2")
        {
            if (currentlyShifting)
            {
                player2Clip = Resources.Load<AudioClip>("Audio/Null");
                audioSourceSheet["Player1"].Stop();
                audioSourceSheet["Player2"].PlayOneShot(player2Clip);
            }
            else
            {
                player2Clip = Resources.Load<AudioClip>("Audio/Shift");
                audioSourceSheet["Player2"].Stop();
                audioSourceSheet["Player2"].PlayOneShot(player2Clip);
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
