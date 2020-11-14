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
            DontDestroyOnLoad(BackgroundMusic); //배경음악 계속 재생하게(이후 버튼매니저에서 조작)
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
