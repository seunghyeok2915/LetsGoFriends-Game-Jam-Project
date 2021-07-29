using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PointText : MonoBehaviour
{
   public Text text;



    public void SetText(int point)
    {
        text.transform.position = Vector3.zero;
        text.text = ($"+{point}");
        text.rectTransform.DOAnchorPosY(200,1).OnComplete(() => gameObject.SetActive(false));
    }
}
