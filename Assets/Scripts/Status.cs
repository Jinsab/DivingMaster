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

	[SerializeField] private int myDepth;
	public int _myDepth { get => myDepth; set => myDepth = value; }

	public Text hpText;
	public Text spText;

	public Slider hpSlider;
	public Slider spSlider;

	private void Start()
	{
		hpSlider.maxValue = HP;
		spSlider.maxValue = SP;
	}

	private void Update()
	{
		hpText.text = _HP.ToString();
		spText.text = _SP.ToString();

		hpSlider.value = _HP;
		spSlider.value = _SP;
	}
}
