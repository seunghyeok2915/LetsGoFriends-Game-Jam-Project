using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RainBowEffect : MonoBehaviour
{

    void Awake()
    {
        this.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<SpriteRenderer>().DOFade(0,1);
           // transform.GetChild(i).transform.DOShakePosition(1,1.2f);
        }
    }

    void OnDisable()
    {
         for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<SpriteRenderer>().DOFade(1,0);
        }
    }
}
