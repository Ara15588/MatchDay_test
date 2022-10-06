using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private Image BallImage;
    [SerializeField]
    private Collider2D OwnCollider;
    [SerializeField]
    private Rigidbody2D OwnRigidbody;

    private int groundTouchesLeft = 3;

    private void OnEnable()
    {
        EventManager.AddListener<BallScoredEvent>(CheckBallScored);
        EventManager.AddListener<BallTouchGroundEvent>(CheckBallGrounded);

        InitializeBall();
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<BallScoredEvent>(CheckBallScored);
        EventManager.RemoveListener<BallTouchGroundEvent>(CheckBallGrounded);
        OwnRigidbody.velocity = Vector2.zero;
    }


    //Setting the state of the ball as it's a new ball
    void InitializeBall()
    {
        groundTouchesLeft = StaticData.maxGroundTouches;
        BallImage.color = StaticData.BallBounceColors[StaticData.BallBounceColors.Count - 1];
        OwnCollider.enabled = true;
        OwnRigidbody.velocity = new Vector2(UnityEngine.Random.Range(-100f, 100f), UnityEngine.Random.Range(-100f, 200f));
    }

    //Modification of the ball state behaviour when scoring
    void CheckBallScored(BallScoredEvent scoreUpdate)
    {
        if (scoreUpdate.scoredEntity == gameObject)
        {
            BallImage.color = StaticData.BallBounceColors[StaticData.BallBounceColors.Count - 1];
            SendBallOutOfPlayArea(scoreUpdate.bottomPosition);
        }
    }

    //Modification of the ball state when touched the bottom bar
    void CheckBallGrounded(BallTouchGroundEvent scoreUpdate)
    {
        if (scoreUpdate.ball == gameObject)
        {
            groundTouchesLeft = groundTouchesLeft > 0 ? groundTouchesLeft - 1  : 0;
             
            BallImage.color = StaticData.BallBounceColors[groundTouchesLeft];

            if (groundTouchesLeft == 0)
            {
                SendBallOutOfPlayArea((Vector2)transform.position + Vector2.down);
            }
        }
    }


    //Ball is sent downwards when scoring or eliminated
    void SendBallOutOfPlayArea(Vector2 directionDown)
    {
        OwnCollider.enabled = false;
        OwnRigidbody.velocity = (directionDown - (Vector2)transform.position) * 3;
        DelayedDeactivation(() => { gameObject.SetActive(false); }, new TimeSpan(0, 0, 1));
        EventManager.TriggerEvent<BallOutOfPlayEvent>(null);
    }

    //Sets an async Action to be executed after a delay
    public async Task DelayedDeactivation(Action action, TimeSpan delay)
    {
        await Task.Delay(delay);
        action.Invoke();
    }
}
