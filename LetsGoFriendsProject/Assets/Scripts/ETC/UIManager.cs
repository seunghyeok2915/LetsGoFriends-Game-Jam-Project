using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject settingPanel;
    private int delayTime;
    public Text countText;
    public GameObject InfoView;


    public void Close()
    {
        if (GameManager.Instance.isStart)
        {
            StartCoroutine(TimeScale(3));
        }
        else
        {
            Time.timeScale = 1;
            SoundManager.Instance.ResumeBGM();
        }

    }

    public void Setting()
    {
        Time.timeScale = 0;
        SoundManager.Instance.PauseBGM();
        settingPanel.SetActive(true);
    }

    IEnumerator TimeScale(int time)
    {
        countText.gameObject.SetActive(true);
        int count = time;
        while (count > 0)
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
