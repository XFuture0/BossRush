using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorCanvs : MonoBehaviour
{
    public Text ColorName;
    public Text ColorDescription;
    public Text ColorUseDescription;
    private void Update()
    {
        if(ColorName.color != ColorManager.Instance.UpdateColor(2))
        {
            ColorName.color = ColorManager.Instance.UpdateColor(2);
        }
        if(ColorDescription.color != ColorManager.Instance.UpdateColor(2))
        {
            ColorDescription.color = ColorManager.Instance.UpdateColor(2);
        }
        if(ColorUseDescription.color != ColorManager.Instance.UpdateColor(2))
        {
            ColorUseDescription.color = ColorManager.Instance.UpdateColor(2);
        }
    }
    public void SetColorText(string name, string description,string usedescription) 
    {
        ColorName.text = name;
        ColorDescription.text = description;
        ColorUseDescription.text = usedescription;
    }
}
