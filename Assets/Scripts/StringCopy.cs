using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StringCopy : MonoBehaviour
{
    public Text copyText;
    private Text originalText;
    private string str;

    private void Start()
    {
        originalText = gameObject.GetComponent<Text>();
    }

    private void Update()
    {
        str = copyText.text;
        var value = str.Split(':');

        originalText.text = value[1].ToString();
    }
}
