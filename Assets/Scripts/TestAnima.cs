using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnima : MonoBehaviour
{
	Animator animator;
	bool swim = false;

	void Start()
    {
		animator = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.E)) {
			if (swim == true) swim = false;
			else swim = true;
		}	
	
		animator.SetBool("isswim", swim);
    }
}
