using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBG : MonoBehaviour
{

    public float scrollSpeed; // kecepatan scroll
    public float tileSizeX = 10f; // ukuran tile background
    public Transform startPosition;

    private Vector2 startPositionVector;

    // Start is called before the first frame update
    void Start()
    {

        startPositionVector = startPosition.position;
    }

    // Update is called once per frame
    void Update()
    {

        // menghitung offset untuk background scroll
        float scrollOffset = Time.time * scrollSpeed;
        float newPosition = Mathf.Repeat(scrollOffset, tileSizeX);

        // mengatur posisi background baru
        transform.position = startPositionVector + Vector2.left * newPosition;
    }
}
