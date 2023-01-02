using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Transform targetTransform;
    private Vector3 cameraFollowVelocity = Vector3.zero;

    private Transform myTransform;
    public float followSpeed = 0.1f;

    public static CameraHandler singleton;

    private void Awake()
    {
        singleton = this;
        myTransform = transform;        
    }

    public void FollowPlayer(float delta)
    {
        myTransform.position = Vector3.SmoothDamp(myTransform.position, targetTransform.position, ref cameraFollowVelocity, delta / followSpeed);
    }
}