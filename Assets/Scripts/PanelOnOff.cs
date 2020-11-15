using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOnOff : MonoBehaviour
{
    public GameObject Panel;

    public void TouchOnOptionButton()
    {
        Panel.SetActive(true);
    }

    public void TouchOffOptionButton()
    {
        Panel.SetActive(false);
    }
}
