using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraSize : MonoBehaviour
{
    private Camera mainCam;

    void Awake()
    {
        mainCam = Utils.Bind<Camera>(gameObject, "");
    }

    void Update()
    {

         //mainCam.orthographicSize
        // mainCam.DOOrthoSize

    }
}
