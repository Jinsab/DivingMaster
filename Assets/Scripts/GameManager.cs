using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	static int health;
	static int stemina;
	static int maxDepth;
	static int coin;
	static int up_Health;
	static int up_Stemina;
	static int up_Swim;
	static int up_Power;

	void Start()
    {
		PlayerPrefs.GetInt("health");
		PlayerPrefs.GetInt("stemina");
		PlayerPrefs.GetInt("maxDepth");
		PlayerPrefs.GetInt("coin");
		PlayerPrefs.GetInt("up_Health");
		PlayerPrefs.GetInt("up_Stemina");
		PlayerPrefs.GetInt("up_Swim");
		PlayerPrefs.GetInt("up_Power");
	}

    /* Update is called once per frame
    void Update()
    {
        
    } */
}
