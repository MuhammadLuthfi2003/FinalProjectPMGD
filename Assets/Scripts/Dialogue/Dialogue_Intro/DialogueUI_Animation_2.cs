using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI_Animation_2 : MonoBehaviour {
    [Header("Dialogue Script")]
    [SerializeField] private GameObject gameObj;
    [SerializeField] private GameObject gameObj2;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject awakeDialogue;

    private TypewriterEffect typewriterEffect;

    // Start is called before the first frame update
    private void Start() {
        typewriterEffect = GetComponent<TypewriterEffect>();
        CloseDialogueBox();
        ShowDialogue(awakeDialogue);
    }

    public void ShowDialogue(DialogueObject dialogueObject) {
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject) {
        foreach (string dialogue in dialogueObject.Dialogue) {

            yield return RunTypingEffect(dialogue);

            textLabel.text = dialogue;

            yield return null;
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }

        CloseDialogueBox();
        gameObj.SetActive(false);
        gameObj2.SetActive(true);
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
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }

}
