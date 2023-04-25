using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    Transform MouthPos;
    Rigidbody RB;
    bool PickedUp;
    [SerializeField]
    Transform indicator;

    [SerializeField]
    AudioClip nom;
    AudioSource nomsource;

    // Start is called before the first frame update
    void Start()
    {
        MouthPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        nomsource = GetComponent<AudioSource>();
        RB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        indicator.transform.LookAt(Camera.main.transform.position);

        if (Vector3.Distance(transform.position, MouthPos.position) < 2 && PickedUp == false)
        {
            indicator.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickedUp = true;
                nomsource.PlayOneShot(nom, 0.2f);
            }
        }
        else if (Input.GetKeyDown(KeyCode.E) && PickedUp == true)
        {
            indicator.gameObject.SetActive(false);
            PickedUp = false;
        }
        else
            indicator.gameObject.SetActive(false);
        PickUp();
    }

    void PickUp()
    {
        if (PickedUp == true)
        {
            transform.position = MouthPos.position;
            transform.rotation = MouthPos.rotation;
            RB.useGravity = false;
            RB.velocity = Vector3.zero;
            RB.mass = 0;
        } else
        {
            RB.mass = 1;
            RB.useGravity = true;
        }
    }
}
