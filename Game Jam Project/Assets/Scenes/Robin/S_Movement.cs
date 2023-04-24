using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Movement : MonoBehaviour
{
    [Header("Player/Stats")]
    Rigidbody body;
    [SerializeField]
    Transform sphere;
    [SerializeField]
    LayerMask jumpLayer;

    public float speed;
    public float runningspeed;
    public float currentspeed;
    float currentJumpHeight;
    public float normalJumpHeight;

    [Header("InteractAbles")]
    [SerializeField]
    float Trampoline;

    [Header("Meow")]
    [SerializeField]
    AudioClip[] meowClip;
    AudioSource meowSource;



    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        meowSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Meow();
    }

    public void Meow()
    {
        if (Input.GetKeyDown(KeyCode.F) && !meowSource.isPlaying)
        {
            meowSource.PlayOneShot(meowClip[Random.Range(0,meowClip.Length)], 0.5f);
            
        }
    }
    public void Movement()
    {
        /*if (Input.GetKey(KeyCode.LeftShift))
            currentspeed = Mathf.Lerp(speed, runningspeed, 10f);
        else
            currentspeed = Mathf.Lerp(runningspeed, speed, 10f);
        */
        //Movement
        bool isgrounded = Physics.CheckSphere(sphere.position, 0.4f, jumpLayer);
        float movementForward = Input.GetAxis("Vertical");
        float movementSides = Input.GetAxis("Horizontal");

        Vector3 rightMove = movementSides * new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z) * currentspeed;
        Vector3 forwardMove = (movementForward * new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z) * currentspeed);

        body.velocity = rightMove + forwardMove + new Vector3(0, body.velocity.y, 0);

        //Rotation
        var dir = new Vector3(body.velocity.x, 0, body.velocity.z);
        if (dir.magnitude > 0.3)
            transform.rotation = Quaternion.LookRotation(dir);

        if (Input.GetButton("Jump") && isgrounded)
        {
            body.AddForce(new Vector3(0, normalJumpHeight, 0));
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name == "Trampoline")
        {
            body.AddForce(new Vector3(0, Trampoline, 0));

        }
    }
}


//Movement
/*if (isgrounded)
{
    body.velocity = rightMove + forwardMove + new Vector3(0, body.velocity.y, 0);
} else
{
    body.velocity = forwardMove + new Vector3(0, body.velocity.y, 0);
}*/