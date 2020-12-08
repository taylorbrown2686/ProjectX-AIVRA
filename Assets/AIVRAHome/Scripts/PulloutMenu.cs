using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulloutMenu : MonoBehaviour
{
    [SerializeField] private RectTransform activitiesContainer;
    private bool menuIsOpen = false;
    private bool isMenuMoving = false;

    public void OpenMenuOnclick() {
      StartCoroutine(MoveMenu());
    }

    private IEnumerator MoveMenu() {
      if (!isMenuMoving) {
        isMenuMoving = true;
        if (menuIsOpen) {
          menuIsOpen = false;
          while (activitiesContainer.anchoredPosition.x < 400f) {
            activitiesContainer.anchoredPosition += new Vector2(40f, 0);
            yield return new WaitForSeconds(0.01f);
          }
          isMenuMoving = false;
        } else {
          menuIsOpen = true;
          while (activitiesContainer.anchoredPosition.x > 0) {
            activitiesContainer.anchoredPosition -= new Vector2(40f, 0);
            yield return new WaitForSeconds(0.01f);
          }
          isMenuMoving = false;
        }
      }
    }
}
