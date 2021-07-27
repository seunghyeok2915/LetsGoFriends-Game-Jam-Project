using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject settingPanel;
    private int delayTime;
    public Text countText;

    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 0;
            SoundManager.Instance.PauseBGM();
            settingPanel.SetActive(true);
        }
    }

    public void Close()
    {
        StartCoroutine( TimeScale(3));
        countText.gameObject.SetActive(true);
    }

    IEnumerator TimeScale(int time)
    {
        int count = time;
        while(count > 0)
        {
        Debug.Log(count);
        countText.text = ($"{count}초뒤에 게임이 시작됩니다");
        yield return new WaitForSecondsRealtime(1);
        count--;
        }
        SoundManager.Instance.ResumeBGM();
        Time.timeScale = 1;
        countText.gameObject.SetActive(false);

    }

    public void Exit()
    {
        Application.Quit();
    }
}
