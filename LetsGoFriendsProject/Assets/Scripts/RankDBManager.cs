using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RankDBManager : MonoBehaviour
{
    const string URL = "https://script.google.com/macros/s/AKfycbxgTYCRC2gk_SmDfgoTnGDGH6HUuCEa3LgabfoYHM2I8It6g4LrKYbCbbAazjgQLd6q/exec";

    private void Start()
    {
        AddRank("이승혁", 300);
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
    }

    IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();

            if (www.isDone) print("실행됨");
            else print("웹의 응답이 없습니다.");
        }
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
