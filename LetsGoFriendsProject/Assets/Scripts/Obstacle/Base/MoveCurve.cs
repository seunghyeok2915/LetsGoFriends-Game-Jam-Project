using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCurve : MovingScript
{
    private float movingDepth = 1f;

    private Vector3 dir;
    private Vector3 up;

    private Vector3 lastPos;

    public void SetMove(Vector2 startPos, Vector2 endPos, float speed, float movingDepth)
    {
        base.startPos = startPos;
        base.endPos = endPos;
        base.speed = speed;
        this.movingDepth = movingDepth;

        transform.position = startPos;

        dir = endPos - new Vector2(transform.position.x, transform.position.y);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = q;
        up = transform.up;

        lastPos = transform.position;
    }

    void Update()
    {
        Vector3 delta = dir.normalized * speed;
        delta += up * movingDepth * Mathf.Cos(Time.time);
        transform.position += delta * Time.deltaTime;

        Vector3 currentPos = transform.position;

        Vector3 arrowDir = (currentPos - lastPos).normalized;
        float angle = Mathf.Atan2(arrowDir.y, arrowDir.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = q;

        //쿵짝쿵짝

        lastPos = currentPos;
    }
}