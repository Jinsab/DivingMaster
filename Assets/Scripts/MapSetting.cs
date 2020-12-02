using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSetting : MonoBehaviour
{
	public Text mapName;
	public Text waterPressure;
	public Text spPower;
	public Text depth;
	public Text unlockDepth;
	public MapDepth mapDepth;

	private Map MapInfo
    {
		get => MapDepth.MapInfomation;
		set => MapDepth.MapInfomation = value;
    }

	private Map PriviousMapInfo
    {
		get => MapDepth.MapPreviousInfomation;
		set => MapDepth.MapPreviousInfomation = value;
	}

	private void Start()
	{
		SettingText();
	}

	public void SettingText() {
		if (mapDepth.mapLevel == 1)
        {
			MapInfo = MapDepth.MapTable[mapDepth.mapLevel];

			mapName.text = MapInfo.Name;
			waterPressure.text = "수압: " + "<color=#ff0000>" + MapInfo.Preessure.ToString() + "</color>";
			spPower.text = "저항: " + "<color=#ff0000>" + MapInfo.Swimming.ToString() + "</color>";
			depth.text = "깊이: " + "<color=#ff0000>" + MapInfo.StageDepth.ToString() + "</color>";
			unlockDepth.text = "해금: " + "<color=#ff0000>" + 0 + "</color>";
		}
		else
        {
			MapInfo = MapDepth.MapTable[mapDepth.mapLevel];
			PriviousMapInfo = MapDepth.MapTable[mapDepth.mapLevel - 1];

			mapName.text = MapInfo.Name;
			waterPressure.text = "수압: " + "<color=#ff0000>" + MapInfo.Preessure.ToString() + "</color>";
			spPower.text = "저항: " + "<color=#ff0000>" + MapInfo.Swimming.ToString() + "</color>";
			depth.text = "깊이: " + "<color=#ff0000>" + MapInfo.StageDepth.ToString() + "</color>";
			unlockDepth.text = "해금: " + "<color=#ff0000>" + PriviousMapInfo.StageDepth.ToString() + "</color>";
		}
	}
}
