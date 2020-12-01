using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Panel : MonoBehaviour, IPointerDownHandler
{
	public StartGame startGame;
	public GameObject inGamePanel;
	public Button[] option;
	public Diving diving;

	public void OnPointerDown(PointerEventData eventData)
	{
		//Debug.Log("Get Touch");
		gameObject.SetActive(false);

		for (int i = 0; i < option.Length; i++)
        {
			option[i].gameObject.SetActive(false);
		}

		inGamePanel.SetActive(true);

		startGame.StartCoroutine("InGame");
		diving.StartCoroutine("Swim");
	}
}