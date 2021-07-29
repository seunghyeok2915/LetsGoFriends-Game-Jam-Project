using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ShowRankPage : MonoBehaviour
{
      public List<PlayerRankData> playerRankDatas;
    public GameObject contentOb;
    public PlayerRankBox[] playerRankBoxes;
    public Text waitText;

    private void Start()
    {
        playerRankBoxes = contentOb.GetComponentsInChildren<PlayerRankBox>();

        foreach (var item in playerRankBoxes)
        {
            item.gameObject.SetActive(false);
        }
    }

    public void OpenPage()
    {
        waitText.gameObject.SetActive(true);
        gameObject.SetActive(true);

        foreach (var item in playerRankBoxes)
        {
            item.gameObject.SetActive(false);
        }

        RankDBManager.Instance.StartGetPlayerData();
    }

    public void MakeBox()
    {
        waitText.gameObject.SetActive(false);
        playerRankDatas = RankDBManager.Instance.playerRankDatas.ToList();

        for (int i = 0; i < playerRankDatas.Count; i++)
        {
            if (i >= 5)
                break;
            playerRankBoxes[i].SetBox(playerRankDatas[i]);
        }
    }
}
