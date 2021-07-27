using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetEditor : MonoBehaviour
{
    private AudioSource music;
    float divSpeed;

    void Awake()
    {
        music = GetComponent<AudioSource>();
    }

    public List<float> noteList = new List<float>();


    void SaveObject()
    {
        noteList.Add(music.time);
    }
}
