using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class DoorSystem : MonoBehaviour
{
    //Transição
    [SerializeField] private VideoPlayer video;
    [SerializeField] private GameObject render;
    [SerializeField] private Transform Spawn;
    [SerializeField] private Transform player;

    [SerializeField] private int index;

    //Sistema de Liberação
    [SerializeField] private Item item;
    [SerializeField] private bool isLocked;
    [SerializeField] private bool isClose;

    //Aviso
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private string warning;
    [SerializeField] private bool startWarning;


    



    // Start is called before the first frame update
    void Start()
    {
        render.SetActive(false);
        dialoguePanel.SetActive(false);
        startWarning = false;
    }
    void Update()
    {
        if(isClose)
        {
            if((Input.GetKey("e") && !isLocked) && !startWarning)
            {
                TakeToTheLocal();
            }
            else if((Input.GetKey("e") && isLocked) && !startWarning)
            {
                if(GameManager.instance.Unlock(item) == true)
                {
                    TakeToTheLocal();
                }
                else
                {
                    Warning();
                }

            }
        }

    }

   void OnTriggerEnter(Collider other)
   {
        if(other.CompareTag("Player"))
        {
            isClose = true;
        }
   }
   void OnTriggerExit(Collider other)
   {
         if(other.CompareTag("Player"))
        {
            isClose = false;
        }
   }

    /*Troca De Cena
   public void LoadNextLevel()
   {
        render.SetActive(true);
        StartCoroutine(LoadLevel(index));
   }

   public IEnumerator LoadLevel(int index)
   {
        video.Play();
        yield return new WaitForSeconds(7.0f);
        video.Stop();
        render.SetActive(false);
        SceneManager.LoadScene(index);
   }*/

   //Troca de posição
    public void TakeToTheLocal()
    {
        render.SetActive(true);
        StartCoroutine(NewLocal());
    }

     public IEnumerator NewLocal()
    {
        video.Play();
        yield return new WaitForSeconds(7.0f);
        video.Stop();
        render.SetActive(false);
        player.position = Spawn.position;
    }



    // Aviso
   public void Warning()
   {
        dialoguePanel.SetActive(true);
        FindObjectOfType<CharTankController>().speedRotation = 0f;
        FindObjectOfType<CharTankController>().speed = 0f;
        startWarning = true;
        StartCoroutine(WarningDialogue());
   }

   IEnumerator WarningDialogue()
    {
        warningText.text = "";
        GameManager.instance.Pause();
        foreach(char Letter in warning)
        {
            warningText.text += Letter;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(2f);
        dialoguePanel.SetActive(false);
        GameManager.instance.Resume();
        FindObjectOfType<CharTankController>().speedRotation = 180f;
        FindObjectOfType<CharTankController>().speed = 3f;
        startWarning = false;
    }
}
