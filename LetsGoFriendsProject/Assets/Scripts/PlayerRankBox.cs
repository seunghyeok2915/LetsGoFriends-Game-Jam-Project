using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRankBox : MonoBehaviour
{
    private PlayerRankData playerRankData;

    public Text rankText;
    public Text nameText;
    public Text scoreText;

    public void SetBox(PlayerRankData playerRank)
    {
        playerRankData = playerRank;

        rankText.text = playerRankData.rank.ToString();
        nameText.text = playerRankData.name;
        scoreText.text = playerRankData.score.ToString();

        gameObject.SetActive(true);
    }
}
