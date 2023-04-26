using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Movement : MonoBehaviour
{
    //ROBIN ZEITLIN TALAMO MADE THIS SCRIPT AND EVERYTHING WITHIN, PASS IT ON AND DEATH I SHALT BRING



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

    [SerializeField]
    AudioClip slurp;

    [Header("Meow")]
    [SerializeField]
    AudioClip[] meowClip;
    AudioSource meowSource;

    [SerializeField]
    AudioClip walking;
    [SerializeField]
    AudioSource meowWalk;

    [Header("Ledge")]
    [SerializeField]
    public static Transform target;

    [SerializeField]
    float initialAngle;

    bool jumping;

    public GameObject[] ledges;

    float nearestDistance;

    [SerializeField]
    GameObject indicator;

    [SerializeField] Animator anim;

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

    
    public void LedgeJumping()
    {
        if (target != null /*&& Vector3.Distance(target.position, transform.position) < 8 && Vector3.Distance(target.position, transform.position) > 4*/)
        {

            transform.rotation = Quaternion.LookRotation(target.position - transform.position);
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


    }

    public void Movement()
    {
        GetClosest();
        if (target != null)
        {
            if (Vector3.Distance(transform.position, target.position) < 12 && Vector3.Distance(transform.position, target.position) > 7)
                indicator.transform.position = target.position + new Vector3(0, 1, 0);
            else
                indicator.transform.position = Vector3.zero;

            indicator.transform.LookAt(Camera.main.transform.position);

        }

        bool isgrounded = Physics.CheckSphere(sphere.position, 0.4f, jumpLayer);

        if (!jumping)
        {
                if (!meowWalk.isPlaying && isgrounded && body.velocity.magnitude > 1f)
                {
                    meowWalk.pitch = (Random.Range(0.6f, 0.9f));
                    meowWalk.PlayOneShot(walking, Random.Range(0.05f, 0.15f));
                }

            nearestDistance = float.MaxValue;
            //Movement

            float movementForward = Input.GetAxis("Vertical");
            float movementSides = Input.GetAxis("Horizontal");

            Vector3 rightMove = movementSides * new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z) * currentspeed;
            Vector3 forwardMove = (movementForward * new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z) * currentspeed);

            body.velocity = rightMove + forwardMove + new Vector3(0, body.velocity.y, 0);

            

            //Rotation
            var dir = new Vector3(body.velocity.x, 0, body.velocity.z);

            anim.SetBool("IsWalking", dir.magnitude != 0);

            if (dir.magnitude > 0.3)
                transform.rotation = Quaternion.LookRotation(dir);


            
        }

        anim.SetBool("OnGround", isgrounded);

        if (isgrounded)
        {
            jumping = false;
            if (Input.GetButton("Jump") && Input.GetKey(KeyCode.LeftShift) && Vector3.Distance(transform.position, target.position) < 13 && Vector3.Distance(transform.position, target.position) > 7)
            {
                LedgeJumping();
                anim.Play("Jump2");
            }
            if (Input.GetButton("Jump"))
            {
                body.AddForce(0, normalJumpHeight * Time.deltaTime, 0);
                anim.Play("Jump2");
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Milk")
        {
            other.GetComponent<Animator>().SetBool("poof", true);
            meowWalk.PlayOneShot(slurp, 0.5f);
            S_GameManager.score += 1;
            
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