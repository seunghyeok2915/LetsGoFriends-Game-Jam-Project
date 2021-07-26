using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStraight : MovingScript
{
    public void SetMove(Vector2 startPos, Vector2 endPos, float speed)
    {
        base.startPos = startPos;
        base.endPos = endPos;
        base.speed = speed;

        transform.position = startPos;
    }

    private void Update()
    {
        MoveStaright();
    }

    public void MoveStaright()
    {
        Vector2 dir = endPos - new Vector2(transform.position.x, transform.position.y);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = q;

        transform.position = Vector2.MoveTowards(transform.position, endPos, Time.deltaTime * speed);
    }
}
