using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Data
{
	public static string stringFormat = "Level:{0},Value:{1},Cost:{2}";
	public override string ToString()
	{
		return string.Format(stringFormat, Level, Value, Cost);
	}

	public Data(string data, int defaultLevel = 1, int defaultValue = 100, int defaultCost = 100)
	{
		Level = defaultLevel;
		Value = defaultValue;
		Cost = defaultCost;
	
		if (string.IsNullOrEmpty(data))
			return;

		var values = data.Split(',');

		Level = int.Parse(values[0].Split(':')[1]);
		Value = int.Parse(values[1].Split(':')[1]);
		Cost = int.Parse(values[2].Split(':')[1]);
	}

	public Data(string[] values, int defaultLevel = 1, int defaultValue = 100, int defaultCost = 100)
	{
		Level = defaultLevel;
		Value = defaultValue;
		Cost = defaultCost;

		if (string.IsNullOrEmpty(values[0]))
			return;

		Level = int.Parse(values[0]);
		Value = int.Parse(values[1]);
		Cost = int.Parse(values[2]);
	}

	public int Level;
	public int Value;
	public int Cost;
}

public enum Stat
{
	Health,
	Stemina,
	Swim,
	Power,
}

public class GameManager : MonoBehaviour
{
	static int health;
	static int stemina;
	static int maxDepth;
	static int coin;
	static int reward;
	static int luck;
	static int maplevel;

	public static Data uphealth;
	public static Data upstemina;
	public static Data upswim;
	public static Data uppower;
	public static Data upreward;
	public static Data upluck;

	public static Dictionary<int, Data> HealthTable;
	public static Dictionary<int, Data> SteminaTable;
	public static Dictionary<int, Data> SwimTable;
	public static Dictionary<int, Data> PowerTable;
	public static Dictionary<int, Data> RewardTable;
	public static Dictionary<int, Data> LuckTable;

	public TextAsset health_text;
	public TextAsset stemina_text;
	public TextAsset swim_text;
	public TextAsset power_text;
	public TextAsset reward_text;
	public TextAsset luck_text;

	void Start()
	{
		health = int.Parse(SecurePlayerPrefs.GetString("health", SecurePlayerPrefs.GetData("Deathwing"), 100));
		stemina = int.Parse(SecurePlayerPrefs.GetString("stemina", SecurePlayerPrefs.GetData("Ysera"), 100));
		Initialize();
	}

	public void Initialize()
	{
		HealthTable = CSVReader.ReadCSV<Data>(health_text.text, (value) =>
		{
			return new Data(value);
		});
		SteminaTable = CSVReader.ReadCSV<Data>(stemina_text.text, (value) =>
		{
			return new Data(value);
		});
		SwimTable = CSVReader.ReadCSV<Data>(swim_text.text, (value) =>
		{
			return new Data(value);
		});
		PowerTable = CSVReader.ReadCSV<Data>(power_text.text, (value) =>
		{
			return new Data(value);
		});
		RewardTable = CSVReader.ReadCSV<Data>(reward_text.text, (value) =>
		{
			return new Data(value);
		});
		LuckTable = CSVReader.ReadCSV<Data>(luck_text.text, (value) =>
		{
			return new Data(value);
		});
	}
}

/* 체력/기력 상승 공식 : (기존수치 + 기존수치/10) / ex) 100 + 10, 9765 + 976 / 100, 110, 121, 133, 146, 160, 176, 193, 212
 * 체력/기력 골드 상승 공식 : (기존수치 + 기존수치/2) / ex) 20 / 30 / 45 / 67 / 100 / 150 / 225 / 337
 * 
 * 자동 하강/터치 하강 상승 공식 : 기존수치 + 1
 * 자동 하강/터치 하강 골드 상승 공식 : (기존수치 + 기존수치/2) / 
 */