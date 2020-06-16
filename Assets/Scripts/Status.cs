using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{
	[SerializeField] private int HP;
	[SerializeField] private int SP;
	[SerializeField] private int myDepth;
	[SerializeField] private int maxDepth;
	[SerializeField] private int coin;

	public int _HP { get => HP; set => HP = value; }
	public int _SP { get => SP; set => SP = value; }
	public int _myDepth { get => myDepth; set => myDepth = value; }
	public int _maxDepth { get => maxDepth; set => maxDepth = value; }
	public int _coin { get => coin; set => coin = value; }

	public Text hpText;
	public Text spText;

	public Slider hpSlider;
	public Slider spSlider;

	private void Start()
	{
		HP = PlayerPrefs.GetInt("health", 100);
		SP = PlayerPrefs.GetInt("stemina", 100);
		
		hpSlider.maxValue = HP;
		spSlider.maxValue = SP;

		coin = PlayerPrefs.GetInt("coin");
		maxDepth = PlayerPrefs.GetInt("maxDepth");
	}

	private void Update()
	{
		hpText.text = _HP.ToString();
		spText.text = _SP.ToString();

		hpSlider.value = _HP;
		spSlider.value = _SP;
	}
}
