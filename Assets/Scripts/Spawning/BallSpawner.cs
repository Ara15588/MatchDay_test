using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject BallsParent;
    [SerializeField]
    private float SpawnInterval;
    [SerializeField]
    private float spawnAreaMaxHeight;
    [SerializeField]
    private float spawnAreaMinHeight;
    [SerializeField]
    private float spawnAreaMaxWidth;
    [SerializeField]
    private float spawnAreaMinWidth;

    private int spawnedBalls = 0;

    void Start()
    {
        EventManager.AddListener<StartGameEvent>(ResetSpawner);
        CalculatePlayArea();
    }

    void ResetSpawner(StartGameEvent startGame)
    {
        spawnedBalls = 0;
        InvokeRepeating("SpawnBall", SpawnInterval, SpawnInterval);
    }

    //Spawn balls on random positions
    void SpawnBall()
    {
        if (spawnedBalls < StaticData.TotalBalls)
        {
            GameObject ball = PoolingSystem.Instance.InstantiateAPS(StaticData.Ball, GetRandomPosition(), Quaternion.identity, BallsParent, null);
            ball.SetActive(true);
            ball.transform.localScale = Vector3.one;

            spawnedBalls++;

            BallSpawnedEvent spawnedBall = new BallSpawnedEvent(StaticData.TotalBalls - spawnedBalls);
            EventManager.TriggerEvent<BallSpawnedEvent>(spawnedBall);
        }
        else
        {
            CancelInvoke();
        }
    }

    //Calculate the spawning area according to screen resolution
    void CalculatePlayArea()
    {
        spawnAreaMaxHeight = Screen.height - Screen.height / 10f;
        spawnAreaMinHeight = Screen.height / 1.3f;

        spawnAreaMaxWidth = Screen.width - Screen.width / 20f;
        spawnAreaMinWidth = Screen.width / 20f;
    }

    //Get random position inside playable area
    Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(spawnAreaMinWidth, spawnAreaMaxWidth), Random.Range(spawnAreaMinHeight, spawnAreaMaxHeight), 0f);
    }
}
