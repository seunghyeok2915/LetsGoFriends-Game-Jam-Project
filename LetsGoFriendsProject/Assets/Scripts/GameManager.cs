using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Transform> spawnPointList = new List<Transform>();
    public List<string> obstacleList;

    void Start()
    {
        PoolManager.CreatePool<Obstacle>("DamageObstacle", gameObject, 5);
        PoolManager.CreatePool<Obstacle>("BounceObstacle", gameObject, 5);
        PoolManager.CreatePool<Obstacle>("HiderObstacle", gameObject, 5);
        StartCoroutine(SpawnObstacles());
    }

    public IEnumerator SpawnObstacles()
    {
        while (true)
        {
            int randIndex = Random.Range(0, obstacleList.Count);
            int randIndex2 = Random.Range(0, 2);
            switch (randIndex2)
            {
                case 0:
                    CreateObstacle(obstacleList[randIndex], MovingType.Straight, randIndex % 4 + 1);
                    break;
                case 1:
                    CreateObstacle(obstacleList[randIndex], MovingType.Curve, randIndex % 4 + 1);
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(0.8f);
        }
    }

    public void CreateObstacle(string name, MovingType movingType, float speed)
    {
        int randIndex = Random.Range(0, spawnPointList.Count);
        PoolManager.GetItem<Obstacle>(name).SetObstacle(movingType, spawnPointList[randIndex].position, new Vector2(0, 0), speed);
    }
}
