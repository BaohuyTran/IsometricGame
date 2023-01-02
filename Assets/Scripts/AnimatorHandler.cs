using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHandler : MonoBehaviour
{
    PlayerManager playerManager;
    public Animator anim;
    public InputHandler inputHandler;
    public PlayerLocomotion playerLocomotion;
    int vertical;
    int horizontal;
    public bool isRotate;

    public void Initialize()
    {
        playerManager = GetComponentInParent<PlayerManager>();
        anim = GetComponent<Animator>();
        inputHandler = GetComponentInParent<InputHandler>();
        playerLocomotion = GetComponentInParent<PlayerLocomotion>();
        vertical = Animator.StringToHash("Vertical");
        horizontal = Animator.StringToHash("Horizontal");
    }

    public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement)
    {
        float v = 0;
        float h = 0;

        #region Vertical
        if (verticalMovement > 0 && verticalMovement < 0.55f)
        {
            v = 0.5f;
        }
        else if(verticalMovement > 0.55f)
        {
            v = 1;
        }
        else if(verticalMovement < 0 && verticalMovement > -0.55f)
        {
            v = -0.5f;
        }
        else if (verticalMovement < -0.55f) 
        {
            v = -1;
        }
        #endregion
        #region Horizontal
        if (horizontalMovement > 0 && horizontalMovement < 0.55f)
        {
            v = 0.5f;
        }
        else if (horizontalMovement > 0.55f)
        {
            v = 1;
        }
        else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
        {
            v = -0.5f;
        }
        else if (horizontalMovement < -0.55f) 
        {
            v = -1;
        }
        #endregion

        anim.SetFloat(vertical, v, 0.1f, Time.deltaTime);
        anim.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
    }

    public void PlayTargetAnimation(string targetAnim, bool isInteracting)
    {
        anim.applyRootMotion = isInteracting; //roll need apply Rootmotion but rotation is not, try make it true, you will remember -_<
        anim.SetBool("isInteracting", isInteracting);
        anim.CrossFade(targetAnim, 0.2f);
    }

    public void CanRotate()
    {
        isRotate = true;
    }

    public void StopRotate()
    {
        isRotate = false;
    }

    private void OnAnimatorMove()
    {
        if(!playerManager.isInteracting)
        {
            return;
        }

        float delta = Time.deltaTime;
        playerLocomotion.rigidbody.drag = 0;
        Vector3 deltaPosition = anim.deltaPosition;
        deltaPosition.y = 0;
        Vector3 velocity = deltaPosition / delta;
        playerLocomotion.rigidbody.velocity = velocity;
    }
}
