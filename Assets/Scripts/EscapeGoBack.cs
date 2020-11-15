using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscapeGoBack : MonoBehaviour
{
    public GameObject escapePanel;

    public void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Escape) && escapePanel.activeSelf == false)
        {
            Debug.Log("입력은 받음");
            escapePanel.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && escapePanel.activeSelf == true)
        {
            escapePanel.SetActive(false);
        }
#else
            if(Application.platform == RuntimePlatform.Android)
            {
                if (Input.GetKeyDown(KeyCode.Escape) && escapePanel.activeSelf == false)
                {
                    escapePanel.SetActive(true);
                }
            }
#endif
    }
}
