using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V1AnimateGraph : MonoBehaviour
{
    public GameObject[] graphBars;
    public ParticleSystem particles;

    private void OnEnable()
    {
        StartCoroutine(AnimateGraph());
    }

    private IEnumerator AnimateGraph()
    {
        for (int i = 0; i < graphBars.Length; i++)
        {
            switch (i)
            {
                case 0:
                    while (graphBars[i].transform.localScale.y < 1.25f)
                    {
                        graphBars[i].transform.localScale += new Vector3(0, 0.25f, 0);
                        yield return null;
                    }
                    break;
                case 1:
                    while (graphBars[i].transform.localScale.y < 1.425f)
                    {
                        graphBars[i].transform.localScale += new Vector3(0, 0.25f, 0);
                        yield return null;
                    }
                    break;
                case 2:
                    while (graphBars[i].transform.localScale.y < 1.374f)
                    {
                        graphBars[i].transform.localScale += new Vector3(0, 0.25f, 0);
                        yield return null;
                    }
                    break;
                case 3:
                    while (graphBars[i].transform.localScale.y < 1.3f)
                    {
                        graphBars[i].transform.localScale += new Vector3(0, 0.25f, 0);
                        yield return null;
                    }
                    break;
                case 4:
                    while (graphBars[i].transform.localScale.y < 1.425f)
                    {
                        graphBars[i].transform.localScale += new Vector3(0, 0.25f, 0);
                        yield return null;
                    }
                    break;
                case 5:
                    while (graphBars[i].transform.localScale.y < 2.125f)
                    {
                        graphBars[i].transform.localScale += new Vector3(0, 0.25f, 0);
                        yield return null;
                    }
                    break;
                case 6:
                    while (graphBars[i].transform.localScale.y < 3.25f)
                    {
                        graphBars[i].transform.localScale += new Vector3(0, 0.25f, 0);
                        yield return null;
                    }
                    break;
                case 7:
                    while (graphBars[i].transform.localScale.y < 4.125f)
                    {
                        graphBars[i].transform.localScale += new Vector3(0, 0.25f, 0);
                        yield return null;
                    }
                    break;
                case 8:
                    while (graphBars[i].transform.localScale.y < 5.125f)
                    {
                        graphBars[i].transform.localScale += new Vector3(0, 0.25f, 0);
                        yield return null;
                    }
                    break;
                case 9:
                    while (graphBars[i].transform.localScale.y < 6.4f)
                    {
                        graphBars[i].transform.localScale += new Vector3(0, 0.25f, 0);
                        yield return null;
                    }
                    break;
            }
        }
        particles.Play();
    }
}
