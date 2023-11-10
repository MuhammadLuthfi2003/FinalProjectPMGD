using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHide : MonoBehaviour
{
    [SerializeField] Color32 hideColor;
    [SerializeField] KeyCode hideButton;
    [Tooltip("Order in Layer of SpriteRenderer to hide in")]
    [SerializeField] int hideLayer = 0; 

    bool canHide = false;
    public bool isHiding = false;
    public bool isInvincible = false;

    private SpriteRenderer sprite;
    private Color32 originalColor;
    private int originalLayer;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color;
        originalLayer = sprite.sortingOrder;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(hideButton) && canHide)
        {
            if (!isHiding)
            {
                isHiding = true;
                sprite.color = hideColor;
                sprite.sortingOrder = hideLayer;
            }
            else
            {
                isHiding = false;
                sprite.color = originalColor;
                sprite.sortingOrder = originalLayer;
            }
        }

        if (!canHide)
        {
            isHiding = false;
            sprite.color = originalColor;
            sprite.sortingOrder = originalLayer;
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HideArea")
        {
            canHide = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HideArea")
        {
            canHide = false;
        }
    }
}


