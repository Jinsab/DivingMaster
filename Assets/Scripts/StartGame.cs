using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
	public MapDepth depth;
	public Status status;
	public GameObject player;
	public UpgradeManager upgrade;

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
		PlayerPrefs.SetInt("up_Health", upgrade._upgrade_health);
		PlayerPrefs.SetInt("up_Stemina", upgrade._upgrade_stemina);
		PlayerPrefs.SetInt("up_Swim", upgrade._upgrade_swim);
		PlayerPrefs.SetInt("up_Power", upgrade._upgrade_power);
		
		Debug.Log("정산 완료");
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public int ReturnCoin(int retain) {
		int coin = ((depth._WaterPressure*depth._WaterPressure) + (status._myDepth/10));
		retain += coin;

		return retain;
	}

	public int ReturnMaxDepth(int retain) {
		if (status._myDepth > retain) {
			retain = status._myDepth;
		}
	
		return retain;
	}
}