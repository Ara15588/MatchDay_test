using UnityEngine;
using DG.Tweening;

public class TweenTransform : MonoBehaviour
{
    Vector3 InitialPosition;

    private void Start()
    {
        InitialPosition = transform.position;
    }

    //Do change the position of the object between target position or initial position
    public void ExecuteTween(Transform target)
    {
        if(target.position != transform.position)
            transform.DOMove(target.position, 0.5f);
        else
            transform.DOMove(InitialPosition, 0.5f);
    }
}
