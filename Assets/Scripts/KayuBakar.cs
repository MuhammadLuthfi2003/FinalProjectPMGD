using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class KayuBakar : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameManager.Instance.kayubakar.value--;
            GameManager.Instance.score.value += 10;
            SFXPlayer.Instance.PlayItemPickupSFX();
            Destroy(gameObject);
        }
    }
}
