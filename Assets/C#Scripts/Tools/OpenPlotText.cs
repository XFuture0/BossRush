using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPlotText : MonoBehaviour
{
    public Plot ThisPlot;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlotManager.Instance.SetPlotText(ThisPlot);
        }
    }
}
