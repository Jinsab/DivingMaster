using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageInfomation : MonoBehaviour
{
    [Header("페이지")]
    public GameObject[] pages;

    [Header("상호작용 버튼")] 
    public Button previousButton;
    public Button nextButton;

    private int index = 0;

    void Start()
    {
        // 여러 페이지가 활성화 되있을 수 있음
        foreach (GameObject page in pages)
        {
            page.SetActive(false);
        }

        // 페이지가 하나도 없다면 접근 시 오류 발생
        // 가장 첫 페이지 활성화 작업
        if (pages.Length != 0)
        {
            pages[0].SetActive(true);
        }
        else
        {
            Debug.Log("Element Error: Empty Pages");
        }
    }

    public void PreviousPage()
    {
        if (index > 0)
        {
            pages[index].SetActive(false);
            pages[--index].SetActive(true);
        }
        else
        {
            Debug.Log("Element Error: Page Underflow");
        }
    }

    public void NextPage()
    {
        if (index < pages.Length - 1)
        {
            pages[index].SetActive(false);
            pages[++index].SetActive(true);
        }
        else
        {
            Debug.Log("Element Error: Page OverFlow");
        }
    }
}
