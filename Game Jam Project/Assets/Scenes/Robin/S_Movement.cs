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
    public float currentspeed;
    public float normalJumpHeight;

    [Header("InteractAbles")]
    [SerializeField]
    float Trampoline;

    [Header("Meow")]
    [SerializeField]
    AudioClip[] meowClip;
    AudioSource meowSource;

    [Header("Ledge")]
    [SerializeField]
    public static Transform target;

    [SerializeField]
    float initialAngle;

    bool jumping;

    public GameObject[] ledges;

    float nearestDistance;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        meowSource = GetComponent<AudioSource>();
        jumping = false;

        ledges = GameObject.FindGameObjectsWithTag("Ledge");
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Meow();
    }

    void GetClosest()
    {
        foreach (GameObject ledge in ledges)
        {
            if (Vector3.Distance(transform.position, ledge.transform.position) < nearestDistance)
            {
                nearestDistance = Vector3.Distance(transform.position, ledge.transform.position);
                target = ledge.transform;
            }
        }
       /* foreach (GameObject ledge in ledges)
            if (ledge.transform != target.transform)
                ledge.transform.GetChild(0).gameObject.SetActive(false);
            else
                ledge.transform.GetChild(0).gameObject.SetActive(true);
       */
    }
    public void LedgeJumping()
    {

        GetClosest();

        if (target != null /*&& Vector3.Distance(target.position, transform.position) < 8 && Vector3.Distance(target.position, transform.position) > 4*/)
        {
            //transform.position = Vector3.MoveTowards(transform.position, target.position, 1 * Time.deltaTime);
            jumping = true;

            var rigid = GetComponent<Rigidbody>();

            Vector3 p = target.position;

            float gravity = Physics.gravity.magnitude;
            // Selected angle in radians
            float angle = initialAngle * Mathf.Deg2Rad;

            // Positions of this object and the target on the same plane
            Vector3 planarTarget = new Vector3(p.x, 0, p.z);
            Vector3 planarPostion = new Vector3(transform.position.x, 0, transform.position.z);

            // Planar distance between objects
            float distance = Vector3.Distance(planarTarget, planarPostion);
            // Distance along the y axis between objects
            float yOffset = transform.position.y - p.y;

            float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

            Vector3 velocity = new Vector3(0, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

            // Rotate our velocity to match the direction between the two objects
            float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarTarget - planarPostion) * (p.x > transform.position.x ? 1 : -1);
            Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

            // Fire!
            rigid.velocity = finalVelocity * 1.4f;
            
        }
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
        bool isgrounded = Physics.CheckSphere(sphere.position, 0.4f, jumpLayer);
        if (!jumping)
        {
            //Movement
           
            float movementForward = Input.GetAxis("Vertical");
            float movementSides = Input.GetAxis("Horizontal");

            Vector3 rightMove = movementSides * new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z) * currentspeed;
            Vector3 forwardMove = (movementForward * new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z) * currentspeed);

            body.velocity = rightMove + forwardMove + new Vector3(0, body.velocity.y, 0);

            //Rotation
            var dir = new Vector3(body.velocity.x, 0, body.velocity.z);
            if (dir.magnitude > 0.3)
                transform.rotation = Quaternion.LookRotation(dir);


            
        }
        if (isgrounded)
        {
            jumping = false;
            if (Input.GetButton("Jump") && Input.GetKey(KeyCode.LeftShift))
            {
                nearestDistance = float.MaxValue;
                LedgeJumping();
            }
            if (Input.GetButton("Jump"))
            {
                body.AddForce(0, normalJumpHeight, 0);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name == "Trampoline")
        {
            body.AddForce(new Vector3(0, Trampoline, 0));

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ledge")
        {
            jumping = false;

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