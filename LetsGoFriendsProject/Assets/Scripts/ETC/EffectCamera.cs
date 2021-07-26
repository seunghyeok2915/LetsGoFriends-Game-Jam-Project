using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EffectCamera : MonoBehaviour
{
    private CinemachineVirtualCamera cmVcam;
    private CinemachineBasicMultiChannelPerlin noise;

    private Coroutine co = null;

    private void Awake()
    {
        cmVcam = GetComponent<CinemachineVirtualCamera>();
        noise = cmVcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public int intense;
    public int during;

    private void Start()
    {
        SetShake(intense, during);
    }

    public void SetShake(float intense, float during)
    {
        noise.m_AmplitudeGain = intense;
        if (co != null) StopCoroutine(co);
        co = StartCoroutine(ReduceShake(during));
    }

    IEnumerator ReduceShake(float during)
    {

        while (noise.m_AmplitudeGain >= 0.2f)
        {
            noise.m_AmplitudeGain = Mathf.Lerp(noise.m_AmplitudeGain, 0, Time.deltaTime / during);
            yield return null;
        }
        noise.m_AmplitudeGain = 0;
    }

    //   public static void CamShake(float intense, float during)
    // {
    //     instance.camEffect.SetShake(intense, during);
    // }

}
