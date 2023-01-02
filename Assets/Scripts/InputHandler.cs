using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    PlayerControls inputActions;

    public float vertical;
    public float horizontal;
    public float moveAmount;

    public bool b_Input; //Don't know why he name this? could be rollInput??
    public bool rollFlag;

    Vector2 movementInput;

    public void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new PlayerControls();
            inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
        }
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void TickInput(float delta)
    {
        MoveInput(delta);
        RollInput(delta);
    }

    private void MoveInput(float delta)
    {
        vertical = movementInput.y;
        horizontal = movementInput.x;
        moveAmount = Mathf.Clamp01(Mathf.Abs(vertical) + Mathf.Abs(horizontal));
    }

    private void RollInput(float delta)
    {
        b_Input = inputActions.PlayerActions.Roll.IsPressed();

        if(b_Input)
        {
            rollFlag = true;
        }
    }

}
