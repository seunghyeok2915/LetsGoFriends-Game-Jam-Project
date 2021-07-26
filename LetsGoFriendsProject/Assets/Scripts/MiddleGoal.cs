using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MiddleGoal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            other.gameObject.GetComponent<Obstacle>().ActiveFalse();
        }

    }
}
