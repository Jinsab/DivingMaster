using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CSVReader
{
	public static Dictionary<int, T> ReadCSV<T>(string value, Func<string[], T> creator)
	{
		var lines = value.Split('\n');
		var dict = new Dictionary<int, T>();

		for(var i = 1; i < lines.Length; i++)
		{
			var line = lines[i];

			var values = line.Split(',');
			T data = creator(values);

			dict.Add(i, data);
		}

		return dict;
	}

	//[MenuItem("Tool/PlayerPrefs/ClearCache")]
	public static void ClearPlayerPrefs()
	{
		PlayerPrefs.DeleteAll();
	}
}
