using UnityEngine;

public class DialogueActivatorStay : MonoBehaviour, IInteractable{
    [SerializeField] private DialogueObject dialogueObject;    

    public void UpdateDialogueObject(DialogueObject dialogueObject) {
        this.dialogueObject = dialogueObject;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && other.TryGetComponent(out Player player)) {
            player.Interactable = this;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player") && other.TryGetComponent(out Player player)) {
            if (player.Interactable is DialogueActivatorStay dialogueActivator && dialogueActivator == this) {
                player.Interactable = null;
            }
        }
    }


    public void Interact(Player player) {        
        player.DialogueUI.ShowDialogue(dialogueObject);
    }

}
