using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Panel : MonoBehaviour, IPointerDownHandler
{
	public StartGame startGame;
	public Diving diving;

	public void OnPointerDown(PointerEventData eventData)
	{
		//Debug.Log("Get Touch");
		gameObject.SetActive(false);
		startGame.StartCoroutine("InGame");
		diving.StartCoroutine("Swim");
	}
}