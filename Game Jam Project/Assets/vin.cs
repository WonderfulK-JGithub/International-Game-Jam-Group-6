using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vin : MonoBehaviour
{
    [SerializeField] GameObject cat;
    [SerializeField] GameObject cat2;
    [SerializeField] GameObject winCamera;
    [SerializeField] GameObject plejer;
    [SerializeField] GameObject ket;

    [SerializeField] Animator anim;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player2")) return;

        GetComponent<CapsuleCollider>().enabled = false;

        cat.SetActive(true);
        cat2.SetActive(true);
        anim.Play("Win_puss");
        ket.SetActive(false);

        plejer.SetActive(false);

        winCamera.SetActive(true);
        CameraController.current.gameObject.SetActive(false);
    }
}
