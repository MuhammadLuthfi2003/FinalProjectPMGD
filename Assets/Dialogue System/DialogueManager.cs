using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
// using static System.Net.Mime.MediaTypeNames;

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
    public static DialogueManager instance;

    public GameObject CanvasBox; // your fancy canvas box that holds your text objects
    public UnityEngine.UI.Text TextBox; // the text body
    public UnityEngine.UI.Text NameText; // the text body of the name you want to display
    public GameObject OptionsBox;
    public UnityEngine.UI.Text yesBtnText;
    public UnityEngine.UI.Text noBtnText;
    public AudioClip typeSFX;
    public AudioClip typeSFX2;
    public AudioClip pressSFX;
    

    public bool freezePlayerOnDialogue = true;
    public DialogueTrigger currentInteractableObject;

    public bool isOpen; // represents if the dialogue box is open or closed

    private Queue<string> inputStream = new Queue<string>(); // stores dialogue
    private AudioSource audioSource;
    public bool isQuestion = false;
    private int optionRemaining = 0;
    public bool lastOptionChosen = false;
    public bool isAnsweringQuestion = false;
    private string currentText = "";
    public int typeSFXToggle = 0;
    
    //private PlayerAnimController animController;

    private bool isTyping = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this); // only one dialogue manager allowed
        }
    }

    private void Start()
    {
        CanvasBox.SetActive(false); // close the dialogue box on play
        OptionsBox.SetActive(false);
        audioSource = GetComponent<AudioSource>();
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
        GameManager.Instance.player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
    }

    private void EnablePlayerController()
    {
        GameManager.Instance.player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void StartDialogue(Queue<string> dialogue)
    {
        if (currentInteractableObject != null)
        {
            if (currentInteractableObject.isFreezePlayer)
            {
                DisablePlayerController();
            }
        }
        typeSFXToggle = 0;
        CanvasBox.SetActive(true); // open the dialogue box

        isOpen = true;
        inputStream = dialogue; // store the dialogue from dialogue trigger
        PrintDialogue(); // Prints out the first line of dialogue
    }

    public void AdvanceDialogue() // call when a player presses a button in Dialogue Trigger
    {
        if (!isTyping)
        {
            audioSource.PlayOneShot(pressSFX);
            if (currentInteractableObject != null)
            {
                currentInteractableObject.onEventProgress.Invoke();
            }
            if (isAnsweringQuestion)
            {
                RespondAnswer();
            }
            else
            {
                PrintDialogue();
            }
        }
        else if (isTyping)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                StopAllCoroutines();
                TextBox.text = currentText;
                isTyping = false;
            }
        }
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

                if (currentInteractableObject != null)
                {
                    if (!currentInteractableObject.canInteractAgain)
                    {
                        Destroy(currentInteractableObject.gameObject);
                        EnablePlayerController();
                    }
                }

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
            else
            {
                currentText = inputStream.Dequeue();
                StartCoroutine(TypeSentence(currentText));
                //TextBox.text = inputStream.Dequeue();
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

    // need to fix asap
    public void PrintDecision(bool isYesOption)
    {
        currentInteractableObject.hasChosenOption = true;
        currentInteractableObject.chosenDecision = isYesOption;
        isQuestion = false;
        lastOptionChosen = isYesOption;
        isAnsweringQuestion = true;
        AdvanceDialogue();
    }

    private void RespondAnswer() // function to display answer
    {
        if (lastOptionChosen)
        {
            if (inputStream.Peek().Contains("{Y}"))
            {
                string text = inputStream.Peek();
                text = inputStream.Dequeue().Substring(text.IndexOf("}") + 1).ToString();
                //TextBox.text = text;
                currentText = text;
                StartCoroutine(TypeSentence(currentText));
            }
            else if (inputStream.Peek().Contains("{N}"))
            {
                inputStream.Dequeue();
                RespondAnswer();
            }
            else if (inputStream.Peek().Contains("[ENDCHOICE]"))
            {
                isAnsweringQuestion = false;
                inputStream.Dequeue();
                AdvanceDialogue();
            }
        }
        else
        {
            if (inputStream.Peek().Contains("{N}"))
            {
                string text = inputStream.Peek();
                text = inputStream.Dequeue().Substring(text.IndexOf("}") + 1).ToString();
                //TextBox.text = text;
                currentText = text;
                StartCoroutine(TypeSentence(currentText));
            }
            else if (inputStream.Peek().Contains("{Y}"))
            {
                inputStream.Dequeue();
                RespondAnswer();
            }
            else if (inputStream.Peek().Contains("[ENDCHOICE]"))
            {
                isAnsweringQuestion = false;
                inputStream.Dequeue();
                AdvanceDialogue();
            }
        }
    }

    public void EndDialogue()
    {
        TextBox.text = "";
        NameText.text = "";
        inputStream.Clear();
        CanvasBox.SetActive(false);
        isOpen = false;
        if (currentInteractableObject != null)
        {
            if (currentInteractableObject.isFreezePlayer)
            {
                EnablePlayerController();
            }
            currentInteractableObject.onExitEvent.Invoke();
        }
    }

    private IEnumerator TypeSentence(string text)
    {
        isTyping = true;
        yield return new WaitForSeconds(.5f);
        TextBox.text = "";
        foreach (char letter in text.ToCharArray())
        {
            TextBox.text += letter;
            typeSFXToggle++;

            if (typeSFXToggle / 2 == 0)
            {
                audioSource.PlayOneShot(typeSFX);
            }
            else
            {
                audioSource.PlayOneShot(typeSFX2);
            }
            yield return new WaitForSeconds(0.02f);
        }
        isTyping = false;
    }

}