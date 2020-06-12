using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
	private int upgrade_health;
	private int upgrade_stemina;
	private int upgrade_swim;
	private int upgrade_power;

	public int _upgrade_health { get => upgrade_health; set => upgrade_health = value; }
	public int _upgrade_stemina { get => upgrade_stemina; set => upgrade_stemina = value; }
	public int _upgrade_swim { get => upgrade_swim; set => upgrade_swim = value; }
	public int _upgrade_power { get => upgrade_power; set => upgrade_power = value; }
}
