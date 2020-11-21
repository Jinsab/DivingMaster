using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSize : MonoBehaviour
{
    public int maxSize; // 최대 사이즈
    public int minSize; // 최소 사이즈
    public int size = 1; // 폰트 사이즈 조절량, 기본 값 1
    public float time = 0.05f; // 사이즈 줄어드는 양
    private Text text; // 텍스트 컴포넌트

    void Start()
    {
        text = GetComponent<Text>();

        StartCoroutine(sizeUp());
    }

    IEnumerator sizeUp()
    {
        while (text.fontSize < maxSize)
        {
            text.fontSize += size;

            yield return new WaitForSeconds(time);
        }

        StartCoroutine(sizeDown());
    }

    IEnumerator sizeDown()
    {
        while (text.fontSize > minSize)
        {
            text.fontSize -= size;

            yield return new WaitForSeconds(time);
        }

        StartCoroutine(sizeUp());
    }
}
