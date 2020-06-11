using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
	public MapDepth depth;
	public Status status;
	
	public IEnumerator InGame()
	{
		yield return new WaitForSeconds(1f);

		StartCoroutine("HpController");
	}

	IEnumerator HpController()
	{
		status._HP -= depth._WaterPressure;

		yield return new WaitForSeconds(1f);

		if (status._HP <= 0) StartCoroutine("DiePlayer");
		else StartCoroutine("HpController");
	}

	IEnumerator DiePlayer()
	{
		yield return new WaitForSeconds(1f);
	}
}