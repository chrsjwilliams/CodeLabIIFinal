using UnityEngine;
using UnityEngine.UI;
using GameScenes;
using IonGameEvents;

public class GameSceneScript : Scene<TransitionData>
{
    //	Private Const Variables
    private const string MAIN_SCENE = "Main";                       //	Name of the main scene
    private const string GAME_TIMER = "Timer";                      //	Name of the Timer GameObject
    private const string PLAYER_1_SCORE = "Player1Score";           //	Name of the Text UI for player 1
    private const string PLAYER_2_SCORE = "Player2Score";			//	Name of the Text UI for player 2
    private const string SPAWN_POINT = "SpawnPoint";

    public int numberOfPlayers;
    public static Transform spawnPoint;

    //	Private Variables
    [SerializeField]
    private Timer gameTimer;										//	Reference to the game timer
    [SerializeField]
    public static Text player1Score;								//	Reference to player 1's score
    [SerializeField]
    public static Text player2Score;								//	Reference to player 2's score

    internal override void OnEnter(TransitionData data)
    {
        gameTimer = GameObject.Find(GAME_TIMER).GetComponent<Timer>();

        numberOfPlayers = GameObject.FindGameObjectsWithTag("Player").Length;

        player1Score = GameObject.Find(PLAYER_1_SCORE).GetComponent<Text>();
        player2Score = GameObject.Find(PLAYER_2_SCORE).GetComponent<Text>();

        spawnPoint = GameObject.Find(SPAWN_POINT).transform;

        //	Adds time to Timer
        gameTimer.AddDurationInSeconds(61);

        //	Fires StartTimerEvent
        Services.Events.Fire(new StartTimerEvent());
    }

    internal override void OnExit()
    {

    }

    private void Update()
    {
        numberOfPlayers = GameObject.FindGameObjectsWithTag("Player").Length;
        if (numberOfPlayers < 2)
        {
            GameObject player = ObjectPool.GetFromPool(Poolable.types.PLAYER);
        }
    }

}
