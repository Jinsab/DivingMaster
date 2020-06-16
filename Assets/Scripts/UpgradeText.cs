using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeText : MonoBehaviour
{
	public Text health;
	public Text _pHealth;
	public Text _cHelath;
	public Text stemina;
	public Text _pStemina;
	public Text _cStemina;
	public Text swim;
	public Text _pSwim;
	public Text _cSwim;
	public Text power;
	public Text _pPower;
	public Text _cPower;

	public void SettingHealth(Data data) {
		health.text = "체력: " + data.Value.ToString();
		_pHealth.text = $"({data.Value.ToString()} + {(data.Value / 10).ToString()})";
		_cHelath.text = data.Cost.ToString();
	}

	public void SettingStemina(Data data)
	{
		stemina.text = "기력: " + data.Value.ToString();
		_pStemina.text = $"({data.Value.ToString()} + {(data.Value / 10).ToString()})";
		_cStemina.text = data.Cost.ToString();
	}

	public void SettingSwim(Data data)
	{
		swim.text = "자동 하강: " + data.Value.ToString();
		_pSwim.text = $"({data.Value.ToString()} + {(1).ToString()})";
		_cSwim.text = data.Cost.ToString();
	}

	public void SettingPower(Data data)
	{
		power.text = "터치 하강: " + data.Value.ToString();
		_pPower.text = $"({data.Value.ToString()} + {(1).ToString()})";
		_cPower.text = data.Cost.ToString();
	}
}