using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLifetime : MonoBehaviour
{

    [Header("Lifetime Settings")]
    [SerializeField] float lifetime = 5f;

    private float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}
