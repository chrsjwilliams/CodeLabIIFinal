using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChrsUtils.ChrsEventSystem.GameEvents;
using IonGameEvents;

public class PanelMillController : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 0.0f;

    private PlayerShiftEvent.Handler onReversePolarityEvent;      //  Handler for OnStartTimerEvent

    // Use this for initialization
    void Start ()
    {
        onReversePolarityEvent = new PlayerShiftEvent.Handler(OnReversePolarityEvent);
        Services.Events.Register<PlayerShiftEvent>(onReversePolarityEvent);
    }

    private void OnReversePolarityEvent(GameEvent ige)
    {
        PlayerShiftEvent shiftEvent = (PlayerShiftEvent)ige;
        if(shiftEvent.player.name == "Player1" && !shiftEvent.player.currentlyReversed)
        {
            rotateSpeed += 1.0f;
            if(rotateSpeed > 5)
            {
                rotateSpeed = 5;
            }
        }
        else if (shiftEvent.player.name == "Player2" && !shiftEvent.player.currentlyReversed)
        {
            rotateSpeed += -1.0f;
            if (rotateSpeed < -5)
            {
                rotateSpeed = -5;
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
        transform.Rotate(new Vector3(0, 0, rotateSpeed));
	}
}
