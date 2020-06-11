using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Diving : MonoBehaviour, IPointerDownHandler
{
	[SerializeField] private int swimPower;
	public int _swimPower { get => swimPower; set => swimPower = value; }
	
	[SerializeField] private int touchPower;
	public int _touchPower { get => touchPower; set => touchPower = value; }

	[SerializeField] private MapDepth depth;
	[SerializeField] private Status status;

	private void Start()
	{
	//	StartCoroutine("Swim");
	}

	private void Update()
	{
		depth.depthText.text = "수심 : " + status._myDepth + "M";
	}

	IEnumerator Swim() {
		if (depth._Depth >= status._myDepth) {
			status._myDepth += swimPower;

			yield return new WaitForSeconds(1f);

			StartCoroutine("Swim");
		}
		else {
			StartCoroutine("Clear");
		}
	}

	IEnumerator Clear() {
		yield return new WaitForSeconds(1f);
	}


	public void OnPointerDown(PointerEventData eventData)
	{
		Debug.Log("Get Swim Touch");

		if (status._SP - depth._SwimmingSP >= 0)
		{
			status._SP -= depth._SwimmingSP;
			status._myDepth += touchPower;
		}
		else {
			Debug.Log("Warring: not have stemina!!");
		}
	}
}