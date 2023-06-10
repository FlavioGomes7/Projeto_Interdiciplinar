using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private string[] dialogues;
    [SerializeField] private string itemDescription;
    [SerializeField] private Item item;
    [SerializeField] private GameObject dialoguePanel; 
    public int dialogueIndex;

    public GameObject btns;


    public TextMeshProUGUI dialogueText;
    public bool readyToInteract;
    public bool startDialogue;
    public bool isItem;
    

    void Start()
    {
        dialoguePanel.SetActive(false);
        btns.SetActive(false);
        readyToInteract = false;
        startDialogue = false;  
    }

    // Update is called once per frame
    void Update()
    {
        if(isItem == false)
        {
            if(Input.GetKey("e") && readyToInteract && !startDialogue)
            {

                FindObjectOfType<CharTankController>().speedRotation = 0f;
                FindObjectOfType<CharTankController>().speed = 0f;
                StartDialogue();

            }
            else if(dialogueText.text == dialogues[dialogueIndex] && Input.GetButton("Fire1"))
            {
                NextDialogue();
            }
        
        }
        else
        {
            if(Input.GetKey("e") && readyToInteract && !startDialogue)
            {
                FindObjectOfType<CharTankController>().speedRotation = 0f;
                FindObjectOfType<CharTankController>().speed = 0f;
                StartItemDialogue();
            }
            else if(dialogueText.text == itemDescription)
            {
                startDialogue = false;
            }

        }
        
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            readyToInteract = true;    
        }
        
        if(other.CompareTag("Player") && gameObject.CompareTag("KeyItem"))
        {
            GameManager.instance.GetItemData(item, gameObject);
        }
    }

    public void OnTriggerExit(Collider other)
    {

        if(other.CompareTag("Player"))
        {
            readyToInteract = false;    
        }

    }

    public void NextDialogue()
    {
        dialogueIndex++;

        if(dialogueIndex < dialogues.Length)
        {
            StartCoroutine(ShowDialogue());
        }
        else
        {
            dialoguePanel.SetActive(false);
            startDialogue = false;
            dialogueIndex = 0;
            GameManager.instance.Resume();
            FindObjectOfType<CharTankController>().speedRotation = 180f;
            FindObjectOfType<CharTankController>().speed = 3f;
        }
    }

    public void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        btns.SetActive(false);
        startDialogue = true;
        dialogueIndex = 0;
        GameManager.instance.Pause();
        StartCoroutine(ShowDialogue());
    }

    public void StartItemDialogue()
    {
        dialoguePanel.SetActive(true);
        btns.SetActive(true);
        startDialogue = true;
        GameManager.instance.Pause();
        StartCoroutine(ShowItemDialogue());
    }



    IEnumerator ShowDialogue()
    {
        dialogueText.text = "";
        foreach(char Letter in dialogues[dialogueIndex])
        {
            dialogueText.text += Letter;
            yield return new WaitForSeconds(0.05f);
        }
    }
    IEnumerator ShowItemDialogue()
    {
        dialogueText.text = "";
        foreach(char Letter in itemDescription)
        {
            dialogueText.text += Letter;
            yield return new WaitForSeconds(0.05f);
        }

    }

    
}
