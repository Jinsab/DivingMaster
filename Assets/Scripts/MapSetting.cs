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
	public MapDepth mapDepth;

	private void Start()
	{
		SettingText();
	}

	public void SettingText() {
		mapName.text = mapDepth.set.n;
		waterPressure.text = "수압: " + "<color=#ff0000>" + mapDepth.set.w.ToString() + "</color>";
		spPower.text = "저항: " + "<color=#ff0000>" + mapDepth.set.s.ToString() + "</color>";
		depth.text = "깊이: " + "<color=#ff0000>" + mapDepth.set.d.ToString() + "</color>";
	}
}
