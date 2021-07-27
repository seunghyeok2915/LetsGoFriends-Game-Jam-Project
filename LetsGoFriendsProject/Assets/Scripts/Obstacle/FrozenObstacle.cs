using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenObstacle : Obstacle
{
    private PlayerMove playerMove;


    public override void OnEnterPlayer(GameObject player)
    {
        if (playerMove == null)
        {
            playerMove = player.GetComponent<PlayerMove>();
        }

        playerMove.FrozenPlayer(1f);
    }
}
