using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectObstacle : Obstacle
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            other.gameObject.GetComponent<Obstacle>().ActiveFalse();
            //ActiveFalse(); //관통여부
        }
    }
}
