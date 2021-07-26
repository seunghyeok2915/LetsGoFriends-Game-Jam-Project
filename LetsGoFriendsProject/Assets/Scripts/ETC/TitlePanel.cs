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

    public Button panelBtn;

    void Start()
    {
        SoundManager.Instance.AdjustMasterVolume(1);
        SoundManager.Instance.AdjustFxVoulme(1);
        SoundManager.Instance.AdjustBGMVolume(1);

        SoundManager.Instance.PlayBGMSound("TitleBackGround");

        panelImage.rectTransform.DOAnchorPosY(15 ,duration).SetEase(ease).OnComplete(()=>
        {
            FindObjectOfType<RippleEffect>().Emit(Camera.main.WorldToViewportPoint(transform.position));
            panelBtn.interactable = true;
             SoundManager.Instance.PlayFXSound("TitleClick");
        });

    }

    void OnEnable()
    {
    }


    public void GameScene()
    {


       // SoundManager.Instance.PlayFXSound("TitleClick");

        mainImage.rectTransform.DOAnchorPosY(-1240,1);

    }

}
