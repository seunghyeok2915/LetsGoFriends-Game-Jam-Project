using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShowRankPage : MonoBehaviour
{
    public List<PlayerRankData> playerRankDatas;
    public GameObject contentOb;
    public PlayerRankBox[] playerRankBoxes;

    private void Start()
    {
        playerRankBoxes = contentOb.GetComponentsInChildren<PlayerRankBox>();
    }

    public void OpenPage()
    {
        playerRankDatas = RankDBManager.Instance.playerRankDatas.ToList();

        for (int i = 0; i < playerRankDatas.Count; i++)
        {
            playerRankBoxes[i].SetBox(playerRankDatas[i]);
        }
    }
}
