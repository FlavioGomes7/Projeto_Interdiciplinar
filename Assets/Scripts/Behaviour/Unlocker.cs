using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlocker : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private GameObject game;

     void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            
            if(Input.GetKey("e"))
            {
               GameManager.instance.Unlock(item);
            }

        }
    }

}
