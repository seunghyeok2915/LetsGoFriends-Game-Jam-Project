using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float PassTime => passTime;
    private float passTime;

    private Text timerText;

    private void Start()
    {
        timerText = Utils.Bind<Text>(gameObject, "");
    }

    private void Update()
    {
        passTime += Time.deltaTime;

        timerText.text = PassTime.ToString("F0");
    }
}
