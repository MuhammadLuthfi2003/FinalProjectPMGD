using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class DIalogueUI_EventChangeScene : MonoBehaviour {
    [Header("Dialogue Script")]
    [SerializeField] private int numberScene;
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
        SceneManager.LoadScene(numberScene);
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
