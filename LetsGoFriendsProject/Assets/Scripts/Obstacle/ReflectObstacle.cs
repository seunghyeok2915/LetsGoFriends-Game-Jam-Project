using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectObstacle : Obstacle
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Obstacle"))
        {
            other.gameObject.GetComponent<Obstacle>().ActiveFalse();
            //ActiveFalse(); //관통여부
        }
    }
}
