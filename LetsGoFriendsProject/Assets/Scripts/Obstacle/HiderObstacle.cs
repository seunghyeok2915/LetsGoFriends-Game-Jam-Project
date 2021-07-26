using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HiderObstacle : Obstacle
{
    private SpriteRenderer spriteRenderer;

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
        StartCoroutine(Hider());
    }

    public IEnumerator Hider()
    {
        while (true)
        {
            Color oriColor = spriteRenderer.color;
            spriteRenderer.DOColor(new Color(0, 0, 0, 0), 0.4f);
            yield return new WaitForSeconds(1f);
            spriteRenderer.DOColor(oriColor, 0.4f);
            yield return new WaitForSeconds(1f);
        }
    }

    public override void OnEnterPlayer(GameObject player)
    {
        //TODO 데미지 or 끝내
    }
}
