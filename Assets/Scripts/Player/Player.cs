using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    [Header("Dialogue")]
    [SerializeField] private DialogueUI dialogueUI;

    public DialogueUI DialogueUI => dialogueUI;
    public IInteractable Interactable { get; set; }

    //movement
    public float movementSpeed;
    public bool isFacingRight;
    Rigidbody2D rb;       

    //animation
    Animator anim;
    string walk_parameter = "walk";
    string idle_parameter = "idle";              

    void Awake() {
        Time.timeScale = 1;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
       

    void FixedUpdate() {
        Movement();
    }

    private void Update() {
        if (dialogueUI.IsOpen) return;
        
        if (Input.GetKeyDown(KeyCode.E)) {
            Interactable?.Interact(this);
        }
    }

    public void Movement() {
        float move = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(move * movementSpeed, rb.velocity.y);

        if (move != 0)
        {
            anim.SetTrigger(walk_parameter);
        }
        else
        {
            anim.SetTrigger(idle_parameter);
        }


        if (move > 0 && !isFacingRight)
        {
            transform.eulerAngles = Vector2.zero;
            isFacingRight = true;
        }
        else if (move < 0 && isFacingRight)
        {
            transform.eulerAngles = Vector2.up * 180;
            isFacingRight = false;
        }        

    }       
    

    void ShowDialog() {
        Interactable?.Interact(this);
    }

} // Class


































