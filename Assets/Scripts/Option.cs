using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    public Text gameText;

    public void TouchOnOptionButton()
    {
        gameText.gameObject.SetActive(false);
    }

    public void TouchOffOptionButton()
    {
        gameText.gameObject.SetActive(true);
    }
}