using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParry : MonoBehaviour
{
    public float maxRadius;
    public float radius;

    public float radiusSpeed;
    public LayerMask obstacleLayer;

    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = Utils.Bind<LineRenderer>(gameObject, "");

        lineRenderer.positionCount = (300);
        lineRenderer.useWorldSpace = false;
        lineRenderer.startWidth = 0.1f;

    }

    public void OnSpaceBtn()
    {
        radius += Time.deltaTime * radiusSpeed;
        radius = Mathf.Clamp(radius, 0, maxRadius);
        CreatePoints();
    }

    public void OnSpaceBtnUp()
    {
        RaycastHit2D[] colls = Physics2D.CircleCastAll(transform.position, radius, Vector2.zero, obstacleLayer);

        foreach (var item in colls)
        {

            if (item.collider.CompareTag("Obstacle"))
            {
                Debug.Log(item.collider.gameObject.name);
                MovingScript ms = item.collider.gameObject.GetComponent<MovingScript>();

                if (ms != null)
                {
                    Vector2 inNormal = Vector3.Normalize(transform.position - item.collider.transform.position);
                    Vector2 bounceVector = Vector3.Reflect(item.normal, inNormal);
                    bounceVector = bounceVector.normalized;

                    ms.SetDirection(bounceVector);
                    ms.gameObject.AddComponent<ReflectObstacle>();
                }

            }
        }

        radius = 0;
        CreatePoints();
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
