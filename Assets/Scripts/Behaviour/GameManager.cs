using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject btns;
    [SerializeField] private List<Item> Items = new List<Item>();
    [SerializeField] private Item item;
    [SerializeField] private GameObject gameObj;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] enemies;
   

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

    }

     private void Awake()
    {
        instance = this;
    }

    public void Pause()
    {
        foreach(GameObject enemy in enemies)
        {
            enemy.GetComponent<NavMeshAgent>().speed = 0;
        }
    }

    public void Resume()
    {
        foreach(GameObject enemy in enemies)
        {
            enemy.GetComponent<NavMeshAgent>().speed = 2f;
        }

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

    public bool Unlock(Item itemSO)
    {
        bool unlock = false;
        unlock = Items.Contains(itemSO);
        if(unlock == true)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
    public void Pickup()
    {
        GameManager.instance.AddItem(item);
        gameObj.SetActive(false);
    }

    public void GetItemData(Item itemData, GameObject gameobj)
    {
        item = itemData;
        gameObj = gameobj;
    }
    public void EndItemDialogue()
    {
        dialoguePanel.SetActive(false);
        btns.SetActive(false);
        Resume();
        FindObjectOfType<CharTankController>().speedRotation = 180f;
        FindObjectOfType<CharTankController>().speed = 3f;
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

    

}
