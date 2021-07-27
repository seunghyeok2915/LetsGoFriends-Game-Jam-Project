using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPage : MonoBehaviour
{
    public Button restartBtn;
    public Button addRankBtn;

    private void Start()
    {
        restartBtn.onClick.AddListener(() => GameManager.Instance.RestartGame());
    }
}
