using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchTrigger : MonoBehaviour
{
    public GameObject cameraA;
    public GameObject cameraB;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
            cameraA.SetActive(false);
            cameraB.SetActive(true);

        }
    }
}
