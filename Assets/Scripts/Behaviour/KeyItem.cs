using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    [SerializeField] private Item Item;
    void Pickup()
    {
        GameManager.instance.AddItem(Item);
        Destroy(gameObject);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            
            if(Input.GetKey("e"))
            {
               Pickup();
            }

        }
    }


}
