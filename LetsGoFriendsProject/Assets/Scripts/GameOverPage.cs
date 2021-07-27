using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameOverPage : MonoBehaviour
{
    public Button restartBtn;

    public Button registerScoreBtn;

    public Text ScoreText;
    public Text TimeText;

    public RectTransform FxHolder;
    public Image CircleImg;

    [SerializeField] [Range(0,1)] float progress = 0f;


    public Button addRankBtn;

    private void Start()
    {

        restartBtn.onClick.AddListener(() => GameManager.Instance.RestartGame());
    }

    void OnEnable()
    {
        ScoreText.DOText(GameManager.Instance.sumScore.ToString(), 1, true, ScrambleMode.All).OnComplete(() =>
          {
              TimeText.DOText($"{GameManager.Instance.PassTime.ToString("F1")} ({(287 % GameManager.Instance.PassTime).ToString("F2")})%", 1, true, ScrambleMode.All)
            .OnComplete(() =>
           {
               restartBtn.interactable = true;
               registerScoreBtn.interactable = true;
           });
          });


    }

    void OnDisable()
    {
        restartBtn.interactable = false;
        registerScoreBtn.interactable = false;
        ScoreText.text = null;
        TimeText.text = null;
    }


    void Update()
    {
      //  CircleImg.fillAmount = progress ;
        FxHolder.rotation = Quaternion.Euler(new Vector3(0f,0f,-progress * 360));
    }

}
