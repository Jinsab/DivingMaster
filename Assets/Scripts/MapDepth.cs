using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
	private List<string> stageName = new List<string>() {"안전한 산호지대", "초원 평야", "깊고 푸른 바다", "빛을 잃어가는 바다",
														 "검고 어두운 바다", "심연", "신비로운 바다", " 심연 깊디 깊은 곳",
														 "심해 급락 지역", "잊혀져 가는 바다"};
	private List<int> stagePreessure = new List<int>() { 5, 7, 10, 15, 20, 30, 40, 50, 76, 110 };
	private List<int> stageSwimming = new List<int>() { 5, 8, 12, 19, 27, 40, 55, 85, 130, 200 };
	private List<int> stageDepth = new List<int>() { 200, 500, 800, 1300, 2000, 3600, 6200, 7800, 11000, 99999999 };

	[SerializeField] private string Name = "";
	[SerializeField] private int WaterPressure = 5; // 수압 (1초당 HP 깎이는 양)
	[SerializeField] private int SwimmingSP = 5; // 저항 (터치당 SP 깎이는 양)
	[SerializeField] private int Depth; // 깊이 (최대 깊이)

	public string _Name { get => Name; set => Name = value; }
	public int _WaterPressure { get => WaterPressure; set => WaterPressure = value; }
	public int _SwimmingSP { get => SwimmingSP; set => SwimmingSP = value; }
	public int _Depth { get => Depth; set => Depth = value; }
	public int mapLevel = 0;
	public Text depthText;
	public Text maxDepthText;
	public SettingData set;

	private void Start()
	{
		set = new SettingData(stageName[mapLevel], stagePreessure[mapLevel], stageSwimming[mapLevel], stageDepth[mapLevel]);
	}

	public void Setting()
	{
		Name = stageName[mapLevel];
		WaterPressure = stagePreessure[mapLevel];
		SwimmingSP = stageSwimming[mapLevel];
		Depth = stageDepth[mapLevel];
	}

	public void PreviousLevel() {
		if (!(mapLevel == 0) || !(mapLevel < 9)) {
			mapLevel--;
			set.setting(stageName[mapLevel], stagePreessure[mapLevel], stageSwimming[mapLevel], stageDepth[mapLevel]);
		}
	}

	public void NextLevel() {
		if (!(mapLevel == 9) || !(mapLevel > 0)) {
			mapLevel++;
			set.setting(stageName[mapLevel], stagePreessure[mapLevel], stageSwimming[mapLevel], stageDepth[mapLevel]);
		}
	}
}