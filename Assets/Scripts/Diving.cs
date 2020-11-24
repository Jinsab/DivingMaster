using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Diving : MonoBehaviour, IPointerDownHandler
{
	[SerializeField] private int swimPower;
	[SerializeField] private int touchPower;
	public Animator animator;

	public int _swimPower { get => swimPower; set => swimPower = value; }
	public int _touchPower { get => touchPower; set => touchPower = value; }

	public MapDepth depth;
	public Status status;
	public AudioSource swimEffect;
	public Text message;

	private float stTime = 0f;
	private float plusTime = 0f;
	private int maxSP;
	private int lastTouchSP;

	private void Start()
	{
		SetMaxSP(status._SP);
		depth.maxDepthText.text = "최대 수심 : " + status._maxDepth + "M";
	}

	private void Update()
	{
		depth.depthText.text = "수심 : " + status._myDepth + "M";

		stTime += Time.deltaTime;

		if (status._HP > 0)
		{
			if (stTime >= 3f && status._SP < maxSP)
			{
				plusTime += Time.deltaTime;
				status._SP = (int)Mathf.Clamp(Mathf.Lerp(lastTouchSP, maxSP, plusTime / 3), 0, maxSP);
			}
		}
	}

	IEnumerator Swim() {
		if (depth._Depth > status._myDepth && status._HP > 0) {
			status._myDepth += swimPower;

			yield return new WaitForSeconds(1f);

			StartCoroutine("Swim");
		}
		else {
			// 체력으로 사망 시 올려줘야됨, 또한 Escape 탈출 시엔 올려주면 안됨
			if (status._HP == 0 && !status.isEscape)
			{
				status._myDepth += swimPower;
			}

			StopCoroutine("Swim");
		}
	}

	public IEnumerator Fade()
	{
		message.color = new Color(message.color.r, message.color.g, message.color.b, 1);

		while (message.color.a > 0.0f)
		{
			message.color = new Color(message.color.r, message.color.g, message.color.b, message.color.a - (Time.deltaTime / 2.0f));
			yield return null;
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (status._HP > 0)
		{
			//Debug.Log("Get Swim Touch");
			stTime = 0f;
			plusTime = 0f;

			if (status._SP - depth._SwimmingSP >= 0 && status._myDepth < depth._Depth)
			{
				status._SP -= depth._SwimmingSP;
				status._myDepth += touchPower;

				swimEffect.Play();
				Running();

				Debug.Log("Touch!!");
			}
			else {
				if (status._SP - depth._SwimmingSP <= 0)
				{
					StopCoroutine("Fade");
					StartCoroutine("Fade");

					Debug.Log("Warring: not have stemina!!");
				}
				else
					Debug.Log("Warring: your already clear!!");
			}

			lastTouchSP = status._SP;
		}
	}

	public void Running()
	{
		animator.Play("Swim", -1);
	}

	public void SetMaxSP(int sp) => maxSP = sp;
}