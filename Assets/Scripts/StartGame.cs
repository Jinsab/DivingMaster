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
	public GameObject endPanel;
	public GameObject rewardEndPanel;
	public Text clearText;
	public Text coinText;
	public Text rewardCoinText;
	public Text newText;
	public Button escapeButton;
	public bool isReward = false;

	//private bool isEscape = false;

	public IEnumerator InGame()
	{
		escapeButton.gameObject.SetActive(true);

		yield return new WaitForSeconds(1f);

		StartCoroutine("HpController");
	}

	IEnumerator HpController()
	{
		status._HP -= depth._WaterPressure;
		status._HP = Mathf.Clamp(status._HP, 0, int.MaxValue);

		yield return new WaitForSeconds(1f);

		if (status._HP <= 0 && !status.isEscape)
		{
			StartCoroutine("DiePlayer");
		}
		else if (depth._Depth <= status._myDepth)
		{
			
			StartCoroutine("Clear");
		}
		else
		{
			StartCoroutine("HpController");
		}
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

		// 신기록 갱신
		if (status._maxDepth < status._myDepth)
        {
			rewardEndPanel.SetActive(true);

			if (status.isEscape == true)
			{
				rewardCoinText.text = ReturnCoin(0, 1, 4 + status.dieCount).ToString();
			}
			else
			{
				rewardCoinText.text = ReturnCoin(0, 1, 1).ToString();
			}

			while (true)
			{
				if (!rewardEndPanel.activeSelf)
				{
					if (isReward == true)
					{
						if (status.isEscape == true)
						{
							status._coin = ReturnCoin(status._coin, 2, 4 + status.dieCount);
						}
						else
						{
							status._coin = ReturnCoin(status._coin, 2, 1);
							CountDown();
						}

						status._maxDepth = ReturnMaxDepth(status._myDepth);

						//PlayerPrefs.SetInt("maxDepth", status._maxDepth);
						//PlayerPrefs.SetInt("coin", status._coin);

						SecurePlayerPrefs.SetString("mapDepth", status._maxDepth.ToString(), SecurePlayerPrefs.GetData("Neltharion"));
						SecurePlayerPrefs.SetString("coin", status._coin.ToString(), SecurePlayerPrefs.GetData("Nefarian"));

						Debug.Log("정산 완료");
						SceneManager.LoadScene(SceneManager.GetActiveScene().name);
					}
					else
					{
						if (status.isEscape == true)
						{
							status._coin = ReturnCoin(status._coin, 1, 4 + status.dieCount);
						}
						else
						{
							status._coin = ReturnCoin(status._coin, 1, 1);
							CountDown();
						}

						status._maxDepth = ReturnMaxDepth(status._myDepth);

						//PlayerPrefs.SetInt("maxDepth", status._maxDepth);
						//PlayerPrefs.SetInt("coin", status._coin);

						SecurePlayerPrefs.SetString("mapDepth", status._maxDepth.ToString(), SecurePlayerPrefs.GetData("Neltharion"));
						SecurePlayerPrefs.SetString("coin", status._coin.ToString(), SecurePlayerPrefs.GetData("Nefarian"));

						Debug.Log("정산 완료");
						SceneManager.LoadScene(SceneManager.GetActiveScene().name);
					}
				}

				yield return null;
			}
		}
		// 일반 플레이
		else
        {
			endPanel.SetActive(true);

			if (status.isEscape)
			{
				coinText.text = ReturnCoin(0, 1, 4 + status.dieCount).ToString();
			}
			else
			{
				coinText.text = ReturnCoin(0, 1, 1).ToString();
			}

			while (true)
			{
				if (!endPanel.activeSelf)
				{
					if (status.isEscape)
					{
						status._coin = ReturnCoin(status._coin, 1, 4 + status.dieCount);
					}
					else
					{
						status._coin = ReturnCoin(status._coin, 1, 1);
						CountDown();
					}

					status._maxDepth = ReturnMaxDepth(status._myDepth);

					//PlayerPrefs.SetInt("maxDepth", status._maxDepth);
					//PlayerPrefs.SetInt("coin", status._coin);

					SecurePlayerPrefs.SetString("mapDepth", status._maxDepth.ToString(), SecurePlayerPrefs.GetData("Neltharion"));
					SecurePlayerPrefs.SetString("coin", status._coin.ToString(), SecurePlayerPrefs.GetData("Nefarian"));

					Debug.Log("정산 완료");
					SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				}

				yield return null;
			}
		}
	}

	IEnumerator Clear()
	{
		clearText.gameObject.SetActive(true);

        float time = 0f;

        while (transform.position.y > 0f)
        {
            time += Time.deltaTime;
            transform.position = new Vector3(0f, Mathf.Lerp(transform.position.y, 0f, time * 0.5f), 0f);

            yield return null;
        }

		// 신기록 미갱신
		if (status._maxDepth > status._myDepth)
        {
			newText.gameObject.SetActive(false);

			Debug.Log("정산 중");
			rewardEndPanel.SetActive(true);

			rewardCoinText.text = ReturnCoin(0, 4, 5).ToString();

			while (true)
			{
				if (!rewardEndPanel.activeSelf)
				{
					if (isReward == true)
					{
						status._coin = ReturnCoin(status._coin, 8, 5);

						CountDown();

						status._maxDepth = ReturnMaxDepth(status._myDepth);

						//PlayerPrefs.SetInt("maxDepth", status._maxDepth);
						//PlayerPrefs.SetInt("coin", status._coin);

						SecurePlayerPrefs.SetString("mapDepth", status._maxDepth.ToString(), SecurePlayerPrefs.GetData("Neltharion"));
						SecurePlayerPrefs.SetString("coin", status._coin.ToString(), SecurePlayerPrefs.GetData("Nefarian"));

						Debug.Log("정산 완료");
						SceneManager.LoadScene(SceneManager.GetActiveScene().name);
					}
					else
					{
						status._coin = ReturnCoin(status._coin, 4, 5);

						CountDown();

						status._maxDepth = ReturnMaxDepth(status._myDepth);

						//PlayerPrefs.SetInt("maxDepth", status._maxDepth);
						//PlayerPrefs.SetInt("coin", status._coin);

						SecurePlayerPrefs.SetString("mapDepth", status._maxDepth.ToString(), SecurePlayerPrefs.GetData("Neltharion"));
						SecurePlayerPrefs.SetString("coin", status._coin.ToString(), SecurePlayerPrefs.GetData("Nefarian"));

						Debug.Log("정산 완료");
						SceneManager.LoadScene(SceneManager.GetActiveScene().name);
					}
				}

				yield return null;
			}
		}
		else
        {
			Debug.Log("정산 중");
			rewardEndPanel.SetActive(true);

			rewardCoinText.text = ReturnCoin(0, 1, 1).ToString();

			while (true)
			{
				if (!rewardEndPanel.activeSelf)
				{
					if (isReward == true)
					{
						status._coin = ReturnCoin(status._coin, 2, 1);

						CountDown();

						status._maxDepth = ReturnMaxDepth(status._myDepth);

						//PlayerPrefs.SetInt("maxDepth", status._maxDepth);
						//PlayerPrefs.SetInt("coin", status._coin);

						SecurePlayerPrefs.SetString("mapDepth", status._maxDepth.ToString(), SecurePlayerPrefs.GetData("Neltharion"));
						SecurePlayerPrefs.SetString("coin", status._coin.ToString(), SecurePlayerPrefs.GetData("Nefarian"));

						Debug.Log("정산 완료");
						SceneManager.LoadScene(SceneManager.GetActiveScene().name);
					}
					else
					{
						status._coin = ReturnCoin(status._coin, 1, 1);

						CountDown();

						status._maxDepth = ReturnMaxDepth(status._myDepth);

						//PlayerPrefs.SetInt("maxDepth", status._maxDepth);
						//PlayerPrefs.SetInt("coin", status._coin);

						SecurePlayerPrefs.SetString("mapDepth", status._maxDepth.ToString(), SecurePlayerPrefs.GetData("Neltharion"));
						SecurePlayerPrefs.SetString("coin", status._coin.ToString(), SecurePlayerPrefs.GetData("Nefarian"));

						Debug.Log("정산 완료");
						SceneManager.LoadScene(SceneManager.GetActiveScene().name);
					}
				}

				yield return null;
			}
		}
	}

	// retain = 플레이어 보유 코인, correction = 코인 배율, reverse = 코인 역배율
	public int ReturnCoin(int retain, int correction, int reverse)
	{
		int bonus;
		//bonus = PlayerPrefs.GetInt("reward");
		bonus = int.Parse(SecurePlayerPrefs.GetString("reward", SecurePlayerPrefs.GetData("Onyxia"), 0));
		
		// correction 2라면 2배, reverse 5라면 0.2배
		int coin = ((depth._WaterPressure * (depth.mapLevel + 4 + bonus)) + (status._myDepth/10)) / reverse * correction;

		//Debug.Log("Have Coin: " + coin);

		retain += coin;

		return retain;
	}

	public int ReturnMaxDepth(int retain)
	{
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

	public void Escape()
    {
		escapeButton.gameObject.SetActive(false);
		status.isEscape = true;
		status._HP = 0;
		status.dieCount += 1;
		//PlayerPrefs.SetInt("dieCount", status.dieCount);
		SecurePlayerPrefs.SetString("dieCount", status.dieCount.ToString(), SecurePlayerPrefs.GetData("Malygos"));
		StartCoroutine(DiePlayer());
    }

	public void CountDown()
    {
		// 클리어하면 5스택씩 다운
		for (int i = 0; i < 5; i++)
        {
			// dieCount의 최소 카운트는 0
			if (0 < status.dieCount)
            {
				status.dieCount--;
            }
			else
            {
				break;
            }
        }

		//PlayerPrefs.SetInt("dieCount", status.dieCount);
		SecurePlayerPrefs.SetString("dieCount", status.dieCount.ToString(), SecurePlayerPrefs.GetData("Malygos"));
	}
}