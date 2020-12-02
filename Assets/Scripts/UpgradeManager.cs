using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
	private Data Health
	{
		get => GameManager.uphealth;
		set => GameManager.uphealth = value;
	}
	private Data Stemina
	{
		get => GameManager.upstemina;
		set => GameManager.upstemina = value;
	}
	private Data Swim
	{
		get => GameManager.upswim;
		set => GameManager.upswim = value;
	}
	private Data Power
	{
		get => GameManager.uppower;
		set => GameManager.uppower = value;
	}
	private Data Reward
	{
		get => GameManager.upreward;
		set => GameManager.upreward = value;
	}
	private Data Luck
	{
		get => GameManager.upluck;
		set => GameManager.upluck = value;
	}

	public Status status;
	public Diving diving;
	public GetCoin[] getCoin;
	public StatText statText;
	public UpgradeText upgradeText;

	public void SettingCoin(GetCoin[] coin)
	{
		for (int i = 0; i < getCoin.Length; i++)
		{
			coin[i].SetCoinText();
		}
	}

	private void Start()
	{
		Health = new Data(PlayerPrefs.GetString("uphealth"), 1, 100, 20);
		Stemina = new Data(PlayerPrefs.GetString("upstemina"), 1, 100, 20);
		Swim = new Data(PlayerPrefs.GetString("upswim"), 1, 1, 100);
		Power = new Data(PlayerPrefs.GetString("uppower"), 1, 1, 300);
		Reward = new Data(PlayerPrefs.GetString("upreward"), 1, 1, 1000);
		Luck = new Data(PlayerPrefs.GetString("upluck"), 1, 1, 1000);

		statText.SettingHealth(Health);
		statText.SettingStemina(Stemina);
		statText.SettingSwim(Swim);
		statText.SettingPower(Power);
		upgradeText.SettingReward(Reward);
		upgradeText.SettingLuck(Luck);

		diving.SetMaxSP(Stemina.Value);
		diving._swimPower = Swim.Value;
		diving._touchPower = Power.Value;

		//PlayerPrefs.SetInt("reward", Reward.Value);
		SecurePlayerPrefs.SetString("reward", Reward.Value.ToString(), SecurePlayerPrefs.GetData("Onyxia"));
	}

	public void UpgradeHealth() {
		if (status._coin >= Health.Cost)
		{
			status._coin -= Health.Cost;

			Health = GameManager.HealthTable[Health.Level + 1];
			//Health.Level++;
			//Health.Value += (Health.Value / 10);
			//Health.Cost += (Health.Cost / 2);

			//PlayerPrefs.SetInt("coin", status._coin);
			//PlayerPrefs.SetInt("health", Health.Value);
			//PlayerPrefs.SetString("uphealth", Health.ToString());

			SecurePlayerPrefs.SetString("coin", status._coin.ToString(), SecurePlayerPrefs.GetData("Nefarian"));
			SecurePlayerPrefs.SetString("health", Health.Value.ToString(), SecurePlayerPrefs.GetData("Deathwing"));
			PlayerPrefs.SetString("uphealth", Health.ToString());

			SettingCoin(getCoin);
			statText.SettingHealth(Health);
			status._HP = Health.Value;
			status.hpSlider.maxValue = status._HP;
		}
		else {
			Debug.Log("Insufficient coin.");
		}
	}

	public void UpgradeStemina() {
		if (status._coin >= Stemina.Cost)
		{
			status._coin -= Stemina.Cost;

			Stemina = GameManager.SteminaTable[Stemina.Level + 1];
			//Stemina.Level++;
			//Stemina.Value += (Stemina.Value / 10);
			//Stemina.Cost += (Stemina.Cost / 2);

			//PlayerPrefs.SetInt("coin", status._coin);
			//PlayerPrefs.SetInt("stemina", Stemina.Value);
			//PlayerPrefs.SetString("upstemina", Stemina.ToString());

			SecurePlayerPrefs.SetString("coin", status._coin.ToString(), SecurePlayerPrefs.GetData("Nefarian"));
			SecurePlayerPrefs.SetString("stemina", Stemina.Value.ToString(), SecurePlayerPrefs.GetData("Ysera"));
			PlayerPrefs.SetString("upstemina", Stemina.ToString());

			SettingCoin(getCoin);
			statText.SettingStemina(Stemina);
			status._SP = Stemina.Value;
			status.spSlider.maxValue = status._SP;
			diving.SetMaxSP(Stemina.Value);
		}
		else
		{
			Debug.Log("Insufficient coin.");
		}
	}

	public void UpgradeSwim() {
		if (status._coin >= Swim.Cost)
		{
			status._coin -= Swim.Cost;

			Swim = GameManager.SwimTable[Swim.Level + 1];
			//Swim.Level++;
			//Swim.Value++;
			//Swim.Cost += (Swim.Cost / 2);

			SecurePlayerPrefs.SetString("coin", status._coin.ToString(), SecurePlayerPrefs.GetData("Nefarian"));
			PlayerPrefs.SetString("upswim", Swim.ToString());
			SettingCoin(getCoin);
			statText.SettingSwim(Swim);
			diving._swimPower = Swim.Value;
		}
		else {
			Debug.Log("Insufficient coin.");
		}
	}

	public void UpgradePower() {
		if (status._coin >= Power.Cost)
		{
			status._coin -= Power.Cost;

			Power = GameManager.PowerTable[Power.Level + 1];

			SecurePlayerPrefs.SetString("coin", status._coin.ToString(), SecurePlayerPrefs.GetData("Nefarian"));
			PlayerPrefs.SetString("uppower", Power.ToString());
			SettingCoin(getCoin);
			statText.SettingPower(Power);
			diving._touchPower = Power.Value;
		}
		else {
			Debug.Log("Insufficient coin.");
		}
	}

	public void UpgradeReward() {
		if (status._coin >= Reward.Cost)
		{
			status._coin -= Reward.Cost;

			Reward = GameManager.RewardTable[Reward.Level + 1];

			SecurePlayerPrefs.SetString("coin", status._coin.ToString(), SecurePlayerPrefs.GetData("Nefarian"));
			PlayerPrefs.SetString("upreward", Reward.ToString());
			SettingCoin(getCoin);
			upgradeText.SettingReward(Reward);
			//PlayerPrefs.SetInt("reward", Reward.Value);
			SecurePlayerPrefs.SetString("reward", Reward.Value.ToString(), SecurePlayerPrefs.GetData("Onyxia"));
		}
		else
		{
			Debug.Log("Insufficient coin.");
		}
	}

	public void UpgradeLuck() {
		if (status._coin >= Luck.Cost)
		{
			status._coin -= Luck.Cost;

			Luck = GameManager.LuckTable[Luck.Level + 1];

			SecurePlayerPrefs.SetString("coin", status._coin.ToString(), SecurePlayerPrefs.GetData("Nefarian"));
			PlayerPrefs.SetString("upluck", Luck.ToString());
			SettingCoin(getCoin);
			upgradeText.SettingLuck(Luck);
			//
		}
		else
		{
			Debug.Log("Insufficient coin.");
		}
	}
}

/* 체력 : 레벨 1 벨류 100 코스트 20
 * 기력 : 레벨 1 벨류 100 코스트 20
 * 자동 하강 : 레벨 1 벨류 1 코스트 100
 * 터치 하강 : 레벨 1 벨류 1 코스트 100
 */ 

/* 체력/기력 상승 공식 : (기존수치 + 기존수치/10) / ex) 100 + 10, 9765 + 976 / 100, 110, 121, 133, 146, 160, 176, 193, 212
 * 체력/기력 골드 상승 공식 : (기존수치 + 기존수치/2) / ex) 20 / 30 / 45 / 67 / 100 / 150 / 225 / 337
 * 
 * 자동 하강/터치 하강 상승 공식 : 기존수치 + 1
 * 자동 하강/터치 하강 골드 상승 공식 : (기존수치 + 기존수치/2) / 
 */
