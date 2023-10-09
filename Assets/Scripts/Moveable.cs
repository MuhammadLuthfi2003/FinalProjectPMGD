using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Left,
    Right,
    Up,
    Down,
    LeftUp,
    LeftDown,
    RightUp,
    RightDown,
    None
}

public class Moveable : MonoBehaviour
{
    [Header("Movement Settings")]
    public Direction direction = Direction.None;
    public float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        if (direction == Direction.None)
        {
            return;
        }
        else if (direction == Direction.Left)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else if (direction == Direction.Right)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else if (direction == Direction.Up)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        else if (direction == Direction.Down)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
        else if (direction == Direction.LeftUp)
        {
            transform.Translate(new Vector2(-1, 1) * speed * Time.deltaTime);
        }
        else if (direction == Direction.LeftDown)
        {
            transform.Translate(new Vector2(-1, -1) * speed * Time.deltaTime);
        }
        else if (direction == Direction.RightUp)
        {
            transform.Translate(new Vector2(1, 1) * speed * Time.deltaTime);
        }
        else if (direction == Direction.RightDown)
        {
            transform.Translate(new Vector2(1, -1) * speed * Time.deltaTime);
        }
    }
}
