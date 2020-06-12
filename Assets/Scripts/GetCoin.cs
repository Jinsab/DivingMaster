using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetCoin : MonoBehaviour
{
	//public Text coinText;

	void Start()
    {
		Text coinText = gameObject.GetComponent<Text>();
		coinText.text = PlayerPrefs.GetInt("coin").ToString();
    }
}
