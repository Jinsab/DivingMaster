using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
	public MapDepth depth;
	public Status status;
	public GameObject player;
	public UpgradeManager upgrade;
	public Text text;

	public IEnumerator InGame()
	{
		yield return new WaitForSeconds(1f);

		StartCoroutine("HpController");
	}

	IEnumerator HpController()
	{
		status._HP -= depth._WaterPressure;
		status._HP = Mathf.Clamp(status._HP, 0, int.MaxValue);

		yield return new WaitForSeconds(1f);

		if (status._HP <= 0) StartCoroutine("DiePlayer");
		else if (depth._Depth <= status._myDepth) StartCoroutine("Clear");
		else StartCoroutine("HpController");
	}

	IEnumerator DiePlayer()
	{
		float time = 0f;
		Quaternion quaternion = Quaternion.identity;
		quaternion.eulerAngles = new Vector3(0, 0, -180f);

		while (transform.rotation.eulerAngles.z < 180f)
		{
			time += Time.deltaTime;
			transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, time * 0.5f);

			yield return null;
		}

		Debug.Log("정산 중");
		yield return new WaitForSeconds(1f);

		status._coin = ReturnCoin(status._coin);
		status._maxDepth = ReturnMaxDepth(status._myDepth);

		PlayerPrefs.SetInt("maxDepth", status._maxDepth);
		PlayerPrefs.SetInt("coin", status._coin);
		
		Debug.Log("정산 완료");
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	IEnumerator Clear() {
		text.gameObject.SetActive(true);

		float time = 0f;

		while (transform.position.y > 0f)
		{
			time += Time.deltaTime;
			transform.position = new Vector3(0f, Mathf.Lerp(transform.position.y, 0f, time * 0.5f), 0f);

			yield return null;
		}

		Debug.Log("정산 중");
		yield return new WaitForSeconds(1f);

		status._coin = ReturnCoin(status._coin);
		status._maxDepth = ReturnMaxDepth(status._myDepth);

		PlayerPrefs.SetInt("maxDepth", status._maxDepth);
		PlayerPrefs.SetInt("coin", status._coin);

		Debug.Log("정산 완료");
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public int ReturnCoin(int retain) {
		int coin = ((depth._WaterPressure*(depth.mapLevel + 5)) + (status._myDepth/10));
		retain += coin;

		return retain;
	}

	public int ReturnMaxDepth(int retain) {
		if (status._maxDepth < retain)
		{
			retain = status._myDepth;
		}
		else
		{
			retain = status._maxDepth;
		}

		return retain;
	}
}