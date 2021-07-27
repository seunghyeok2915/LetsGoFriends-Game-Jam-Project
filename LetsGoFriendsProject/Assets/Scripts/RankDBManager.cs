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

public class RankDBManager : MonoBehaviour
{
    const string URL = "https://script.google.com/macros/s/AKfycbwqYSEjaPmn72O9TTL0bAIHEWSSc7YL8EQFCKn47tVTvp7w95duZhg_dtzCAn8VLx6w/exec";
    public List<PlayerRankData> playerRankDatas = new List<PlayerRankData>();

    private IEnumerator GetData()
    {
        playerRankDatas.Clear();
        for (int i = 0; i < 5; i++)
        {
            Debug.Log(i);
            GetPlayerData(i);
            yield return new WaitForSeconds(0.1f);
        }
    }

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

        StartCoroutine(GetData());
    }

    public void GetPlayerData(int num)
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "getRank");
        form.AddField("num", num);
        StartCoroutine(Post(form));
    }

    IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();

            if (www.isDone) Response(www.downloadHandler.text);
            else print("웹의 응답이 없습니다.");
        }
    }


    void Response(string json)
    {
        if (string.IsNullOrEmpty(json)) return;

        print(json);
        PlayerRankData playerRank = JsonUtility.FromJson<PlayerRankData>(json);
        if (playerRank.score < 10) return;
        playerRankDatas.Add(playerRank);
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
