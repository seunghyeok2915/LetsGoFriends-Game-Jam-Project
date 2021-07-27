using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObstacle : Obstacle
{
    public override void OnEnterPlayer(GameObject player)
    {
        GameManager.Instance.GameOver();
    }
}
