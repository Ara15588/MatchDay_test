using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private Transform inputTarget;
    [SerializeField]
    private Rigidbody2D rigidBodyTarget;

    [SerializeField]
    private float inputSensitivity = 500;

    public float minimumX;
    public float maximumX;

    private int lastFrameDirection = 0;

    private void Start()
    {
        EventManager.AddListener<StartGameEvent>(ResetTargetPosition);
        
        SetInputConstraints();
    }

    //Set the target on int initial position when starting the game
    void ResetTargetPosition(StartGameEvent startGame)
    {
        SetInputConstraints();

        inputTarget.position = new Vector3(Screen.width/2f, inputTarget.position.y, 0f);
    }


    // Update is called once per frame
    void Update()
    {
        MoveTarget();
    }

    //Target position moved horizontally based on input position
    void MoveTarget()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 NewPosition = Input.mousePosition;
            Vector3 pos = inputTarget.position;
            
            if (NewPosition.x > pos.x)
            {
                lastFrameDirection = 1;
            }
            else if (NewPosition.x < pos.x)
            {
                lastFrameDirection = -1;
            }
            
            if (EventSystem.current.IsPointerOverGameObject() || inputTarget.position.x < minimumX && lastFrameDirection == -1 || inputTarget.position.x > maximumX && lastFrameDirection == 1
                || Mathf.Abs(inputTarget.position.x - NewPosition.x) < 10f)
                lastFrameDirection = 0;


            rigidBodyTarget.velocity = inputSensitivity * lastFrameDirection * inputTarget.right;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            rigidBodyTarget.velocity = Vector2.zero;
        }
    }

    //Caltulate the limits area to move the target
    void SetInputConstraints()
    {
        minimumX = Screen.width / 10;
        maximumX = Screen.width - minimumX;

        inputSensitivity = 300 * Screen.width / 1000f;
    }
}
