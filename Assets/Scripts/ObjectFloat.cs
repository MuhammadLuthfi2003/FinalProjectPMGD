using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFloat : MonoBehaviour
{
    [SerializeField] float hoverSpeed = 1f;
    [SerializeField] float amplitude = 0.5f;
    [SerializeField] bool isVertical = true;

    private Vector2 tmpPos;

    // Start is called before the first frame update
    void Start()
    {
        tmpPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        FloatKey(); 
    }

    void FloatKey()
    {
        if (isVertical)
        {
            tmpPos.y += Mathf.Sin(Time.time * hoverSpeed) * amplitude * Time.deltaTime;   
        }
        else
        {
            tmpPos.x += Mathf.Sin(Time.time * hoverSpeed) * amplitude * Time.deltaTime;
        }
        transform.position = tmpPos;
    }
}
