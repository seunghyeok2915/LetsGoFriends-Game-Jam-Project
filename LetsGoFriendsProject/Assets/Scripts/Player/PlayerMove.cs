using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("속도, 반지름")]

    [SerializeField] [Range(0f, 10f)] public float speed = 1;
    [SerializeField] [Range(0f, 10f)] public float radius = 1;

    private float runningTime = 0;
    private Vector2 newPos = new Vector2();

    private void Update()
    {
        runningTime += Time.deltaTime * speed;
        float x = radius * Mathf.Cos(runningTime);
        float y = radius * Mathf.Sin(runningTime);
        newPos = new Vector2(x, y);
        this.transform.position = newPos;
    }

    public void ChangeDirection()
    {
        speed = speed * -1f;
    }
}