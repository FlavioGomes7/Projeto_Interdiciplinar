using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private List<Item> Items = new List<Item>();
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] enemies;
   

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
    public GameObject AutoAim()
    {
        float minDistance = Mathf.Infinity;
        float distance;
        int indexOfCloserEnemy = 0;
        for (int i = 0; i < enemies.Length; i++)
        {
   
            distance = Vector3.Distance(player.transform.position, enemies[i].transform.position);
            if (minDistance > distance)
            {
               minDistance = distance;
               indexOfCloserEnemy = i;

            }

        }
        return enemies[indexOfCloserEnemy];
    }

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
    public void Update()
    {
        


    }
    

}
