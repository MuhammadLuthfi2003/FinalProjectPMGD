using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/********************
 * DIALOGUE TRIGGER *
 ********************
 * A Dialogue Trigger can be anything, an NPC, a signpost, or even an in game event.
 * 
 * A Dialogue Trigger is responsible to activate based on some action in the game e.g. talking to an NPC
 * To handle talking to an NPC, we first attach this script to an NPC along with a dialogue file we write (e.g. .txt)
 */

public class DialogueTrigger : MonoBehaviour
{
    [Header("General Settings")]
    public TextAsset TextFileAsset; // your imported text file for your NPC
    public bool TriggerWithButton;
    public GameObject indicator;
    public TextMeshPro displayText; // found in npc
    public bool isFreezePlayer = false;

    [Header("After Decision Dialogue and Reinteraction")]
    public bool changeDialogAfterOptions = false;
    public TextAsset trueOptionDialog;
    public TextAsset falseOptionDialog;
    public bool canInteractAgain = true;

    [Header("Event when player has entered the trigger")]
    public UnityEngine.Events.UnityEvent onEnterEvent;

    [Header("Event when player has progressed the dialogue")]
    public UnityEngine.Events.UnityEvent onEventProgress;

    [Header("Event when player has exited the trigger")]
    public UnityEngine.Events.UnityEvent onExitEvent;

    // public Vector3 optionalIndicatorOffset = new Vector3 (0,0,0);
    private Queue<string> dialogue = new Queue<string>(); // stores the dialogue (Great Performance!)
    private float waitTime = 0.5f; // lag time for advancing dialogue so you can actually read it
    private float nextTime = 0f; // used with waitTime to create a timer system
    private bool dialogueTiggered;
    private bool firstDialogFlag = true;


    private bool isNear = false;
    private DialogueManager dialogueManager;
    [HideInInspector] public bool hasChosenOption = false;
    [HideInInspector] public bool chosenDecision = false;
    [HideInInspector] public bool hasExited = false;
    //private GameObject indicator;

    // public bool useCollision; // unused for now


    private void Update()
    {
        if (canInteractAgain)
        {
            if (isNear)
            {
                if (TriggerWithButton && firstDialogFlag)
                {
                    displayText.enabled = true;

                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        TriggerDialogue();
                        firstDialogFlag = false;
                        displayText.enabled = false;
                    }
                }

                OutputLine();
            }
            if (!isNear)
            {
                displayText.enabled = false;
            }
        }
        else
        {
            if (isNear)
            {
                if (TriggerWithButton && firstDialogFlag && !hasExited)
                {
                    displayText.enabled = true;

                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        TriggerDialogue();
                        firstDialogFlag = false;
                        displayText.enabled = false;
                    }
                }

                OutputLine();
            }
            if (!isNear)
            {
                displayText.enabled = false;
            }
        }
    }

    /* Called when you want to start dialogue */
    void TriggerDialogue()
    {
        onEnterEvent.Invoke();
        DialogueManager.instance.currentInteractableObject = this;
        ReadTextFile(); // loads in the text file
        dialogueManager = DialogueManager.instance; // gets the dialogue manager
        dialogueManager.StartDialogue(dialogue); // Accesses Dialogue Manager and Starts Dialogue
    }

    /* loads in your text file */
    private void ReadTextFile()
    {
        string txt = TextFileAsset.text;

        if (changeDialogAfterOptions && hasChosenOption)
        {
            if (chosenDecision)
            {
                txt = trueOptionDialog.text;
            }
            else
            {
                txt = falseOptionDialog.text;
            }
        }

        string[] lines = txt.Split(System.Environment.NewLine.ToCharArray()); // Split dialogue lines by newline

        foreach (string line in lines) // for every line of dialogue
        {
            if (!string.IsNullOrEmpty(line))// ignore empty lines of dialogue
            {
                if (line.StartsWith("[NAME")) // e.g [NAME=Michael] Hello, my name is Michael
                {
                    string special = line.Substring(0, line.IndexOf(']') + 1); // special = [NAME=Michael]
                    string curr = line.Substring(line.IndexOf(']') + 1); // curr = Hello, ...
                    dialogue.Enqueue(special); // adds to the dialogue to be printed
                    dialogue.Enqueue(curr);
                }
                else if (line.StartsWith("[OPTIONS"))
                {
                    string options = line.Substring(line.IndexOf("]") + 1);
                    dialogue.Enqueue(options);
                    //dialogueManager.isQuestion = true;
                }
                else
                {
                    dialogue.Enqueue(line); // adds to the dialogue to be printed
                }
            }
        }
        dialogue.Enqueue("EndQueue");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isNear = true;
            if (!TriggerWithButton)
            {
                TriggerDialogue();
            }
            // Debug.Log("Collision");
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            if (!dialogueTiggered)
            {
                if (indicator != null && indicator.activeSelf == false)
                {
                    indicator.SetActive(true);
                }
            }

        }
    }

    private void OutputLine()
    {
        if (Input.GetKeyDown(KeyCode.F) && nextTime < Time.timeSinceLevelLoad && !dialogueManager.isQuestion && dialogueManager.isOpen)
        {
            if (!dialogueTiggered)
            {
                TriggerDialogue();
                dialogueTiggered = true;
                if (indicator != null && indicator.activeSelf == true)
                {
                    indicator.SetActive(false);
                }
                nextTime = Time.timeSinceLevelLoad + waitTime;
            }
            else
            {
                nextTime = Time.timeSinceLevelLoad + waitTime;
                DialogueManager.instance.AdvanceDialogue();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isNear = false;
            DialogueManager.instance.currentInteractableObject = null;
            firstDialogFlag = true;
            DialogueManager.instance.EndDialogue();
            dialogueTiggered = false;
            onExitEvent.Invoke();
            if (indicator != null && indicator.activeSelf == true)
            {
                indicator.SetActive(false);
            }
        }
    }
}