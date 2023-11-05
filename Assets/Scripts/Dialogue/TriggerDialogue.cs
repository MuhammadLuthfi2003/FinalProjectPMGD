using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour {
    public GameObject Button;

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("Player")) {
            Button.SetActive(true);
        }
    }    

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.CompareTag("Player")) {
            //        Destroy(this.gameObject);
            //        Destroy(Button);
            Button.SetActive(false);
        }
    }
}
