using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Panel : MonoBehaviour, IPointerDownHandler
{
	public StartGame startGame;
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
		
		startGame.StartCoroutine("InGame");
		diving.StartCoroutine("Swim");
	}
}