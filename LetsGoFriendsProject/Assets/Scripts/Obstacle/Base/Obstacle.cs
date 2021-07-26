using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum MovingType
{
    Straight,
    Curve
}

public class Obstacle : MonoBehaviour
{
    private MovingType movingType;
    private bool onScreen;

    protected SpriteRenderer spriteRenderer;
    protected TrailRenderer trailRenderer;

    private void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = Utils.Bind<SpriteRenderer>(gameObject, "");
            if (spriteRenderer == null)
            {
                Debug.LogWarning("SpriteRenderer is null");
            }
        }

        if (trailRenderer == null)
        {
            trailRenderer = Utils.Bind<TrailRenderer>(gameObject, "");
            if (trailRenderer == null)
            {
                Debug.LogWarning("TrailRenderer is null");
            }
        }
    }

    public virtual void SetObstacle(MovingType movingType, Vector2 startPos, Vector2 endPos, float speed, float movingDepth = 1f)
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = Utils.Bind<SpriteRenderer>(gameObject, "");
            if (spriteRenderer == null)
            {
                Debug.LogWarning("SpriteRenderer is null");
            }
        }

        if (trailRenderer == null)
        {
            trailRenderer = Utils.Bind<TrailRenderer>(gameObject, "");
            if (trailRenderer == null)
            {
                Debug.LogWarning("TrailRenderer is null");
            }
        }

        this.movingType = movingType;
        switch (movingType)
        {
            case MovingType.Straight:
                MoveStraight ms = gameObject.AddComponent<MoveStraight>();
                ms.SetMove(startPos, endPos, speed);
                break;
            case MovingType.Curve:
                MoveCurve mc = gameObject.AddComponent<MoveCurve>();
                mc.SetMove(startPos, endPos, speed, movingDepth);
                break;
            default:
                break;
        }

        onScreen = false;

        spriteRenderer.color = new Color(1, 1, 1, 1);
        trailRenderer.material.color = new Color(1, 1, 1, 1);

        Invoke("ActiveFalse", 10f);
    }

    public virtual void ActiveFalse()
    {
        var ms = GetComponents<MovingScript>();
        foreach (var item in ms)
        {
            Destroy(item);
        }

        trailRenderer.material.DOColor(new Color(0, 0, 0, 0), 0.3f);
        spriteRenderer.DOColor(new Color(0, 0, 0, 0), 0.3f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

    private void Update()
    {
        if ((transform.position.x > -9f && transform.position.x < 9f) && (transform.position.y > -5f && transform.position.y < 5f))
        {
            onScreen = true;
        }
        if (onScreen)
        {
            if ((transform.position.x < -9f && transform.position.x > 9f) && (transform.position.y < -5f && transform.position.y > 5f))
            {
                ActiveFalse();
            }
        }
    }


    public virtual void OnEnterPlayer(GameObject player) { }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnEnterPlayer(other.gameObject);
        }
    }
}
