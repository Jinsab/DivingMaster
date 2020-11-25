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

	private Map _Map
    {
		get => MapDepth.MapInfomation;
		set => MapDepth.MapInfomation = value;
    }

	private Map _PriviousMap
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
			_Map = MapDepth.MapTable[mapDepth.mapLevel];

			mapName.text = _Map.Name;
			waterPressure.text = "수압: " + "<color=#ff0000>" + _Map.Preessure.ToString() + "</color>";
			spPower.text = "저항: " + "<color=#ff0000>" + _Map.Swimming.ToString() + "</color>";
			depth.text = "깊이: " + "<color=#ff0000>" + _Map.StageDepth.ToString() + "</color>";
			unlockDepth.text = "해제: " + "<color=#ff0000>" + 0 + "</color>";
		}
		else
        {
			_Map = MapDepth.MapTable[mapDepth.mapLevel];
			_PriviousMap = MapDepth.MapTable[mapDepth.mapLevel - 1];

			mapName.text = _Map.Name;
			waterPressure.text = "수압: " + "<color=#ff0000>" + _Map.Preessure.ToString() + "</color>";
			spPower.text = "저항: " + "<color=#ff0000>" + _Map.Swimming.ToString() + "</color>";
			depth.text = "깊이: " + "<color=#ff0000>" + _Map.StageDepth.ToString() + "</color>";
			unlockDepth.text = "해제: " + "<color=#ff0000>" + _PriviousMap.StageDepth.ToString() + "</color>";
		}
	}
}
