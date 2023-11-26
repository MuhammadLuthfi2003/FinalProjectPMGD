using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTowardPlayer : MonoBehaviour
{
    [SerializeField] bool isSpriteFacingRight = true;

    private Transform playerTransform;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameManager.Instance.player.transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float currentPlayerX = playerTransform.position.x;

        if (currentPlayerX > transform.position.x) // Player is to the right of the NPC
        {
            if (isSpriteFacingRight)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
        }
        else
        {
            if (isSpriteFacingRight)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
    }
}
