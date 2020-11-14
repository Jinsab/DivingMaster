using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    public GameObject Panel;
    public Text gameText;

    public void TouchOnOptionButton()
    {
        Panel.SetActive(true);
        gameText.gameObject.SetActive(false);
    }

    public void TouchOffOptionButton()
    {
        Panel.SetActive(false);
        gameText.gameObject.SetActive(true);
    }
}