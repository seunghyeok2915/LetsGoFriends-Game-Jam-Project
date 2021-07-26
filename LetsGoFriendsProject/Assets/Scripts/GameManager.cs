using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Transform> spawnPointList = new List<Transform>();

    void Start()
    {
        PoolManager.CreatePool<Obstacle>("CircleObstacle", gameObject, 5);
        CreateObstacle();
    }

    public void CreateObstacle()
    {
        int randIndex = Random.Range(0, spawnPointList.Count);
        PoolManager.GetItem<Obstacle>("CircleObstacle").SetMovingEntity(spawnPointList[randIndex].position, new Vector2(0, 0), 3f);
    }
}
