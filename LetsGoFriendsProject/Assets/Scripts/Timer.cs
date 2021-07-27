using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Text timerText;

    private void Start()
    {
        timerText = Utils.Bind<Text>(gameObject, "");
    }

    private void Update()
    {
        timerText.text = ($"{GameManager.Instance.PassTime.ToString("F1")}s");
        if (GameManager.Instance.PassTime < 1)
            timerText.text = "";
    }
}
