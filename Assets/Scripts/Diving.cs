using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Diving : MonoBehaviour, IPointerDownHandler
{
	[SerializeField] private int swimPower;
	[SerializeField] private int touchPower;

	public int _swimPower { get => swimPower; set => swimPower = value; }
	public int _touchPower { get => touchPower; set => touchPower = value; }

	[SerializeField] private MapDepth depth;
	[SerializeField] private Status status;
	
	private float stTime = 0f;
	private float plusTime = 0f;
	private float maxSP;
	
	private void Start()
	{
		maxSP = status._SP;
		depth.maxDepthText.text = "최대 수심 : " + status._maxDepth + "M";
	}

	private void Update()
	{
		depth.depthText.text = "수심 : " + status._myDepth + "M";

		stTime += Time.deltaTime;
		
		if (stTime >= 3f && status._SP < maxSP)
		{
			plusTime += Time.deltaTime;

			if (plusTime >= 0.1f)
			{
				status._SP = (int)Mathf.Lerp(status._SP, (status._SP + maxSP / 20), stTime);
				plusTime = 0f;
			}
		}
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
		//Debug.Log("Get Swim Touch");
		stTime = 0f;

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