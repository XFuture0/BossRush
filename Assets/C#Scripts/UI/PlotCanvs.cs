using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Plot;

public class PlotCanvs : MonoBehaviour
{
    public GameObject PlotText;
    public Transform PlotTextBox;
    public void SetPlotText(ExcerptText ExcerptText)
    {
        for(int i = 0; i < PlotTextBox.childCount; i++)
        {
            PlotTextBox.GetChild(i).gameObject.GetComponent<PlotText>().UpPosition();
        }
        var NewPlot = Instantiate(PlotText,PlotTextBox);
        NewPlot.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -573.74f);
        NewPlot.transform.GetChild(3).gameObject.GetComponent<Text>().text = ExcerptText.Text;
        switch (ExcerptText.CharacterType) 
        {
            case CharacterType.Player:
                NewPlot.transform.GetChild(1).gameObject.SetActive(true);
                break;
            case CharacterType.Boss:
                NewPlot.transform.GetChild(2).gameObject.SetActive(true);
                break;
        }
    }
    public void ClearPlotTextBox()
    {
        for (int i = 0; i < PlotTextBox.childCount; i++)
        {
            Destroy(PlotTextBox.GetChild(i).gameObject);
        }
    }
}
