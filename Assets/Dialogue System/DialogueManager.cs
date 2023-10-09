using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static System.Net.Mime.MediaTypeNames;

/********************
 * DIALOGUE MANAGER *
 ********************
 * This Dialogue Manager is what links your dialogue which is sent by the Dialogue Trigger to Unity
 *
 * The Dialogue Manager navigates the sent text and prints it to text objects in the canvas and will toggle
 * the Dialogue Box when appropriate
 */

public class DialogueManager : MonoBehaviour
{
    public GameObject CanvasBox; // your fancy canvas box that holds your text objects
    public UnityEngine.UI.Text TextBox; // the text body
    public UnityEngine.UI.Text NameText; // the text body of the name you want to display
    public GameObject OptionsBox;
    public UnityEngine.UI.Text yesBtnText;
    public UnityEngine.UI.Text noBtnText;
    

    public bool freezePlayerOnDialogue = true;


    // private bool isOpen; // represents if the dialogue box is open or closed

    private Queue<string> inputStream = new Queue<string>(); // stores dialogue
    public bool isQuestion = false;
    private int optionRemaining = 0;
    //private PlayerAnimController animController;

    private void Start()
    {
        CanvasBox.SetActive(false); // close the dialogue box on play
        OptionsBox.SetActive(false);
        //[TODO] : Add code to find player animator controller
        //animController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAnimController>();
    }

    private void Update()
    {
        if (!isQuestion)
        {
            OptionsBox.SetActive(false);
        }
        else if (isQuestion)
        {
            OptionsBox.SetActive(true);
        }
    }

    private void DisablePlayerController()
    {
        // todo : add function to make player idle
        //animController.ForceIdle();
        //animController.enabled = false;
    }

    private void EnablePlayerController()
    {
        // todo : add line to reenable player animator
        //animController.enabled = true;
    }

    public void StartDialogue(Queue<string> dialogue)
    {
        if (freezePlayerOnDialogue)
        {
            DisablePlayerController();
        }

        CanvasBox.SetActive(true); // open the dialogue box

        // isOpen = true;
        inputStream = dialogue; // store the dialogue from dialogue trigger
        PrintDialogue(); // Prints out the first line of dialogue
    }

    public void AdvanceDialogue() // call when a player presses a button in Dialogue Trigger
    {
        PrintDialogue();
    }

    private void PrintDialogue()
    {
        if (optionRemaining <= 0)
        {
            isQuestion = false;
        }

        if (!isQuestion)
        {
            if (inputStream.Count == 0 || inputStream.Peek().Contains("EndQueue")) // special phrase to stop dialogue
            {
                inputStream.Dequeue(); // Clear Queue
                EndDialogue();
            }
            else if (inputStream.Peek().Contains("[NAME="))
            {
                string name = inputStream.Peek();
                name = inputStream.Dequeue().Substring(name.IndexOf('=') + 1, name.IndexOf(']') - (name.IndexOf('=') + 1));
                NameText.text = name;
                PrintDialogue(); // print the rest of this line
            }
            else if (inputStream.Peek().Contains("[CHOICE"))
            {
                isQuestion = true;
                SetOptions();
            }
            else if (inputStream.Peek().Contains("{Y}") || inputStream.Peek().Contains("{N}"))
            {
                inputStream.Dequeue();
            }
            else
            {
                TextBox.text = inputStream.Dequeue();
            }
        }

    }

    public void SetOptions()
    {
        if (inputStream.Peek().Contains("[CHOICE"))
        {
            if (inputStream.Peek().Contains("[CHOICE=Y"))
            {
                string text = inputStream.Peek();
                text = inputStream.Dequeue().Substring(text.IndexOf("]") + 1).ToString();
                yesBtnText.text = text;
            }
            if (inputStream.Peek().Contains("[CHOICE=N"))
            {
                string text = inputStream.Peek();
                text = inputStream.Dequeue().Substring(text.IndexOf("]") + 1).ToString();
                noBtnText.text = text;
            }
        }
    }

    public void PrintDecision(bool isYesOption)
    {
        isQuestion = false;

        if (isYesOption)
        {
            if (inputStream.Peek().Contains("{Y}"))
            {
                string text = inputStream.Peek();
                text = inputStream.Dequeue().Substring(text.IndexOf("}") + 1).ToString();
                TextBox.text = text;
                PrintDialogue();
            }
        }
        else
        {
            if (inputStream.Peek().Contains("{N}"))
            {
                string text = inputStream.Peek();
                text = inputStream.Dequeue().Substring(text.IndexOf("}") + 1).ToString();
                TextBox.text = text;
                PrintDialogue();
            }
        }
        
    }

    public void EndDialogue()
    {
        TextBox.text = "";
        NameText.text = "";
        inputStream.Clear();
        CanvasBox.SetActive(false);
        // isOpen = false;
        if (freezePlayerOnDialogue)
        {
            EnablePlayerController();
        }
    }

}