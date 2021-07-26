using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitlePanel : MonoBehaviour
{
    public Image panelImage;
    public Ease ease;
    public int duration;

    void Start()
    {
        panelImage.rectTransform.DOAnchorPosY(61 ,duration).SetEase(ease).OnComplete(()=>FindObjectOfType<RippleEffect>().Emit(Camera.main.WorldToViewportPoint(transform.position)));
    }
}
