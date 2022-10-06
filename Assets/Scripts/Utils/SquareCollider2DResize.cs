using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareCollider2DResize : MonoBehaviour
{
    private void Start()
    {
        ResizeCollider();
    }


    //Resize of collider bounds according to the rect transform attached to this gameobject
    void ResizeCollider()
    {
        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        RectTransform rectTransform = GetComponent<RectTransform>();
        var rect = rectTransform.rect;
        boxCollider2D.size = new Vector2(rect.width, rect.height);
    }
}
