using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapDepth : MonoBehaviour
{
	[SerializeField] private int WaterPressure = 5;
	[SerializeField] private int SwimmingSP = 5;
	[SerializeField] private int Depth;
	
	public int _WaterPressure { get => WaterPressure; set => WaterPressure = value; }
	public int _SwimmingSP { get => SwimmingSP; set => SwimmingSP = value; }
	public int _Depth { get => Depth; set => Depth = value; }

	public Text depthText;
}