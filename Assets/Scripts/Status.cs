using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{
	[SerializeField] private int HP;
	public int _HP { get => HP; set => HP = value; }

	[SerializeField] private int SP;
	public int _SP { get => SP; set => SP = value;}

	public Text HpText;
	public Text SpText;

	public Slider HpSlider;
	public Slider SpSlider;

	private void Start()
	{
		HpSlider.maxValue = HP;
		SpSlider.maxValue = SP;
	}

	private void Update()
	{
		HpText.text = _HP.ToString();
		SpText.text = _SP.ToString();

		HpSlider.value = _HP;
		SpSlider.value = _SP;
	}
}
