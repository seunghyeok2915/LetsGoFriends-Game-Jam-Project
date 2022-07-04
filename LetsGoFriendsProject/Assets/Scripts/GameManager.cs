using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

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
    private float oriRadius;
    public bool isStart;


    public EffectCamera camEffect;
    public CinemachineVirtualCamera virtualCamera;
    public RippleEffect rippleEffect;
    public GameOverPage gameOverPage;
    public SheetEditor sheetEditor;
    public GameObject rainEffect;
    public GameObject player;





    private int hiderStack = 0;
    private int frozenStack = 0;

    public List<float> phaseTime;
    private List<float> phaseTimeCache;
    public List<float> highlightTime;
    private List<float> highlightTimeCache;

    private Vignette vg;

    public int Score;
    public Text scoreText;
    public Text timeText;
    public Text scoreName;
    public Text timeName;
    public int sumScore;

  

    public AddRankPage addRankPage;
    public ShowRankPage showRankPage;

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
        PostProcessVolume pp = FindObjectOfType<PostProcessVolume>();
        pp.profile.TryGetSettings<Vignette>(out vg);
        phaseTimeCache = phaseTime.ToList();
        highlightTimeCache = highlightTime.ToList();
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
     
        player.GetComponent<LineRenderer>().enabled = true;
        sheetEditor.enabled = true;
        virtualCamera.transform.position = new Vector3(0, 0, -10);
        virtualCamera.m_Lens.OrthographicSize = 8.1f;
        virtualCamera.Follow = null;
        player.transform.GetComponent<PlayerInput>().enabled = true;
        player.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
        timeText.gameObject.SetActive(true);
        scoreName.gameObject.SetActive(true);
        timeName.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        playerMove.GetComponent<CircleCollider2D>().enabled = true;
        phaseTime = phaseTimeCache.ToList();
        highlightTime = highlightTimeCache.ToList();
        sheetEditor.SetRecord();
        SoundManager.Instance.PlayBGMSound("Title");
        passTime = 0f;
        phase = 1;
        playerMove.radius = oriRadius;
        radius = playerMove.radius;
        StartCoroutine(SpawnObstacles());
        isStart = true;
        FindObjectOfType<SheetEditor>().EffectStart();
        Score = 0;
        playerMove.speed = 1.5f;

        vg.enabled.value = true;
    }
    public Canvas canvas;
    void Start()
    {
        PoolManager.CreatePool<DrawEffectCircle>("EffectCircle", gameObject, 10);
        PoolManager.CreatePool<Obstacle>("DamageObstacle", gameObject, 5);
        PoolManager.CreatePool<Obstacle>("BounceObstacle", gameObject, 5);
        PoolManager.CreatePool<Obstacle>("HiderObstacle", gameObject, 5); //FrozenObstacle
        PoolManager.CreatePool<Obstacle>("FrozenObstacle", gameObject, 5);
        PoolManager.CreatePool<PointText>("PlusPointText", canvas.gameObject, 5);

        SoundManager.Instance.PlayBGMSound("Lobby");
        oriRadius = playerMove.radius;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            StartGame();

        }

        if (!isStart)
        {
            return;
        }

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
        sumScore = Score + (int)PassTime;
        scoreText.text = ($"{(Score + PassTime).ToString("F0")}");




    }

    public void Highlight()
    {
        rainEffect.SetActive(true);
        CamShake(10, 2f);
    }



    public void PhaseUp()
    {
        phase++;
        if (phase == 3)
        {
            return;
        }

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
        Debug.Log("페이즈 1");
        CreateObstacle("DamageObstacle", MovingType.Straight, 3f);
    }

    public void SpawnMobPhase2()
    {
        Debug.Log("페이즈 2");
        CreateObstacle("DamageObstacle", MovingType.Straight, 3f);
        CreateObstacle("DamageObstacle", MovingType.Curve, 3f, 1f);
    }

    public void SpawnMobPhase3()
    {
        Debug.Log("페이즈 3");
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
        Debug.Log("페이즈 4");
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
        Debug.Log("페이즈 5");
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

    public void GameOver()
    {
        sheetEditor.enabled = false;
        player.GetComponent<LineRenderer>().enabled = false;
        timeText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        scoreName.gameObject.SetActive(false);
        timeName.gameObject.SetActive(false);
        player.transform.GetChild(0).GetComponent<SpriteRenderer>().DOFade(0, 2);
        player.transform.GetComponent<LineRenderer>().material.DOFade(0, 2);
        player.transform.GetComponent<PlayerInput>().enabled = false;

        virtualCamera.Follow = player.transform;
        playerMove.GetComponent<CircleCollider2D>().enabled = false;
        SoundManager.Instance.PauseBGM();
        SoundManager.Instance.PlayFXSound("Window");
        DOTween.To(() => virtualCamera.m_Lens.OrthographicSize, value => virtualCamera.m_Lens.OrthographicSize = value, 4f, 1).OnComplete(() =>
        {
            DOTween.To(() => playerMove.speed, value => playerMove.speed = value, 0f, 0.7f).OnComplete(() =>
            {
                DOTween.To(() => Time.timeScale, value => Time.timeScale = value, 1, 0.7f).OnComplete(() =>
                {

                    SoundManager.Instance.PlayBGMSound("Lobby");
                    isStart = false;
                    vg.enabled.value = false;
                    StopAllCoroutines();
                    gameOverPage.gameObject.SetActive(true);

                });
            });

        });






    }

    public void RestartGame()
    {
        gameOverPage.gameObject.SetActive(false);
        StartGame();
    }

}
