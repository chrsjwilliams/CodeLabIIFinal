using UnityEngine;
using GameScenes;
using UnityEngine.UI;

public class ResultsSceneScript : Scene<TransitionData>
{
    public Text winner;
    public Text playerName;

    private void Start()
    {
        if (!winner)
        {
            winner = GameObject.Find("Winner").GetComponent<Text>();
        }

        if (!playerName)
        {
            playerName = GameObject.Find("PlayerName").GetComponent<Text>();
        }

        Camera.main.backgroundColor = Color.black;

        playerName.text = GameManager.Instance.winner;

        if (playerName.text.Contains("ORANGE"))
        {
            playerName.color = new Color(1.0f, 0.617f, 0.266f);
            winner.color = new Color(1.0f, 0.617f, 0.266f);
        }
        else if (playerName.text.Contains("PINK"))
        {
            playerName.color = new Color(1.0f, 0.57f, 1.0f);
            winner.color = new Color(1.0f, 0.57f, 1.0f);
        }
    }


    internal override void OnEnter(TransitionData data)
    {
        

    }

    internal override void OnExit()
    {

    }
}
