using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TitlePanel : MonoBehaviour
{
    public Image panelImage;
    public Image mainImage;
    public AudioSource audioSource;


    public Ease ease;
    public int duration;



    void Start()
    {
        panelImage.rectTransform.DOAnchorPosY(15 ,duration).SetEase(ease).OnComplete(()=>FindObjectOfType<RippleEffect>().Emit(Camera.main.WorldToViewportPoint(transform.position)));
    }


    public void GameScene()
    {
        audioSource.Play();
        mainImage.rectTransform.DOAnchorPosY(-1240,1);

    }

}
