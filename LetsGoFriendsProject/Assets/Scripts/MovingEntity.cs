using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEntity : MonoBehaviour
{
    public Vector2 startPos;
    public Vector2 endPos;

    public float speed;

    public void SetMovingEntity(Vector2 startPos, Vector2 endPos, float speed)
    {
        this.startPos = startPos;
        this.endPos = endPos;
        this.speed = speed;

        transform.position = startPos;

        Quaternion.LookRotation(endPos);
    }

    private void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * speed);
    }
}
