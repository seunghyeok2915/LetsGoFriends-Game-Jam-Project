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
    private MovingType movingType;

    public void SetObstacle(MovingType movingType, Vector2 startPos, Vector2 endPos, float speed, float movingDepth = 2f)
    {
        this.movingType = movingType;
        switch (movingType)
        {
            case MovingType.Straight:
                MoveStraight ms = gameObject.AddComponent<MoveStraight>();
                ms.SetMove(startPos, endPos, speed);
                break;
            case MovingType.Curve:
                MoveCurve mc = gameObject.AddComponent<MoveCurve>();
                mc.SetMove(startPos, endPos, speed, movingDepth);
                break;
            default:
                break;
        }
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
