using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct Map
{
	public static string stringFormat = "Level:{0}:Name:{1}:Preessure:{2},Swimming:{3},StageDepth:{4}";

	public override string ToString()
	{
		return string.Format(stringFormat, Level, Name, Preessure, Swimming, StageDepth);
	}

	public Map(string data, int defaultLevel = 1, string defaultName = "안전한 여울", int defaultPreessure = 5, int defaultSwimming = 5, int defaultStageDepth = 100)
	{
		Level = defaultLevel;
		Name = defaultName;
		Preessure = defaultPreessure;
		Swimming = defaultSwimming;
		StageDepth = defaultStageDepth;

		if (string.IsNullOrEmpty(data))
			return;

		var values = data.Split(',');

		Level = int.Parse(values[0].Split(':')[1]);
		Name = values[1].Split(':')[1].ToString();
		Preessure = int.Parse(values[2].Split(':')[1]);
		Swimming = int.Parse(values[3].Split(':')[1]);
		StageDepth = int.Parse(values[4].Split(':')[1]);
	}

	public Map(string[] values, int defaultLevel = 1, string defaultName = "안전한 여울", int defaultPreessure = 5, int defaultSwimming = 5, int defaultStageDepth = 100)
	{
		Level = defaultLevel;
		Name = defaultName;
		Preessure = defaultPreessure;
		Swimming = defaultSwimming;
		StageDepth = defaultStageDepth;

		if (string.IsNullOrEmpty(values[0]))
			return;

		Level = int.Parse(values[0]);
		Name = values[1].ToString();
		Preessure = int.Parse(values[2]);
		Swimming = int.Parse(values[3]);
		StageDepth = int.Parse(values[4]);
	}

	public int Level;
	public string Name;
	public int Preessure;
	public int Swimming;
	public int StageDepth;
}

public struct SettingData
{
	public SettingData(string name, int water, int sp, int depth) {
		n = name;
		w = water;
		s = sp;
		d = depth;
	}

	public void setting(string name, int water, int sp, int depth)
	{
		n = name;
		w = water;
		s = sp;
		d = depth;
	}

	public string n;
	public int w;
	public int s;
	public int d;
}

public class MapDepth : MonoBehaviour
{
	public static Map MapInfomation;
	public static Map MapPreviousInfomation;
	public static Dictionary<int, Map> MapTable;
	public TextAsset map_text;

	[SerializeField] private string Name = "";
	[SerializeField] private int WaterPressure = 5; // 수압 (1초당 HP 깎이는 양)
	[SerializeField] private int SwimmingSP = 5; // 저항 (터치당 SP 깎이는 양)
	[SerializeField] private int Depth; // 깊이 (최대 깊이)
	[SerializeField] private int maxDepth; // 현재 달성한 최고기록
	public string _Name { get => Name; set => Name = value; }
	public int _WaterPressure { get => WaterPressure; set => WaterPressure = value; }
	public int _SwimmingSP { get => SwimmingSP; set => SwimmingSP = value; }
	public int _Depth { get => Depth; set => Depth = value; }
	public int mapLevel = 1;
	public Text depthText;
	public Text maxDepthText;
	public Text mapMaxDepthText;
	public Text lockMessage;
	public Text unLockMessage;
	public SettingData set;

	private int data = 0;

	private void Start()
	{
		//mapLevel = PlayerPrefs.GetInt("maplevel");
		//maxDepth = PlayerPrefs.GetInt("maxDepth");

		mapLevel = int.Parse(SecurePlayerPrefs.GetString("mapLevel", SecurePlayerPrefs.GetData("Alexstrasza"), 1));
		maxDepth = int.Parse(SecurePlayerPrefs.GetString("mapDepth", SecurePlayerPrefs.GetData("Neltharion"), 0));

		Initialize();

		MapInfomation = MapTable[mapLevel];

		Name = MapInfomation.Name;
		WaterPressure = MapInfomation.Preessure;
		SwimmingSP = MapInfomation.Swimming;
		Depth = MapInfomation.StageDepth;

		mapMaxDepthText.text = $"목표 수심: {MapInfomation.StageDepth}M";
    }

	public int UnlockInfomation()
    {
		for (int i = 0; i < MapTable.Count; i++)
        {
			if (MapTable[i].StageDepth < maxDepth)
            {
				data++;
            }
        }

		return data;
    }

	public void Setting()
	{
        if (mapLevel == 1)
        {
			Name = MapInfomation.Name;
            WaterPressure = MapInfomation.Preessure;
            SwimmingSP = MapInfomation.Swimming;
            Depth = MapInfomation.StageDepth;

            mapMaxDepthText.text = $"목표 수심: {MapInfomation.StageDepth}M";

			AllFade();
			StopCoroutine("UnLockFade");
			StartCoroutine("UnLockFade");
		}
        else if (maxDepth >= MapPreviousInfomation.StageDepth)
		{
			MapPreviousInfomation = MapTable[mapLevel - 1];

			Name = MapInfomation.Name;
			WaterPressure = MapInfomation.Preessure;
			SwimmingSP = MapInfomation.Swimming;
			Depth = MapInfomation.StageDepth;

			mapMaxDepthText.text = $"목표 수심: {MapInfomation.StageDepth}M";

			AllFade();
			StopCoroutine("UnLockFade");
			StartCoroutine("UnLockFade");
		}
		else
        {
			AllFade();
			StopCoroutine("LockFade");
			StartCoroutine("LockFade");
        }
	}

	public void PreviousLevel() {
		if (!(mapLevel == 1) || !(mapLevel < 24)) {
			mapLevel--;

			//PlayerPrefs.SetInt("maplevel", mapLevel);
			SecurePlayerPrefs.SetString("mapLevel", mapLevel.ToString(), SecurePlayerPrefs.GetData("Alexstrasza"));

			MapInfomation = MapTable[mapLevel];
		}
	}

	public void NextLevel() {
		if (!(mapLevel == 24) || !(mapLevel > 1)) {
			mapLevel++;

			//PlayerPrefs.SetInt("maplevel", mapLevel);
			SecurePlayerPrefs.SetString("mapLevel", mapLevel.ToString(), SecurePlayerPrefs.GetData("Alexstrasza"));

			MapInfomation = MapTable[mapLevel];
		}
	}

	public IEnumerator LockFade()
	{
		lockMessage.color = new Color(lockMessage.color.r, lockMessage.color.g, lockMessage.color.b, 1);

		while (lockMessage.color.a > 0.0f)
		{
			lockMessage.color = new Color(lockMessage.color.r, lockMessage.color.g, lockMessage.color.b, lockMessage.color.a - (Time.deltaTime / 2.0f));
			yield return null;
		}
	}

	public IEnumerator UnLockFade()
	{
		unLockMessage.color = new Color(unLockMessage.color.r, unLockMessage.color.g, unLockMessage.color.b, 1);

		while (unLockMessage.color.a > 0.0f)
		{
			unLockMessage.color = new Color(unLockMessage.color.r, unLockMessage.color.g, unLockMessage.color.b, unLockMessage.color.a - (Time.deltaTime / 2.0f));
			yield return null;
		}
	}

	public void AllFade()
    {
		lockMessage.color = new Color(lockMessage.color.r, lockMessage.color.g, lockMessage.color.b, 0);
		unLockMessage.color = new Color(unLockMessage.color.r, unLockMessage.color.g, unLockMessage.color.b, 0);
	}

	public void Initialize()
	{
		MapTable = CSVReader.ReadCSV<Map>(map_text.text, (value) =>
		{
			return new Map(value);
		});
	}
}