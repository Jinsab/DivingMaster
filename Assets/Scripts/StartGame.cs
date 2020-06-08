using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
	[SerializeField] private int WaterPressure = 5;
	public Status status;
	
	public IEnumerator InGame()
	{
		yield return new WaitForSeconds(1f);

		StartCoroutine("HpController");
	}

	IEnumerator HpController()
	{
		status._HP -= WaterPressure;

		yield return new WaitForSeconds(1f);

		if (status._HP <= 0) StartCoroutine("DiePlayer");
		else StartCoroutine("HpController");
	}

	IEnumerator DiePlayer()
	{
		yield return new WaitForSeconds(1f);
	}
}