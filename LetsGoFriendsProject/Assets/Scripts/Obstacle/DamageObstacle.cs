using UnityEngine;

public class DamageObstacle : Obstacle
{
    public override void OnEnterPlayer(GameObject player)
    {
        GameManager.Instance.GameOver();
    }
}
