using UnityEngine;
using ChrsUtils.ChrsEventSystem.GameEvents;
using IonGameEvents;


//  TODO: show winner at end
/*--------------------------------------------------------------------------------------*/
/*																						*/
/*	GameManager: Updates Score and resets game											*/
/*																						*/
/*		Functions:																		*/
/*			private:																	*/
/*				void Start ()															*/
/*				void OnParticleEnter(GameEvent ige)										*/
/*				void OnParticleExit(GameEvent ige)										*/
/*				void OnTimeIsOver(GameEvent ige)										*/
/*				void UpdateScore(string tag, float score)								*/
/*				void RestartGame()														*/
/*				void Update () 															*/
/*																						*/
/*--------------------------------------------------------------------------------------*/
public class GameManager : MonoBehaviour 
{
    private static GameManager _instance;
    public static  GameManager Instance
    {
        get { return _instance; }
        private set { }
    }

	//	Public Const Variable
	public const KeyCode RESTART_GAME = KeyCode.Backspace;					//	The button to restart the game
    public string sceneName;
    public string winner;

    [SerializeField]
    private float player1Score;
    [SerializeField]
    private float player2Score;

    private GameObject _Main;

	private ParticleEnteredZoneEvent.Handler onParticleEnter;		//	Handler for OnParticleEnteredZoneEvent
	private ParticleExitedZoneEvent.Handler onParticleExit;			//	Handler for OnParticleExitedZoneEvent
    private StartTimerEvent.Handler onStartTimerEvent;              //  Handler for OnStartTimerEvent
    private TimeIsOverEvent.Handler onTimeIsOver;					//	Handler for TimeIsOverEvent

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	Start: Runs once at the begining of the game. Initalizes variables.					*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	void Start () 
	{
        if (_instance == null)
        {
            _instance = this;
        }

		//	Sets up the handlers
		onParticleEnter = new ParticleEnteredZoneEvent.Handler(OnParticleEnter);
		onParticleExit = new ParticleEnteredZoneEvent.Handler(OnParticleExit);
        onStartTimerEvent = new StartTimerEvent.Handler(OnStartTimerEvent);
        onTimeIsOver = new TimeIsOverEvent.Handler(OnTimeIsOver);

        //	Registers for events
        Services.Events.Register<StartTimerEvent>(onStartTimerEvent);
        Services.Events.Register<TimeIsOverEvent>(onTimeIsOver);

        _Main = GameObject.Find("Main");
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	OnParticleEnter: Handler for OnParticleEnter Event									*/
	/*			param:																		*/
	/*				GameEvent ige - access to readonly variables in event					*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	void OnParticleEnter(GameEvent ige)
	{
		//	Decrements score
		((ParticleEnteredZoneEvent)ige).zone.score--;
		UpdateScore(((ParticleEnteredZoneEvent)ige).zone.tag, ((ParticleEnteredZoneEvent)ige).zone.score);
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	OnParticleExit: Handler for OnParticleExit Event									*/
	/*			param:																		*/
	/*				GameEvent ige - access to readonly variables in event					*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	void OnParticleExit(GameEvent ige)
	{
		//	Increments score
		((ParticleExitedZoneEvent)ige).zone.score++;
		UpdateScore(((ParticleExitedZoneEvent)ige).zone.tag, ((ParticleExitedZoneEvent)ige).zone.score);
	}

    /*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	OnStartTimerEvent: Handler for OnStartTimerEvent Event								*/
    /*			param:																		*/
    /*				GameEvent ige - access to readonly variables in event					*/
    /*																						*/
    /*--------------------------------------------------------------------------------------*/
    void OnStartTimerEvent(GameEvent ige)
    {
        Services.Events.Register<ParticleEnteredZoneEvent>(onParticleEnter);
        Services.Events.Register<ParticleExitedZoneEvent>(onParticleExit);
    }

    /*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	OnTimeIsOver: Handler for OnTimeIsOver Event										*/
    /*			param:																		*/
    /*				GameEvent ige - access to readonly variables in event					*/
    /*																						*/
    /*--------------------------------------------------------------------------------------*/
    void OnTimeIsOver(GameEvent ige)
	{
        //	Pauses the game
        Services.Events.Unregister<ParticleEnteredZoneEvent>(onParticleEnter);
        Services.Events.Unregister<ParticleExitedZoneEvent>(onParticleExit);
        Services.Events.Fire(new TenSecondsLeftEvent(false));
        Services.Scenes.Swap<ResultsSceneScript>();
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	UpdateScore: Updates the game's UI													*/
	/*			param:																		*/
	/*				string tag - determines which score should be updated					*/
	/*				float score - the new score												*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	void UpdateScore(string tag, float score)
	{
		if(tag.Contains("1"))
		{
			GameSceneScript.player1Score.text = score.ToString();
		}
		else if (tag.Contains("2"))
		{
            GameSceneScript.player2Score.text = score.ToString();
		}

        player1Score = float.Parse(GameSceneScript.player1Score.text);
        player2Score = float.Parse(GameSceneScript.player2Score.text);
        if (player1Score > player2Score)
        {
            winner = "ORANGE WINS";
        }
        else if (player1Score < player2Score)
        {
            winner = "PINK WINS";
        }
        else
        {
            winner = "TIE GAME";
        }
    }
	
	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	RestartGame: Handles clean up for error free transitions							*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	void RestartGame()
	{
        Services.Events.Unregister<ParticleEnteredZoneEvent>(onParticleEnter);
        Services.Events.Unregister<ParticleExitedZoneEvent>(onParticleExit);
        Services.Scenes.Swap<TitleSceneScript>();
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	Update: Called once per frame														*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	void Update () 
	{
        sceneName = _Main.transform.GetChild(0).tag;

        if(sceneName == "Title")
        {
            winner = "";
        }

		if(Input.GetKeyDown(RESTART_GAME))
		{	
			RestartGame();
		}
	}
}
