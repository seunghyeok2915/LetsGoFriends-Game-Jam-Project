using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HiderObstacle : Obstacle
{
    public override void SetObstacle(MovingType movingType, Vector2 startPos, Vector2 endPos, float speed, float movingDepth = 2f)
    {
        base.SetObstacle(movingType, startPos, endPos, speed);
        StartCoroutine(Hider());

    }

    public IEnumerator Hider()
    {
        Color oriColor = spriteRenderer.color;
        while (true)
        {
            spriteRenderer.DOColor(new Color(0, 0, 0, 0), 0.05f);
            trailRenderer.material.DOColor(new Color(0, 0, 0, 0), 0.05f);
            yield return new WaitForSeconds(0.05f);
            trailRenderer.material.DOColor(oriColor, 0.05f);
            spriteRenderer.DOColor(oriColor, 0.05f);
            yield return new WaitForSeconds(0.05f);
            spriteRenderer.DOColor(new Color(0, 0, 0, 0), 0.05f);
            trailRenderer.material.DOColor(new Color(0, 0, 0, 0), 0.05f);
            yield return new WaitForSeconds(0.3f);
            trailRenderer.material.DOColor(oriColor, 0.4f);
            spriteRenderer.DOColor(oriColor, 0.4f);
            yield return new WaitForSeconds(1f);
        }
    }

    public override void ActiveFalse()
    {
        base.ActiveFalse();
        StopCoroutine(Hider());
    }

    public override void OnEnterPlayer(GameObject player)
    {
        GameManager.Instance.GameOver();
    }
}
