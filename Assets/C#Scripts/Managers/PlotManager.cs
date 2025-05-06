using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotManager : SingleTons<PlotManager>
{
    public PlotCanvs PlotCanvs;
    public Plot ThisRoomPlot;
    public void SetPlotText(Plot ThisPlot)
    {
        PlotCanvs.gameObject.SetActive(true);
        StartCoroutine(OnSetPlotText(ThisPlot));
    }
    public void SetRoomPlotText()
    {
        PlotCanvs.gameObject.SetActive(true);
        StartCoroutine(OnSetPlotText(ThisRoomPlot));
    }
    private IEnumerator OnSetPlotText(Plot ThisPlot)
    {
        PlotCanvs.ClearPlotTextBox();
        for (int i = 0; i < ThisPlot.PlotExcerpts[ThisPlot.CurrentIndex].Excerpts.Count; i++)
        {
            PlotCanvs.SetPlotText(ThisPlot.PlotExcerpts[ThisPlot.CurrentIndex].Excerpts[i]);
            yield return new WaitForSeconds(ThisPlot.PlotExcerpts[ThisPlot.CurrentIndex].Excerpts[i].WaitTime);
        }
        yield return new WaitForSeconds(1f);
        ThisPlot.CurrentIndex++;
        PlotCanvs.gameObject.SetActive(false);
    }
}
