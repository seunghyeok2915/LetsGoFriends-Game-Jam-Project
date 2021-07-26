using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingScript : MonoBehaviour
{
    protected Vector2 startPos;
    protected Vector2 endPos;

    protected float speed;

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetDirection(Vector2 pos)
    {
        MoveDirection md = gameObject.AddComponent<MoveDirection>();
        md.SetDir(pos, speed * 5);
        Destroy(this);
    }
}
