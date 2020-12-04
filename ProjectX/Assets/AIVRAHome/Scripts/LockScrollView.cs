using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockScrollView : MonoBehaviour
{
    private RectTransform scrollView;
    [SerializeField] private int itemCount;

    void Start()
    {
        scrollView = this.GetComponent<RectTransform>();
    }

    void Update()
    {
        if (scrollView.offsetMin.x > 0)
        {
            scrollView.offsetMin = new Vector2(0, 0);
            scrollView.offsetMax = new Vector2(0, 0);
        }
        if (scrollView.offsetMin.x < -300 * (itemCount - 3))
        {
            scrollView.offsetMin = new Vector2(-300 * (itemCount - 3), 0);
            scrollView.offsetMax = new Vector2(-300 * (itemCount - 3), 0);
        }
    }


}
