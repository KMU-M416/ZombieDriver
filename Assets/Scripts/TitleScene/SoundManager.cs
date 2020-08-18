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

    public Sprite[] ButtonImage;
    
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

    public void SoundMuteButton(Toggle change)
    {
        if (change.isOn)
        {
            if (change.name.Contains("Main")) audioMixer.SetFloat("Main", -80);
            else if (change.name.Contains("BGM")) audioMixer.SetFloat("BGM", -80);
            else if (change.name.Contains("Effect")) audioMixer.SetFloat("Effect", -80);
        }
        else
        {
            if (change.name.Contains("Main")) setMainSoundValue();
            else if (change.name.Contains("BGM")) setBGMSoundValue();
            else if (change.name.Contains("Effect")) setEffectValue();
        }
    }

    public void offUI()
    {
        gameObject.SetActive(false);
    }
}
