using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public static class SoundValue{
    public static float MainSoundValue;
    public static float BGMSoundValue;
    public static float EffectSoundValue;
}

public class SoundManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider MainSlider;
    public Slider BGMSlider;
    public Slider EffectSlider;
    
    public void setMainSoundValue()
    {
        audioMixer.SetFloat("Main", Mathf.Log(MainSlider.value) * 10);
        SoundValue.MainSoundValue = Mathf.Log(MainSlider.value) * 10;
    }

    public void setBGMSoundValue()
    {
        audioMixer.SetFloat("BGM", Mathf.Log(BGMSlider.value) *10);
        SoundValue.BGMSoundValue = Mathf.Log(BGMSlider.value) * 10;
    }

    public void setEffectValue()
    {
        audioMixer.SetFloat("Effect", Mathf.Log(EffectSlider.value) * 10);
        SoundValue.EffectSoundValue = Mathf.Log(EffectSlider.value) * 10;
    }

    public void offUI()
    {
        gameObject.SetActive(false);
    }
}
