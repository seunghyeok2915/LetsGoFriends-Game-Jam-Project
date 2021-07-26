using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public float PassTime => passTime;
    private float passTime;

    public List<Transform> spawnPointList = new List<Transform>();
    public List<string> obstacleList;

    public PlayerMove playerMove;

    private float radius;

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null) // instance 가 비어있다면
            {
                instance = FindObjectOfType<GameManager>(); // 찾아준다
                if (instance == null) // 그래도 없다면 
                {
                    instance = new GameObject(typeof(GameManager).ToString()).AddComponent<GameManager>(); // 만든다
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this as GameManager;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this);
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = default;
        }
    }

    void Start()
    {

        PoolManager.CreatePool<Obstacle>("DamageObstacle", gameObject, 5);
        PoolManager.CreatePool<Obstacle>("BounceObstacle", gameObject, 5);
        PoolManager.CreatePool<Obstacle>("HiderObstacle", gameObject, 5);

        StartCoroutine(SpawnObstacles());
        passTime = 0f;
        radius = playerMove.radius;
    }

    private void Update()
    {
        passTime += Time.deltaTime;
        if (PassTime > 10)
            radius = 3f;
        DOTween.To(() => playerMove.radius, x => playerMove.radius = x, radius, 3f);
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
            yield return new WaitForSeconds(2f);
        }
    }

    public void CreateObstacle(string name, MovingType movingType, float speed)
    {
        int randIndex = Random.Range(0, spawnPointList.Count);
        PoolManager.GetItem<Obstacle>(name).SetObstacle(movingType, spawnPointList[randIndex].position, new Vector2(0, 0), speed);
    }
}
