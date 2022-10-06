using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    [SerializeField]
    private Transform BasketBottom;

    //Trigger event of ball scoring
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(StaticData.Ball) && collision.transform.position.y > transform.position.y)
        {
            BallScoredEvent scoreUpdate = new BallScoredEvent(collision.gameObject, BasketBottom.position);
            EventManager.TriggerEvent(scoreUpdate);


            DragBallWithBasket(collision.transform);
        }
    }

    //Sets the scoring ball as child of the gameobject so it can follow the transform movement
    void DragBallWithBasket(Transform ball)
    {
        ball.SetParent(transform);
        ball.transform.SetAsFirstSibling();
    }

}
