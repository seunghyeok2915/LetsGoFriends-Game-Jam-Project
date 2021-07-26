using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSet : MonoBehaviour
{


    private IEnumerator SetSkill()
    {
        Time.timeScale = 0.3f;
        yield return new WaitForSecondsRealtime(5f);
        Time.timeScale = 1;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
             StartCoroutine(SetSkill());
        }
    }
}
