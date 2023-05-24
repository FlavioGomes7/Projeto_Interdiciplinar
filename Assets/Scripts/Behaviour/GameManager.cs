using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private List<Item> Items = new List<Item>();
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] enemys;
   

    private void Awake()
    {
        instance = this;
    }

    public void AddItem(Item item)
    {
        Items.Add(item);
    }
    
    public void RemoveItem(Item item)
    {
        Items.Remove(item);
    }

    public void Unlock(Item itemSO, GameObject game)
    {
        bool unlock = false;
        unlock = Items.Contains(itemSO);
        if(unlock == true)
        {
            Destroy(game);
        }
        
    }
    public GameObject AutoAim(GameObject[] enemys)
    {
        
    }

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
    }
    

}
