﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetCoin : MonoBehaviour
{
	void Start()
    {
		SetCoinText();
    }

	public void SetCoinText() {
		Text coinText = gameObject.GetComponent<Text>();
		coinText.text = SecurePlayerPrefs.GetString("coin", SecurePlayerPrefs.GetData("Nefarian"), 0);
	}
}
