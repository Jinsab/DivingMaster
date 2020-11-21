using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundPlayer : MonoBehaviour
{
    private AudioSource BGM;

    private void Awake()
    {
        BGM = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (BGM.isPlaying)
        {
            return; //배경음악이 재생되고 있다면 패스
        }
        else
        {
            BGM.Play();
        }
    }
}
