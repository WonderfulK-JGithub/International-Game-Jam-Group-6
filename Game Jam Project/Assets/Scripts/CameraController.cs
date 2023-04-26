using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController current;

    [SerializeField] float mouseSence = 3f;

    float rotationX;
    float rotationY;

    [SerializeField] Transform target;

    [SerializeField] float distanceFromTarget = 5f;

    Vector3 currentRotation;
    Vector3 smoothVelocity = Vector3.zero;

    [SerializeField] float smoothTime;
    [SerializeField] bool hideMouse;
    [SerializeField] LayerMask avoidLayers;
    [SerializeField] float smallOffset;

    private void Awake()
    {
        current = this;
        if (hideMouse) Cursor.lockState = CursorLockMode.Locked; 
    }


    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        rotationX += mouseX * mouseSence;
        rotationY += mouseY * -mouseSence;

        rotationY = Mathf.Clamp(rotationY, 0, 80f);

        Vector3 nextRotation = new Vector3(rotationX, rotationY);

        currentRotation = Vector3.SmoothDamp(currentRotation, nextRotation, ref smoothVelocity, smoothTime);
    }

    void FixedUpdate()
    {
        transform.localEulerAngles = new Vector3(currentRotation.y, currentRotation.x, 0f);

        Vector3 _cameraPos = target.position - transform.forward * distanceFromTarget;

        if (Physics.Raycast(target.position,-transform.forward,out RaycastHit _hit,distanceFromTarget,avoidLayers))
        {
            _cameraPos = _hit.point + transform.forward * smallOffset;
        }

        transform.position = _cameraPos;
    }
}