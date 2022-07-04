using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class PlayerRankData
{
    public int rank;
    public string name;
    public int score;
}

[System.Serializable]
public class Ranklist
{
    public List<PlayerRankData> list;
}

public class RankDBManager : MonoBehaviour
{
    const string URL = "https://script.google.com/macros/s/AKfycbwYx9NbFbORwq7Du991IsdSe-uPAf_7iHX2RRNYaX6z9w5uFqZxbZbIklOIFH42SBy9/exec";
    public List<PlayerRankData> playerRankDatas;

   
    public void AddRank(string name, int score)
    {
        if (name == "")
        {
            print("이름이 비어있다.");
            return;
        }

        WWWForm form = new WWWForm();
        form.AddField("order", "addRank");
        form.AddField("name", name);
        form.AddField("score", score);

        StartCoroutine(Post(form));
    }

    public void StartGetPlayerData()
    {
        GetPlayerData();
    }

    public void GetPlayerData()
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "getRank");

        Debug.Log("부름");

        StartCoroutine(Get());
    }

    IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();

            if (www.isDone) { StartGetPlayerData(); }
            else print("웹의 응답이 없습니다.");
        }
    }

    IEnumerator Get()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(URL)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();

            if (www.isDone) { Response(www.downloadHandler.text); }
            else print("웹의 응답이 없습니다.");
        }
    }


    void Response(string json)
    {
        if (string.IsNullOrEmpty(json)) return;

        print(json);

        Ranklist playerRank = JsonUtility.FromJson<Ranklist>(json);
        playerRankDatas = playerRank.list;

        GameManager.Instance.showRankPage.MakeBox();
    }

    private static RankDBManager instance;
    public static RankDBManager Instance
    {
        get
        {
            if (instance == null) // instance 가 비어있다면
            {
                instance = FindObjectOfType<RankDBManager>(); // 찾아준다
                if (instance == null) // 그래도 없다면
                {
                    instance = new GameObject(typeof(RankDBManager).ToString()).AddComponent<RankDBManager>(); // 만든다
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this as RankDBManager;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this);
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = default;
        }
    }

}
