using System;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class SaveSlot : MonoBehaviour
{
    private Button ThisButton;
    public Button DeleteDataButton;
    public int Index;
    public Text TimeText;
    public GameObject DeletePanel;
    private void Awake()
    {
        ThisButton = GetComponent<Button>();
        ThisButton.onClick.AddListener(OnLoad);
        DeleteDataButton.onClick.AddListener(OnDelete);
    }
    private void OnEnable()
    {
        LoadTimeText();
    }
    private void SaveTimeText()
    {
        if (Index == GameManager.Instance.GlobalData.SaveTime1.Index)
        {
            GameManager.Instance.GlobalData.SaveTime1.Time = DateTime.Now.ToString();
        }
        if (Index == GameManager.Instance.GlobalData.SaveTime2.Index)
        {
            GameManager.Instance.GlobalData.SaveTime2.Time = DateTime.Now.ToString();
        }
        if (Index == GameManager.Instance.GlobalData.SaveTime3.Index)
        {
            GameManager.Instance.GlobalData.SaveTime3.Time = DateTime.Now.ToString();
        }
    }
    private void LoadTimeText()
    {
        if(Index == GameManager.Instance.GlobalData.SaveTime1.Index)
        {
            if(GameManager.Instance.GlobalData.SaveTime1.Time != "")
            {
                TimeText.text = "最近游玩：" + GameManager.Instance.GlobalData.SaveTime1.Time;
            }
        }
        if(Index == GameManager.Instance.GlobalData.SaveTime2.Index)
        {
            if (GameManager.Instance.GlobalData.SaveTime2.Time != "")
            {
                TimeText.text = "最近游玩：" + GameManager.Instance.GlobalData.SaveTime2.Time;
            }
        }
        if(Index == GameManager.Instance.GlobalData.SaveTime3.Index)
        {
            if(GameManager.Instance.GlobalData.SaveTime3.Time != "")
            {
                TimeText.text = "最近游玩：" + GameManager.Instance.GlobalData.SaveTime3.Time;
            }
        }
    }
    private void OnLoad()
    {
        DataManager.Instance.Load(Index);
        SaveTimeText();
        DataManager.Instance.SaveGlobal();
        gameObject.transform.parent.parent.parent.gameObject.SetActive(false);
    }
    private void OnDelete()
    {
        if(TimeText.text != "")
        {
            DeletePanel.SetActive(true);
            DeletePanel.GetComponent<DeletePanel>().Index = Index;
            DeletePanel.GetComponent<DeletePanel>().TimeText = TimeText;
        }
    }
}
