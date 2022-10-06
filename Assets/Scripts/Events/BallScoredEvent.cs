using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScoredEvent
{
    public GameObject scoredEntity;
    public Vector2 bottomPosition;
    public BallScoredEvent(GameObject lastScoredEntity, Vector2 basketBottomPosition)
    {
        scoredEntity = lastScoredEntity;
        bottomPosition = basketBottomPosition;
    }
}