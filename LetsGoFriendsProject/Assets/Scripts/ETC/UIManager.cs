using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject settingPanel;
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 0;
            settingPanel.SetActive(true);
        }
    }

    public void Close()
    {
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
