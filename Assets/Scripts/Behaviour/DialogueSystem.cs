using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private string[] dialogues;
    private GameObject dialoguePanel; 
    public int dialogueIndex;

    public TextMeshProUGUI dialogueText;
    public bool readyToSpeak;
    public bool startDialogue;
    

    void Start()
    {
        dialoguePanel = GameObject.FindObjectOfType<Canvas>().gameObject;
        dialoguePanel.SetActive(false);
        readyToSpeak = false;
        startDialogue = false;  
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("e") && readyToSpeak && !startDialogue)
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
            FindObjectOfType<CharTankController>().speedRotation = 180f;
            FindObjectOfType<CharTankController>().speed = 3.8f;
        }
    }

    public void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        startDialogue = true;
        dialogueIndex = 0;
        StartCoroutine(ShowDialogue());
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


    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            readyToSpeak = true;    
        }
    }

    public void OnTriggerExit(Collider other)
    {

        if(other.CompareTag("Player"))
        {
            readyToSpeak = false;    
        }

    }
}
