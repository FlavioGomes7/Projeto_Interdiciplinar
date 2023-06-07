using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitchTrigger : MonoBehaviour
{
    public CinemachineVirtualCamera activeCam;

    public Transform player;


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
            activeCam.Priority = 1;

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            
           activeCam.Priority = 0;

        }
    }
}
