using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public GameObject dontDestoryObject;

    private void Awake()
    {
        
    }

    private void Start()
    {
        DontDestroyOnLoad(dontDestoryObject);
    }
}
