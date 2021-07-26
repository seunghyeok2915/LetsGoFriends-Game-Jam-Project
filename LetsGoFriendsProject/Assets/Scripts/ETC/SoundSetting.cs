using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour
{
    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider fxSlider;

    private List<Slider> sliders;

    private void Start()
    {
        sliders = new List<Slider>
        {
            masterSlider,
            bgmSlider,
            fxSlider
        };

        sliders.ForEach((x) =>
        x.onValueChanged.AddListener((value) =>
        {
            x.value = value;
            AdjustVolumes();
        }));
    }

    private void AdjustVolumes()
    {
        SoundManager.Instance.AdjustMasterVolume(masterSlider.value);
        SoundManager.Instance.AdjustBGMVolume(bgmSlider.value);
        SoundManager.Instance.AdjustFxVoulme(fxSlider.value);
    }
}
