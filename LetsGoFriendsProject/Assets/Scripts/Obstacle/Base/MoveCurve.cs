using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCurve : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 endPos;

    private float speed;
    private float movingDepth = 1f;

    private Vector3 dir;

    public void SetMove(Vector2 startPos, Vector2 endPos, float speed, float movingDepth)
    {
        this.startPos = startPos;
        this.endPos = endPos;
        this.speed = speed;
        this.movingDepth = movingDepth;

        transform.position = startPos;

        dir = endPos - new Vector2(transform.position.x, transform.position.y);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = q;
    }

    void Update()
    {
        Vector3 delta = dir.normalized * speed;
        delta += transform.up * movingDepth * Mathf.Cos(Time.time);
        transform.position += delta * Time.deltaTime;
    }
}