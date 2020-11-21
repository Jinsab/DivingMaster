using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public GameObject BackgroundMusic;
    public AudioSource BGM;

    private void Awake()
    {
        BackgroundMusic = GameObject.Find("BackgroundMusic");

        BGM = BackgroundMusic.GetComponent<AudioSource>(); //배경음악 저장해둠

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
