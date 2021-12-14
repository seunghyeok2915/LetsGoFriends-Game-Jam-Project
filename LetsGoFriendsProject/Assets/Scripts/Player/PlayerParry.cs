using UnityEngine;

public class PlayerParry : MonoBehaviour
{
    public float maxRadius;
    public float radius;

    public float radiusSpeed;
    public LayerMask obstacleLayer;
    public GameObject rainObject;
    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = Utils.Bind<LineRenderer>(gameObject, "");

        lineRenderer.positionCount = (300);
        lineRenderer.useWorldSpace = false;
        lineRenderer.startWidth = 0.1f;

    }

    private void Update()
    {
        radius += Time.deltaTime * radiusSpeed;
        radius = Mathf.Clamp(radius, 0, maxRadius);
        CreatePoints();
    }

    public void OnSpaceBtn()
    {
        int countMs = 0;
        RaycastHit2D[] colls = Physics2D.CircleCastAll(transform.position, radius, Vector2.zero, obstacleLayer);
        foreach (RaycastHit2D item in colls)
        {
            if (item.collider.CompareTag("Obstacle"))
            {
                Debug.Log(item.collider.gameObject.name);
                MovingScript ms = item.collider.gameObject.GetComponent<MovingScript>();

                if (ms != null)
                {
                    countMs++;

                    Vector2 inNormal = Vector3.Normalize(transform.position - item.collider.transform.position);
                    Vector2 bounceVector = Vector3.Reflect(item.normal, inNormal);
                    bounceVector = bounceVector.normalized;

                    ms.SetDirection(bounceVector);
                    ms.gameObject.AddComponent<ReflectObstacle>();
                    ms.gameObject.GetComponent<Obstacle>().ChangeColorRandom();


                    GameManager.Instance.RippleEffects();
                    SoundManager.Instance.PlayFXSound("TitleClick");
                }
            }
        }
        if (countMs >= 2)
        {
            rainObject.SetActive(true);
        }

        switch (countMs)
        {
            case 1:
                GameManager.Instance.Score += 5;
                PoolManager.GetItem<PointText>("PlusPointText").SetText(5);
                break;

            case 2:
                GameManager.Instance.Score += 25;
                PoolManager.GetItem<PointText>("PlusPointText").SetText(25);
                break;

            case 3:
                GameManager.Instance.Score += 125;
                PoolManager.GetItem<PointText>("PlusPointText").SetText(125);
                break;

            case 4:
                GameManager.Instance.Score += 625;
                PoolManager.GetItem<PointText>("PlusPointText").SetText(625);
                break;

        }

        radius = 0;
        CreatePoints();

    }

    public void OnSpaceBtnUp()
    {

    }

    void CreatePoints()
    {
        float x;
        float y;
        float z = 0f;

        float angle = 20f;

        for (int i = 0; i < 300; i++)
        {
            x = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            y = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;

            lineRenderer.SetPosition(i, new Vector3(x, y, z));
            angle += (360f / 299);
        }
    }
}
