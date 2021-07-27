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
    public Button button;



    public Ease ease;
    public int duration;

    public Button panelBtn;



    void Start()
    {

        SoundManager.Instance.AdjustMasterVolume(1);
        SoundManager.Instance.AdjustFxVoulme(1);
        SoundManager.Instance.AdjustBGMVolume(1);


    }



    public void titleLogo()
    {
        SoundManager.Instance.PlayFXSound("InfoClick");
        panelImage.rectTransform.DOAnchorPosY(13, duration).SetEase(ease).OnComplete(() =>
        {
            FindObjectOfType<RippleEffect>().Emit(Camera.main.WorldToViewportPoint(transform.position));
            panelBtn.interactable = true;
            SoundManager.Instance.PlayFXSound("TitleClick");
        });
    }


    public void GameScene()
    {

        button.gameObject.SetActive(false);
        mainImage.rectTransform.DOAnchorPosY(-1240, 1);
        GameManager.Instance.StartGame();
    }

}
