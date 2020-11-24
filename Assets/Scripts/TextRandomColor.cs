using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextRandomColor : MonoBehaviour
{
    private Text text;
    private Color randomColor;
    public float duration = 5; // This will be your time in seconds.
    public float smoothness = 0.02f; // This will determine the smoothness of the lerp. Smaller values are smoother. Really it's the time between updates.

    void Start()
    {
        text = gameObject.GetComponent<Text>();

        text.color = new Color(Random.value, Random.value, Random.value);

        StartCoroutine(ChangeColor());
    }

    IEnumerator ChangeColor()
    {
        while (true)
        {
            float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
            float increment = smoothness / duration;

            randomColor = new Color(Random.value, Random.value, Random.value);

            while (progress < 1)
            {
                text.color = Color.Lerp(text.color, randomColor, progress);

                progress += increment;

                yield return new WaitForSeconds(smoothness);
            }

            yield return null;
        }
    }

    // 출처: https://redccoma.tistory.com/145 [My Data Factory]
}
