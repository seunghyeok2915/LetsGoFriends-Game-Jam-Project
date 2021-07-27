using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SheetEditor : MonoBehaviour
{
    public List<float> noteList = new List<float>();

    private AudioSource music;
    private float time;
    private bool isPlay = false;

    //JSON
    const string saveFileName = "jsonUtillFile.sav";

    void Awake()
    {
        music = GetComponent<AudioSource>();
    }

    void Start()
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
        return Application.persistentDataPath + "/" + fileName;
    }

    public void Play()
    {
        isPlay = true;
        time = 0;
        music.Play();
    }

    void Update()
    {
        if (isPlay)
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


        if(noteList.Count <= 0 ) return;

        if(time >= noteList[0])
        {
            Debug.Log(time);
            noteList.RemoveAt(0);
        }
    }



    void SaveObject()
    {
        noteList.Add(music.time);
    }
}
