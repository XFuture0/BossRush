using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultSlot : MonoBehaviour
{
    public GameObject DifficultPanel;
    public int ThisIndex = 0;
    public Button ChooseButton;
    private Image image;
    private void Awake()
    {
        ChooseButton.onClick.AddListener(OnChooseButton);
        image = GetComponent<Image>();
    }
    public void OnChooseButton()
    {
        DifficultPanel.GetComponent<DifficultPanel>().ClearChoose();
        image.color = new Color(0.8f, 0.8f, 0.8f, 1f);
        DifficultPanel.GetComponent<DifficultPanel>().ChooseDifficult(ThisIndex);
    }
}
