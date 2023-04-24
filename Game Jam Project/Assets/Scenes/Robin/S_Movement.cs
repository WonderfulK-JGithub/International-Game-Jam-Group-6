using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Movement : MonoBehaviour
{
    [Header("Player/Stats")]

    Rigidbody body;

    public float speed;
    public float jumpheight;

    [SerializeField]
    Transform sphere;

    [SerializeField]
    LayerMask jumpLayer;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        float movementForward = Input.GetAxis("Vertical");
        float movementSides = Input.GetAxis("Horizontal");

        //Movement
        Vector3 rightMove = movementSides * new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z) * speed;
        Vector3 forwardMove = (movementForward * new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z) * speed);

        body.velocity = rightMove + forwardMove + new Vector3(0, body.velocity.y, 0);

        var dir = new Vector3(body.velocity.x, 0, body.velocity.z);
        if (dir.magnitude > 0.3)
            transform.rotation = Quaternion.LookRotation(dir);

        if (Input.GetButton("Jump") && Physics.CheckSphere(sphere.position, 1, jumpLayer))
        {
            body.AddForce(new Vector3(0, jumpheight, 0));
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }
}
