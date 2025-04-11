using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndCanvs : MonoBehaviour
{
    public FadeCanvs Fadecanvs;
    public GameObject Startcanvs;
    public Button ReturnButton;
    private void Awake()
    {
        ReturnButton.onClick.AddListener(EndGame);
    }
    private void EndGame()
    {
        StartCoroutine(Ending());
    }
    private IEnumerator Ending()
    {
        Fadecanvs.FadeIn();
        Startcanvs.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Fadecanvs.FadeOut();
        gameObject.SetActive(false);
    }
}
