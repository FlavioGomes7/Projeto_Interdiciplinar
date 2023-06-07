using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject options;
    [SerializeField] private List<Item> Items = new List<Item>();
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] enemies;
   

    private void Awake()
    {
        instance = this;
    }

    //Inventory Manager
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

    // AutoAim System
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

    //Scenes Manager
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Debug.Log("Saiu");
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenOptions()
    {
        menu.SetActive(false);
        options.SetActive(true);
    }

    public void CloseOptions()
    {
        menu.SetActive(true);
        options.SetActive(false);
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
