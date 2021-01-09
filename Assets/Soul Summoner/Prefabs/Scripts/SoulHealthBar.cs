using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulHealthBar : MonoBehaviour
{
    public Transform hpBar;
    public Vector3 target;
    // Start is called before the first frame update
    public void ChangeBar(int fullhp,int currenthp)
    {
        float percentage = (float) currenthp / fullhp;

        hpBar.localScale = new Vector3(percentage, hpBar.localScale.y, hpBar.localScale.z);
        hpBar.localPosition = new Vector3((1-percentage) * 5, hpBar.localPosition.y, hpBar.localPosition.z);

        if (transform.parent.GetComponent<SoulMonster>().photonview.IsMine)
        {
            SoulGameManager.Instance.monsterBar.fillAmount = percentage;
        }

    }

    private void FixedUpdate()
    {
        target = Camera.main.transform.position;
        transform.LookAt(target,Vector3.up);
        Quaternion targetQ = Quaternion.Euler(90, transform.rotation.eulerAngles.y, 0);
        transform.rotation = targetQ;
    }
}
