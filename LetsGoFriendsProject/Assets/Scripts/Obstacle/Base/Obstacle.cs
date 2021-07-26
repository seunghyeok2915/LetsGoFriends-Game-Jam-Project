using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovingType
{
    Straight,
    Curve
}

public class Obstacle : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 endPos;

    private float speed;

    private MovingType movingType;

    public void SetMovingEntity(Vector2 startPos, Vector2 endPos, float speed)
    {
        this.startPos = startPos;
        this.endPos = endPos;
        this.speed = speed;

        transform.position = startPos;
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        switch (movingType)
        {
            case MovingType.Straight:
                MoveStaright();
                break;
            case MovingType.Curve:
                break;
            default:
                break;
        }
    }

    public void MoveStaright()
    {
        transform.position = Vector2.MoveTowards(transform.position, endPos, Time.deltaTime * speed);
    }

    public virtual void OnEnterPlayer(GameObject player) { }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnEnterPlayer(other.gameObject);
        }
    }
}
