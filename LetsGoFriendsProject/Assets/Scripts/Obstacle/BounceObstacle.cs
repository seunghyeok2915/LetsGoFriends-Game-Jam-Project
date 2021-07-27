using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceObstacle : Obstacle
{
    private PlayerMove playerMove;


    public override void OnEnterPlayer(GameObject player)
    {
        if (playerMove == null)
        {
            playerMove = player.GetComponent<PlayerMove>();
        }

        playerMove.ChangeDirection();
    }
}
