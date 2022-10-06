using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDisabler : MonoBehaviour
{
    //Trigger event ball touched ground upon collision with the balls
    private void OnCollisionEnter2D(Collision2D collision)
    {
        BallTouchGroundEvent ballTouchedGround = new BallTouchGroundEvent(collision.gameObject);
        EventManager.TriggerEvent(ballTouchedGround);
    }
}
