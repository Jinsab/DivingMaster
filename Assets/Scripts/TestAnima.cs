using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestAnima : MonoBehaviour, IPointerDownHandler
{
	[SerializeField] Animator animator;
	
	void Start()
    {
		animator = GetComponent<Animator>();
	}

    public void OnPointerDown(PointerEventData eventData)
	{
		Running();

		Debug.Log("Touch!!");
	}

	public void Running()
    {
		animator.SetTrigger("Swim");
	}
}
