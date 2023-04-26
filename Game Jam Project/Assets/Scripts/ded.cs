using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ded : MonoBehaviour
{
    AudioSource sos;
    private void Awake()
    {
        sos = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player2")){
            SceneTransition.current.ReloadScene();
            sos.Play();
            CameraController.current.enabled = false;
        }
    }
}
