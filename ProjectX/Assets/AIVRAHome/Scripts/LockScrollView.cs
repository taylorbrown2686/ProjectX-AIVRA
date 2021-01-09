using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockScrollView : MonoBehaviour
{
    private RectTransform scrollView;
    [SerializeField] private int itemCount, itemLengthOrWidth;
    [SerializeField] private bool isHorizontalScroll;
    [SerializeField] private int extraScrollCompensation;

    void Start()
    {
        scrollView = this.GetComponent<RectTransform>();
        if (isHorizontalScroll)
        {
            scrollView.sizeDelta = new Vector2(itemLengthOrWidth * itemCount, scrollView.sizeDelta.y);
        } else
        {
            scrollView.sizeDelta = new Vector2(scrollView.sizeDelta.x, itemLengthOrWidth * itemCount);
        }
    }

    void Update()
    {
        if (isHorizontalScroll)
        {
            if (scrollView.offsetMin.x > 0)
            {
                scrollView.offsetMin = new Vector2(0, 0);
                scrollView.offsetMax = new Vector2(0, 0);
            }
            if (scrollView.offsetMin.x < -itemLengthOrWidth * (itemCount - extraScrollCompensation))
            {
                scrollView.offsetMin = new Vector2(-itemLengthOrWidth * (itemCount - extraScrollCompensation), 0);
                scrollView.offsetMax = new Vector2(-itemLengthOrWidth * (itemCount - extraScrollCompensation), 0);
            }
        } else
        {
            if (scrollView.offsetMin.y < 0)
            {
                scrollView.offsetMin = new Vector2(0, 0);
                scrollView.offsetMax = new Vector2(0, 0);
            }
            if (scrollView.offsetMin.y > itemLengthOrWidth * (itemCount - extraScrollCompensation))
            {
                scrollView.offsetMin = new Vector2(0, itemLengthOrWidth * (itemCount - extraScrollCompensation));
                scrollView.offsetMax = new Vector2(0, itemLengthOrWidth * (itemCount - extraScrollCompensation));
            }
        }
    }

    public void ChangeItemCount(int count)
    {
        itemCount = count;
        if (isHorizontalScroll)
        {
            scrollView.sizeDelta = new Vector2(itemLengthOrWidth * itemCount, scrollView.sizeDelta.y);
        }
        else
        {
            scrollView.sizeDelta = new Vector2(scrollView.sizeDelta.x, itemLengthOrWidth * itemCount);
        }
    }


}
