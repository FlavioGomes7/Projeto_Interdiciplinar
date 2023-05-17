using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private List<Item> Items = new List<Item>();
    [SerializeField] private int hpmax;
    [SerializeField] private int hpNow;
    [SerializeField] private int damage;


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

    public void TakeDamage(int damage)
    {
        hpNow -= damage;  
    }
    public void RecorverHealth(int healthrecorver)
    {
        hpNow += healthrecorver;
    }
    public void IncreseHealthMax(int healthIncrese)
    {
        hpmax += healthIncrese;
    }
    public void GiveDamage(int hp)
    {
        hp -= damage;
    }



}
