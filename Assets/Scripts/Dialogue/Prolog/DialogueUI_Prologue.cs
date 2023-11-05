using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI_Prologue : MonoBehaviour {
    [Header("Dialogue Script")]
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject awakeDialogue;
       
    private TypewriterEffect typewriterEffect;

    public bool IsOpen { get; private set; }

    //[Header("Audio Text")]
    //public AudioSource sc;
    //public AudioSource ac;

    // Start is called before the first frame update
    private void Start() {
        typewriterEffect = GetComponent<TypewriterEffect>();
        CloseDialogueBox();
        ShowDialogue(awakeDialogue);        
    }

    public void ShowDialogue(DialogueObject dialogueObject) {
        IsOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
        //sc.Stop();        
        //ac.Play();
    }   

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject) {
        foreach (string dialogue in dialogueObject.Dialogue) {            

            yield return RunTypingEffect(dialogue);

            textLabel.text = dialogue;            

            yield return null;
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            //ac.Play();
        }
        
        //ac.Stop();
        CloseDialogueBox();        

    }

    private IEnumerator RunTypingEffect(string dialogue) {
        typewriterEffect.Run(dialogue, textLabel);

        while (typewriterEffect.isRunning) {
            yield return null;

            if (Input.GetKeyDown(KeyCode.Space)) {
                typewriterEffect.Stop();
            }
        }

    }

    public void CloseDialogueBox() {
        IsOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }

}






























