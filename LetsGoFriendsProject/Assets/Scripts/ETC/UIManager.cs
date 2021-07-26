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
            settingPanel.SetActive(true);
        }
    }
}
