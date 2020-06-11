using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
	public GameObject _stat;
	public GameObject _equipment;
	public GameObject _reinforce;
	public GameObject _area;

	public void Stat() {
		if (_stat.activeSelf)
			_stat.SetActive(false);
		else
			_stat.SetActive(true);
	}

	public void Equipment() {
		if (_equipment.activeSelf)
			_equipment.SetActive(false);
		else
			_equipment.SetActive(true);
	}

	public void Reinforce() {
		if (_reinforce.activeSelf)
			_reinforce.SetActive(false);
		else
			_reinforce.SetActive(true);
	}

	public void Area() {
		if (_area.activeSelf)
			_area.SetActive(false);
		else
			_area.SetActive(true);
	}
}
