using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(LineRenderer))]
public class DrawEffectCircle : MonoBehaviour
{
    public int segments;

    public float radius;

    public float width;
    LineRenderer line;

    public void DrawCircle()
    {
        DOTween.To(() => radius, value => radius = value, GameManager.Instance.playerMove.radius + 1f, 0.01f).OnComplete(() =>
             {
                 DOTween.To(() => radius, value => radius = value, GameManager.Instance.playerMove.radius, 0.4f).OnComplete(() => gameObject.SetActive(false));
             });
    }

    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        line.positionCount = (segments + 1);
        line.useWorldSpace = false;
        line.startWidth = width;
    }

    private void Update()
    {
        CreatePoints();
    }

    void CreatePoints()
    {
        float x;
        float y;
        float z = 0f;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            y = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;

            line.SetPosition(i, new Vector3(x, y, z));
            angle += (360f / segments);
        }
    }
}



