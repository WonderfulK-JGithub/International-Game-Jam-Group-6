using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ded : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            SceneTransition.current.ReloadScene();
        }
    }
}
