using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDirection : MovingScript
{
    private Vector3 dir;

    public void SetDir(Vector3 dir, float speed)
    {
        this.dir = dir;
        this.speed = speed;
    }

    private void MoveDir()
    {
        transform.position += dir * speed * Time.deltaTime;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = q;
    }

    private void Update()
    {
        MoveDir();
    }
}
