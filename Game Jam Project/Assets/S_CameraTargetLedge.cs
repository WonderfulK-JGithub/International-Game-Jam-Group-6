using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CameraTargetLedge : MonoBehaviour
{
    [SerializeField]
    LayerMask mask;

    
    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
       
        /*RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, mask))
        {
            S_Movement.target = hit.transform;
            print(hit.transform);
        }

        Debug.DrawRay(transform.position, transform.forward, Color.red);
        */
    }
}
