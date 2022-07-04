using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TitlePanel : MonoBehaviour
{
    public Image panelImage;
    public Image mainImage;
    public AudioSource audioSource;
    public Button button;



    public Ease ease;
    public int duration;

    public Button panelBtn;

    public Text readyTxt;

    private bool isReady = false;

    public static bool isStart = false;

    public CanvasGroup group;

    void Start()
    {
        isStart = false;
        SoundManager.Instance.AdjustMasterVolume(1);
        SoundManager.Instance.AdjustFxVoulme(1);
        SoundManager.Instance.AdjustBGMVolume(1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isStart)
            {
                if (!isReady)
                {
                    titleLogo();
                    isReady = true;
                }
                else
                {
                    GameScene();
                    isStart = true;
                }

            }
        }
    }



    public void titleLogo()
    {
        SoundManager.Instance.PlayFXSound("InfoClick");
        panelImage.rectTransform.DOAnchorPosY(13, duration).SetEase(ease).OnComplete(() =>
        {
            FindObjectOfType<RippleEffect>().Emit(Camera.main.WorldToViewportPoint(transform.position));
            panelBtn.interactable = true;
            SoundManager.Instance.PlayFXSound("TitleClick");
            readyTxt.gameObject.SetActive(true);
        });
    }


    public void GameScene()
    {
        FindObjectOfType<DrawCircle>().ChangeColor();
        button.gameObject.SetActive(false);
        readyTxt.gameObject.SetActive(false);

        group.DOFade(0, 1);
        //mainImage.rectTransform.DOAnchorPosY(-1240, 1);
        GameManager.Instance.StartGame();
    }

}
