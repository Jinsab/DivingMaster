using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeText : MonoBehaviour
{
	public Text reward;
	public Text _pReward;
	public Text _cReward;
	public Text luck;
	public Text _pLuck;
	public Text _cLuck;

	public void SettingReward(Data data)
	{
		reward.text = "보상 강화: " + data.Value.ToString();
		_pReward.text = $"({data.Value.ToString()} + {(1).ToString()})";
		_cReward.text = data.Cost.ToString();
	}

	public void SettingLuck(Data data)
	{
		luck.text = "행운 강화: " + data.Value.ToString();
		_pLuck.text = $"({data.Value.ToString()} + {(1).ToString()})";
		_cLuck.text = data.Cost.ToString();
	}
}
