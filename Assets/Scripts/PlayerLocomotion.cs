using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    Transform cameraObject;
    InputHandler inputHandler;
    AnimatorHandler animatorHandler;

    Vector3 moveDiretcion;
    Vector3 targetDirrection;

    [HideInInspector] public Transform myTransform;
    public new Rigidbody rigidbody;

    [Header("Movement stat")]
    public float movementSpeed = 5;
    public float rotationSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        cameraObject = Camera.main.transform;
        inputHandler = GetComponent<InputHandler>();
        animatorHandler = GetComponentInChildren<AnimatorHandler>();
        rigidbody = GetComponent<Rigidbody>();
        myTransform = transform;
        animatorHandler.Initialize();
    }

    #region Movement
    Vector3 normalVector; //this var may have more value in the future, let's see
    public void HandleMovement(float delta)
    {
        if(animatorHandler.isRotate)
        {
            HandleRotation(delta);
        }

        float speed = movementSpeed;

        moveDiretcion = cameraObject.forward * inputHandler.vertical;
        moveDiretcion += cameraObject.right * inputHandler.horizontal;
        moveDiretcion.Normalize();
        moveDiretcion.y = 0;
        
        moveDiretcion *= speed;

        Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDiretcion, normalVector);
        rigidbody.velocity = projectedVelocity;

        animatorHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0);
    }

    public void HandleRotation(float delta)
    {
        targetDirrection = Vector3.zero;

        targetDirrection = cameraObject.forward * inputHandler.vertical;
        targetDirrection += cameraObject.right * inputHandler.horizontal;
        targetDirrection.Normalize();
        targetDirrection.y = 0;

        if(targetDirrection == Vector3.zero)
        {
            targetDirrection = myTransform.forward;
        }

        float speed = rotationSpeed;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirrection);

        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, targetRotation, speed * delta);
    }

    public void HandleRolling(float delta)
    {
        if (animatorHandler.anim.GetBool("isInteracting"))
        {
            return;
        }

        if(inputHandler.rollFlag)
        {
            moveDiretcion = cameraObject.forward * inputHandler.vertical;
            moveDiretcion += cameraObject.right * inputHandler.horizontal;

            if(inputHandler.moveAmount == 0)
            {
                moveDiretcion = myTransform.forward;
            }

            animatorHandler.PlayTargetAnimation("Rolling", true);
            moveDiretcion.y = 0;
            myTransform.rotation = Quaternion.LookRotation(moveDiretcion);
        }
    }
    #endregion
}
