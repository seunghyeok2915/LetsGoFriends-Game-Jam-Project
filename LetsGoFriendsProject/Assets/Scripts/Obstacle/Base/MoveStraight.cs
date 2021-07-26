using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStraight : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 endPos;

    private float speed;

    public void SetMove(Vector2 startPos, Vector2 endPos, float speed)
    {
        this.startPos = startPos;
        this.endPos = endPos;
        this.speed = speed;

        transform.position = startPos;
    }

    private void Update()
    {
        MoveStaright();
    }

    public void MoveStaright()
    {
        transform.position = Vector2.MoveTowards(transform.position, endPos, Time.deltaTime * speed);
    }
}
