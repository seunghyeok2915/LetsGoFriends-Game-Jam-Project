using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float PassTime => passTime;
    private float passTime;

    public Text timerText;

    private void Update()
    {
        passTime += Time.deltaTime;

        timerText.text = PassTime.ToString("F0");
    }
}
