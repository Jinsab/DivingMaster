using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SetSound : MonoBehaviour
{
    public Slider volume;
    public new string name;
    public AudioMixer audioMixer;

    public void AudioControl()
    {
        float bgmVolume = volume.value;

        if (bgmVolume == -40f)
        {
            audioMixer.SetFloat(name, -80f);
        }
        else
        {
            audioMixer.SetFloat(name, bgmVolume);
        }
    }
}
