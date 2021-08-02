using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SheetEditor : MonoBehaviour
{
    public bool isRecord = false;

    public List<float> noteList = new List<float>();

    private AudioSource music;
    private float time;

    //JSON
    const string saveFileName = "jsonUtillFile.sav";

    void Awake()
    {
        music = GetComponent<AudioSource>();
    }

    void Start()
    {
        //if (isRecord) noteList.Clear();
    }

    public void SetRecord()
    {
        string fileStr = getFilePath(saveFileName);
        if (File.Exists(fileStr)) // 현재 이 경로에 세이브 파일이 존재하냐
        {
            StreamReader sr = new StreamReader(fileStr);
            string jsonString = sr.ReadToEnd();
            sr.Close();

            JsonUtility.FromJsonOverwrite(jsonString, this); // 덮어쓰기

            print(jsonString);
        }
        else
        {
            print("no File");
        }
    }

    string getFilePath(string fileName)
    {
        return Application.streamingAssetsPath + "/" + fileName;
    }

    public void Play()
    {
        time = 0;
        music.Play();
        isRecord = true;
    }

    public void EffectStart()
    {
        isRecord = false;
    }

    void Update()
    {
        if (isRecord)
        {
            time += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.F))
            {
                noteList.Add(time);

                string jsonString = JsonUtility.ToJson(this); // jsonString에 통으로 저장됌

                StreamWriter sw = new StreamWriter(getFilePath(saveFileName));

                sw.WriteLine(jsonString);
                sw.Close();
            }
        }
        else
        {
            if (noteList.Count <= 0) return;

            if (GameManager.Instance.PassTime >= noteList[0])
            {
                noteList.RemoveAt(0);
                CameraMove();
            }

        }
    }


    void SaveObject()
    {
        noteList.Add(music.time);
    }

    public void CameraMove()
    {
        GameManager.Instance.CamZoomInOut();
    }
}
