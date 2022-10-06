﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI ScoreText;
    [SerializeField]
    private TextMeshProUGUI RecordText;

    [SerializeField]
    private GameObject TotalScore;
    [SerializeField]
    private GameObject NewRecordPanel;

    private int currentScore;
    private int currentRecord;

    void Start()
    {
        EventManager.AddListener<StartGameEvent>(InitializeScore);
        EventManager.AddListener<BallScoredEvent>(UpdateScoreText);
        EventManager.AddListener<EndGameEvent>(SaveAndShowRecord);

        currentRecord = PlayerPrefs.GetInt(StaticData.CurrentRecord, 0);
        RecordText.text = currentRecord.ToString();
    }


    void InitializeScore(StartGameEvent startGame)
    {
        currentScore = 0;
        ScoreText.text = currentScore.ToString();
    }

    //Increases the score amount and updates the text property
    void UpdateScoreText(BallScoredEvent scoreUpdate)
    {
        currentScore++;
        ScoreText.text = currentScore.ToString();
    }

    //Shows the record panel and saves the current record
    void SaveAndShowRecord(EndGameEvent endGame)
    {
        if (currentScore > currentRecord) {
            currentRecord = currentScore;
            PlayerPrefs.SetInt(StaticData.CurrentRecord, currentRecord);
            PlayerPrefs.Save();

            NewRecordPanel.SetActive(true);
        }

        RecordText.text = currentRecord.ToString();
        TotalScore.SetActive(true);
    }

}
