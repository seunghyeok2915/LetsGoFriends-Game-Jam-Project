using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UnityEngine.Rendering.PostProcessing;

public class GameManager : MonoBehaviour
{
    public float PassTime => passTime;
    private float passTime;

    public float spawnDelay;
    public int phase = 1;

    public List<Transform> spawnPointList = new List<Transform>();
    public List<string> obstacleList;

    public PlayerMove playerMove;

    private float radius;
    private bool isStart;


    public EffectCamera camEffect;
    public CinemachineVirtualCamera virtualCamera;
    public RippleEffect rippleEffect;


    private int hiderStack = 0;
    private int frozenStack = 0;

    public List<float> phaseTime;
    public List<float> highlightTime;

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

    public void StartGame()
    {
        SoundManager.Instance.PlayBGMSound("TitleBackGround");
        passTime = 0f;
        radius = playerMove.radius;
        StartCoroutine(SpawnObstacles());
        isStart = true;

        FindObjectOfType<SheetEditor>().EffectStart();
        Vignette vg;
        var pp = FindObjectOfType<PostProcessVolume>();
        pp.profile.TryGetSettings<Vignette>(out vg);
        vg.enabled.value = true;
    }

    void Start()
    {
        PoolManager.CreatePool<DrawEffectCircle>("EffectCircle", gameObject, 10);
        PoolManager.CreatePool<Obstacle>("DamageObstacle", gameObject, 5);
        PoolManager.CreatePool<Obstacle>("BounceObstacle", gameObject, 5);
        PoolManager.CreatePool<Obstacle>("HiderObstacle", gameObject, 5); //FrozenObstacle
        PoolManager.CreatePool<Obstacle>("FrozenObstacle", gameObject, 5);
    }

    private void Update()
    {
        if (!isStart) return;

        passTime += Time.deltaTime;

        if (phaseTime.Count > 0)
        {
            if (PassTime >= phaseTime[0])
            {
                phaseTime.RemoveAt(0);
                PhaseUp();
            }
        }

        if (highlightTime.Count > 0)
        {
            if (PassTime >= highlightTime[0])
            {
                highlightTime.RemoveAt(0);
                Highlight();
            }
        }

    }

    public void Highlight()
    {
        CamShake(10, 2f);
    }

    public void PhaseUp()
    {
        phase++;
        if (phase == 3) return;
        radius = playerMove.radius - 0.5f;
        DOTween.To(() => playerMove.radius, x => playerMove.radius = x, radius, 3f);
    }

    public IEnumerator SpawnObstacles()
    {
        while (true)
        {
            SpawnObstcle();
            yield return new WaitForSeconds(1f);
        }
    }

    public IEnumerator SpawnRandomObstacles()
    {
        while (true)
        {
            int randIndex = Random.Range(0, obstacleList.Count);
            int randIndex2 = Random.Range(0, 2);
            int randIndex3 = Random.Range(2, 4);
            switch (randIndex2)
            {
                case 0:
                    CreateObstacle(obstacleList[randIndex], MovingType.Straight, randIndex3);
                    break;
                case 1:
                    CreateObstacle(obstacleList[randIndex], MovingType.Curve, randIndex3);
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public void CreateObstacle(string name, MovingType movingType, float speed, float movingDepth = 1f)
    {
        int randIndex = Random.Range(0, spawnPointList.Count);
        PoolManager.GetItem<Obstacle>(name).SetObstacle(movingType, spawnPointList[randIndex].position, new Vector2(0, 0), speed, movingDepth);
    }



    public void SpawnMobPhase1()
    {
        CreateObstacle("DamageObstacle", MovingType.Straight, 3f);
    }

    public void SpawnMobPhase2()
    {
        CreateObstacle("DamageObstacle", MovingType.Straight, 3f);
        CreateObstacle("DamageObstacle", MovingType.Curve, 3f, 1f);
    }

    public void SpawnMobPhase3()
    {
        CreateObstacle("DamageObstacle", MovingType.Straight, 3f);
        CreateObstacle("DamageObstacle", MovingType.Curve, 3f, 1f);
        hiderStack++;
        if (hiderStack >= 2)
        {
            CreateObstacle("HiderObstacle", MovingType.Straight, 3f);
            hiderStack = 0;
        }
    }

    public void SpawnMobPhase4()
    {
        CreateObstacle("DamageObstacle", MovingType.Straight, 3f);
        CreateObstacle("DamageObstacle", MovingType.Curve, 3f, 1f);
        hiderStack++;
        if (hiderStack >= 2)
        {
            CreateObstacle("HiderObstacle", MovingType.Straight, 3f);
            hiderStack = 0;
        }

        frozenStack++;
        if (frozenStack >= 3)
        {
            CreateObstacle("FrozenObstacle", MovingType.Straight, 3f);
            frozenStack = 0;
        }
    }

    public void SpawnMobPhase5()
    {
        CreateObstacle("DamageObstacle", MovingType.Straight, 3f);
        CreateObstacle("DamageObstacle", MovingType.Curve, 3f, 1f);
        hiderStack++;
        if (hiderStack >= 2)
        {
            CreateObstacle("HiderObstacle", MovingType.Straight, 3f);
            hiderStack = 0;
        }

        frozenStack++;
        if (frozenStack >= 2)
        {
            CreateObstacle("FrozenObstacle", MovingType.Straight, 3f);
            frozenStack = 0;
        }
    }


    public void SpawnObstcle()
    {
        switch (phase)
        {
            case 1:
                SpawnMobPhase1();
                break;
            case 2:
                SpawnMobPhase2();
                break;
            case 3:
                SpawnMobPhase3();
                break;
            case 4:
                SpawnMobPhase4();
                break;
            case 5:
                SpawnMobPhase5();
                break;
            default:
                break;
        }
    }


    public static void CamShake(float intense, float during)
    {
        instance.camEffect.SetShake(intense, during);
    }

    public void RippleEffects()
    {
        rippleEffect.Emit(Camera.main.WorldToViewportPoint(transform.position));
    }

    public void CamZoomInOut()
    {
        DOTween.To(() => virtualCamera.m_Lens.OrthographicSize, value => virtualCamera.m_Lens.OrthographicSize = value, 8f, 0).OnComplete(() =>
            {
                DOTween.To(() => virtualCamera.m_Lens.OrthographicSize, value => virtualCamera.m_Lens.OrthographicSize = value, 8.1f, 0.2f);
            });

        PoolManager.GetItem<DrawEffectCircle>("EffectCircle").DrawCircle();
    }

}
