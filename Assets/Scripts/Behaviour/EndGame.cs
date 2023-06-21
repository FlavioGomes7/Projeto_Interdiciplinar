using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] private Item item;
    private bool isClose;

    void Update()
    {
        if( ( GameManager.instance.Unlock(item) == true && Input.GetKey("e") ) && isClose == true)
        {
            GameManager.instance.EndGame();
        }
    }

    public void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag("Player"))
        {
            isClose = true;    
        }

    }

     public void OnTriggerExit(Collider other)
    {

        if(other.CompareTag("Player"))
        {
            isClose = false;    
        }

    }
}
