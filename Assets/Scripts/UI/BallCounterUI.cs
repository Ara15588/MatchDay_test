using TMPro;
using UnityEngine;

public class BallCounterUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI BallsRemainingText;

    private int ballsOut = 0;
    private bool ballsLeftToSpawn = true;

    private void Start()
    {
        EventManager.AddListener<StartGameEvent>(InitializeCounter);
        EventManager.AddListener<BallSpawnedEvent>(UpdateBallsRemainingText);
        EventManager.AddListener<BallOutOfPlayEvent>(EndGameCheck);

        InitializeCounter(null);
    }

    //Reset variables and texts to initial state
    void InitializeCounter(StartGameEvent startGame)
    {
        BallsRemainingText.text = StaticData.TotalBalls.ToString();
        ballsLeftToSpawn = true;
        ballsOut = 0;
    }

    //Update counter UI texts
    void UpdateBallsRemainingText(BallSpawnedEvent ballsSpawned)
    {
        BallsRemainingText.text = ballsSpawned.BallsRemaining.ToString();
        ballsLeftToSpawn = ballsSpawned.BallsRemaining > 0;
    }

    //Trigger event of end game if conditions are met
    void EndGameCheck(BallOutOfPlayEvent ballOut)
    {
        ballsOut++;

        if (!ballsLeftToSpawn && ballsOut == StaticData.TotalBalls)
        {
            EventManager.TriggerEvent<EndGameEvent>(null);
        }
    }

}
