using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky_TridetnBox : MonoBehaviour
{
    public List<SkyTrident> SkyTridents = new List<SkyTrident>();
    private void OnEnable()
    {
        for (int i = 0; i < SkyTridents.Count; i++)
        {
            SkyTridents[i].gameObject.SetActive(true);
        }
        StartCoroutine(SkyTridentDown());
    }
    private IEnumerator SkyTridentDown()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 6; i++)
        {
            SkyTridents[i].IsDown = true;
        }
        yield return new WaitForSeconds(0.7f);
        for (int i = 6; i < SkyTridents.Count; i++)
        {
            SkyTridents[i].IsDown = true;
        }
    }
}
